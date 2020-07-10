using MarioPartyEditor.Util;
using System;
using System.IO;

namespace MarioPartyEditor.ROM
{
    public class NDSFile
    {
        public NDSFilesystem Filesystem { get; private set; }

        /// <summary>
        /// The parent directory's entry ID.
        /// </summary>
        public ushort ParentID { get; private set; }

        /// <summary>
        /// The containing directory of this file.
        /// </summary>
        public NDSDirectory Parent { get => Filesystem.Directories.TryGetValue(ParentID, out var val) ? val : null; }
        public string FullPath => (Parent?.FullPath ?? "") + "/" + Name;

        /// <summary>
        /// The entry ID of this directory in the FAT.
        /// </summary>
        public ushort EntryID { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Retrieves the File Address Table from the ROM.
        /// </summary>
        ByteSlice RawFAT => Filesystem.ROM.Data.Slice((int)Filesystem.ROM.Header.FATAddress, (int)Filesystem.ROM.Header.FATSize);
        const int FATEntrySize = 8;

        public NDSFile(NDSFilesystem fs, ushort entryID, string name, ushort parentID)
        {
            Filesystem = fs;
            EntryID = entryID;
            Name = name;
            ParentID = parentID;
        }

        public virtual ByteSlice RetrieveContents()
        {
            var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
            uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
            uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
            return Filesystem.ROM.Data.Slice((int)lowerBound, (int)(upperBound - lowerBound));
        }
    }

    /// <summary>
    /// Simulates a NDSFile with data that is outside of the ROM.
    /// </summary>
    public sealed class NDSExternalFile : NDSFile
    {
        public string ExternalFilepath { get; private set; }

        public NDSExternalFile(NDSFilesystem fs, ushort entryID, string name, ushort parentID, string externalFilepath) : base(fs, entryID, name, parentID) => ExternalFilepath = externalFilepath;

        public override ByteSlice RetrieveContents()
        {
            using(var file = File.OpenRead(ExternalFilepath))
            {
                byte[] contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);
                return new ByteSlice(contents);
            }
        }
    }
}
