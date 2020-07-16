using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDSUtils
{
    [Serializable]
    public class NDSFilesystem
    {
        public NDSROM ROM { get; private set; }

        public NDSDirectory RootDataDirectory;
        public NDSDirectory RootOverlayDirectory;

        /// <summary>
        /// Retrieves the File Name Table from the ROM.
        /// </summary>
        ByteSlice RawFNT => ROM.Data.Slice((int)ROM.Header.FNTAddress, (int)ROM.Header.FNTSize);

        const ushort EntryDirectoryFlag = 0xF000;
        const int DirectoryEntrySize = 8;

        /// <summary>
        /// Locate and create a directory from the root of a ROM file.
        /// </summary>
        /// <param name="rom">The ROM to use.</param>
        public NDSFilesystem(NDSROM rom)
        {
            ROM = rom;
        }

        public void Initialize()
        {
            Dictionary<ushort, NDSDirectory> directories = new Dictionary<ushort, NDSDirectory>();
            Dictionary<ushort, NDSFile> files = new Dictionary<ushort, NDSFile>();

            // The maximum address we can access while reading metaentries.
            uint dirEntriesAddrLimit = BitConverter.ToUInt32(RawFNT.Slice(0, sizeof(uint)).GetAsArrayCopy(), startIndex: 0);
            // We create a dictionary for children before setting them because folders/files may be out of order
            // and parents may not exist before referencing them.
            var childrenStructure = new Dictionary<ushort, List<ushort>>();

            // Everything before this entry in the FAT is an overlay file.
            ushort firstDataFileID = BitConverter.ToUInt16(RawFNT.Slice(sizeof(uint), sizeof(ushort)).GetAsArrayCopy(), startIndex: 0);
            // 0xFFFF is just a fictional ID used for convenience.
            RootOverlayDirectory = new NDSDirectory(this, 0xFFFF, "overlay");
            for (ushort overlayID = 0; overlayID < firstDataFileID; overlayID++)
                RootOverlayDirectory.ChildrenFiles.Add(new NDSFile(this, overlayID, $"overlay_{overlayID}") { Parent = RootOverlayDirectory });

            for (int currentRelativeAddress = 0; currentRelativeAddress < dirEntriesAddrLimit; currentRelativeAddress += DirectoryEntrySize)
            {
                ByteSlice dirEntry = RawFNT.Slice(currentRelativeAddress, DirectoryEntrySize);
                // The address of the first named entry inside of this directory.
                uint namedEntryAddress = BitConverter.ToUInt32(dirEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), startIndex: 0);
                // The ID of the first file in this directory.
                ushort firstFileID = BitConverter.ToUInt16(dirEntry.Slice(sizeof(uint), sizeof(ushort)).GetAsArrayCopy(), startIndex: 0);
                // The ID of the parent of this directory. If folder is root, this will not start with 0xF000.
                ushort parentDirID = BitConverter.ToUInt16(dirEntry.Slice(sizeof(uint) + sizeof(ushort), sizeof(ushort)).GetAsArrayCopy(), startIndex: 0);
                // The ID of the directory that all these files belong to.
                ushort containerDirID = (ushort)(currentRelativeAddress / DirectoryEntrySize + EntryDirectoryFlag);

                ByteSlice namedEntry = RawFNT.Slice((int)namedEntryAddress, RawFNT.Size);
                readNamedEntry(namedEntry, firstFileID);

                void readNamedEntry(ByteSlice namedEntry, ushort fileID)
                {
                    if (namedEntry[0] == 0) return;

                    bool isDirectory = (namedEntry[0] & 0b10000000) > 0;
                    byte nameLength = (byte)(namedEntry[0] & 0b01111111);
                    string name = Encoding.UTF8.GetString(namedEntry.Slice(1, nameLength).GetAsArrayCopy());

                    if (isDirectory)
                    {
                        ushort entryID = BitConverter.ToUInt16(namedEntry.Slice(1 + nameLength, sizeof(ushort)).GetAsArrayCopy(), startIndex: 0);
                        namedEntry.SliceEnd = namedEntry.SliceStart + 1 + nameLength + sizeof(ushort);
                        var directory = new NDSDirectory(this, entryID, name);
                        directories.Add(entryID, directory);

                        // If the parent is not a directory, then this entry represents the root directory (/data/)
                        if ((parentDirID & EntryDirectoryFlag) == 0)
                            RootDataDirectory = directory;
                        else
                        {
                            childrenStructure.TryGetValue(containerDirID, out var parent);
                            if (parent == null)
                            {
                                childrenStructure.Add(containerDirID, new List<ushort>() { entryID });
                            }
                            else
                            {
                                parent.Add(entryID);
                            }
                        }
                    }
                    else
                    {
                        namedEntry.SliceEnd = namedEntry.SliceStart + 1 + nameLength;
                        var file = new NDSFile(this, fileID, name);
                        if (files.ContainsKey(fileID))
                        {
                            files[fileID] = file;
                            Console.WriteLine($"[NDSFilesystem.Initialize] The file {fileID} was repeated!");
                        }
                        else
                        {
                            files.Add(fileID, file);
                        }

                        childrenStructure.TryGetValue(containerDirID, out var parent);
                        if (parent == null)
                        {
                            childrenStructure.Add(containerDirID, new List<ushort>() { fileID });
                        }
                        else
                        {
                            parent.Add(fileID);
                        }
                    }

                    // Read next entry
                    readNamedEntry(RawFNT.Slice(namedEntry.SliceEnd - RawFNT.SliceStart, RawFNT.Size), (ushort)(fileID + 1));
                }
            }

            foreach (var parentRelations in childrenStructure)
            {
                directories.TryGetValue(parentRelations.Key, out var parent);
                if (parent == null)
                {
                    Console.WriteLine($"[NDSFilesystem.Initialize] Parent 0x{parentRelations.Key:X} does not exist as a directory!! (First child: {files[parentRelations.Value.First()].Name})");
                    continue;
                }
                else
                {
                    foreach (var child in parentRelations.Value)
                    {
                        if ((child & EntryDirectoryFlag) > 0)
                        {
                            parent.ChildrenDirectories.Add(directories[child]);
                            directories[child].Parent = parent;
                        }
                        else
                        {
                            parent.ChildrenFiles.Add(files[child]);
                            files[child].Parent = parent;
                        }
                    }
                }
            }
        }

        // TODO: This is not the most elegant way to do this...
        /// <summary>
        /// Updates a byteslice with the same size as the ROM to reflect the filesystem's contents. This method will NOT change the header of the ROM,
        /// nor any sensitive data. For now, only updates the FAT & file data, not the FNT.
        /// </summary>
        public void PackTo(ByteSlice target)
        {
            ByteSlice targetRawFAT = target.Slice((int)ROM.Header.FATAddress, (int)ROM.Header.FATSize);
            ByteSlice targetHeader = ROM.Header.Data;
            targetHeader.Source = target.Source;
            ByteSlice targetRawFNT = RawFNT;
            targetRawFNT.Source = target.Source;
            const int fatEntrySize = 8;
            ByteSlice[] protectedMemoryRanges = new ByteSlice[]
            {
                targetHeader,
                targetRawFNT,
                targetRawFAT,
                target.Slice((int)ROM.Header.ARM9CodeAddress, (int)ROM.Header.ARM9CodeSize),
                target.Slice((int)ROM.Header.ARM7CodeAddress, (int)ROM.Header.ARM7CodeSize),
                target.Slice((int)ROM.Header.ARM9OverlayTableAddress, (int)ROM.Header.ARM9OverlayTableSize),
                target.Slice((int)ROM.Header.ARM7OverlayTableAddress, (int)ROM.Header.ARM7OverlayTableSize),
                target.Slice((int)ROM.Header.BannerAddress, (int)ROM.Header.BannerSize)
            };
            uint lastFileSaveAddress = 0;

            void SaveFile(NDSFile file)
            {
                int filesize = file.LatestVersionSize;
                ByteSlice GetFileSlice() => target.Slice((int)lastFileSaveAddress, filesize);
                IEnumerable<ByteSlice> calculateRangesIntersecting() =>
                    from range in protectedMemoryRanges where range.Intersects(GetFileSlice()) select range;
                for (var rangesIntersecting = calculateRangesIntersecting();
                    rangesIntersecting.Any();
                    rangesIntersecting = calculateRangesIntersecting())
                {
                    lastFileSaveAddress = (uint)rangesIntersecting.First().SliceEnd;
                }
                Console.WriteLine($"Found space for file: {lastFileSaveAddress:X}");
                if (lastFileSaveAddress + filesize > target.SliceEnd) throw new Exception("No space in ROM to fit any more files!");
                GetFileSlice().ReplaceWith(new ByteSlice(file.RetrieveLatestVersionData()));
                Console.WriteLine($"Changed {filesize}B.");

                var fileROMBounds = target.Slice((int)lastFileSaveAddress, filesize);

                // Change FAT entry (lower and upper bound)
                targetRawFAT.Slice(file.EntryID * fatEntrySize, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(fileROMBounds.SliceStart)));
                targetRawFAT.Slice(file.EntryID * fatEntrySize + sizeof(uint), sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(fileROMBounds.SliceEnd)));

                lastFileSaveAddress += (uint)filesize;
            }

            void SaveDir(NDSDirectory dir)
            {
                foreach(var child in dir.ChildrenDirectories)
                {
                    SaveDir(child);
                }
                foreach (var child in dir.ChildrenFiles)
                {
                    SaveFile(child);
                }
            }

            SaveDir(RootOverlayDirectory);
            SaveDir(RootDataDirectory);
        }
    }
}
