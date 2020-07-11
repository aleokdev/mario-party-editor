using System;
using System.IO;
using System.Linq;

namespace NDSUtils
{
    public class NDSFile
    {
        public NDSFilesystem Filesystem { get; private set; }

        /// <summary>
        /// The containing directory of this file.
        /// </summary>
        public NDSDirectory Parent { get; set; }
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

        public NDSFile(NDSFilesystem fs, ushort entryID, string name)
        {
            Filesystem = fs;
            EntryID = entryID;
            Name = name;
        }

        public virtual ByteSlice RetrieveContents()
        {
            var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
            uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
            uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
            return Filesystem.ROM.Data.Slice((int)lowerBound, (int)(upperBound - lowerBound));
        }

        public virtual int Size()
        {
            var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
            uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
            uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
            return (int)(upperBound - lowerBound);
        }

        /// <summary>
        /// Replaces this file in the containing directory with another one.
        /// </summary>
        public void ReplaceWith(NDSFile file)
        {
            for(int i = 0; i < Parent.ChildrenFiles.Count; i++)
            {
                if(Parent.ChildrenFiles[i] == this)
                {
                    Parent.ChildrenFiles[i] = file;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Simulates a NDSFile with data that is outside of the ROM.
    /// </summary>
    public sealed class NDSExternalFile : NDSFile
    {
        public string ExternalFilepath { get; private set; }

        public NDSExternalFile(NDSFilesystem fs, ushort entryID, string name, string externalFilepath) : base(fs, entryID, name) => ExternalFilepath = externalFilepath;

        public override ByteSlice RetrieveContents()
        {
            using(var file = File.OpenRead(ExternalFilepath))
            {
                byte[] contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);
                return new ByteSlice(contents);
            }
        }

        public override int Size() => (int)new FileInfo(ExternalFilepath).Length;
    }
}
