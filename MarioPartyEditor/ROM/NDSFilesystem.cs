using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarioPartyEditor.Util;

namespace MarioPartyEditor.ROM
{
    public class NDSFilesystem
    {
        public NDSROM ROM { get; private set; }

        public NDSDirectory RootDirectory;

        public Dictionary<ushort, NDSDirectory> Directories { get; private set; } = new Dictionary<ushort, NDSDirectory>();
        public Dictionary<ushort, NDSFile> Files { get; private set; } = new Dictionary<ushort, NDSFile>();

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
            // The maximum address we can access while reading metaentries.
            uint dirEntriesAddrLimit = BitConverter.ToUInt32(RawFNT.Slice(0, sizeof(uint)).GetAsArrayCopy(), startIndex: 0);
            // We create a dictionary for children before setting them because folders/files may be out of order
            // and parents may not exist before referencing them.
            var childrenStructure = new Dictionary<ushort, List<ushort>>();

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
                        var directory = new NDSDirectory(this, entryID, name, containerDirID);
                        Directories.Add(entryID, directory);

                        // If the parent is not a directory, then this entry represents the root directory (/data/)
                        if ((parentDirID & EntryDirectoryFlag) == 0)
                            RootDirectory = directory;
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
                        var file = new NDSFile(this, fileID, name, containerDirID);
                        if (Files.ContainsKey(fileID))
                        {
                            Files[fileID] = file;
                            Console.WriteLine($"[NDSFilesystem.Initialize] The file {fileID} was repeated!");
                        }
                        else
                            Files.Add(fileID, file);

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
                Directories.TryGetValue(parentRelations.Key, out var parent);
                if (parent == null)
                {
                    Console.WriteLine($"[NDSFilesystem.Initialize] Parent 0x{parentRelations.Key:X} does not exist as a directory!! (First child: {Files[parentRelations.Value.First()].Name})");
                    continue;
                }
                else
                {
                    foreach (var child in parentRelations.Value)
                    {
                        if ((child & EntryDirectoryFlag) > 0)
                            parent.ChildrenDirectories.Add(Directories[child]);
                        else
                            parent.ChildrenFiles.Add(Files[child]);
                    }
                }
            }
        }
    }
}
