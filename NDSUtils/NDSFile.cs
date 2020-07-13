using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NDSUtils
{
    [Serializable]
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

        public List<ByteSlice> Patches { get; private set; } = new List<ByteSlice>();

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

        /// <summary>
        /// Returns the original file contents without patching.
        /// </summary>
        /// <returns></returns>
        public virtual ByteSlice RetrieveOriginalContents()
        {
            var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
            uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
            uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
            return Filesystem.ROM.Data.Slice((int)lowerBound, (int)(upperBound - lowerBound));
        }

        /// <summary>
        /// Returns the file contents completely patched up.
        /// </summary>
        /// <returns></returns>
        public ByteSlice RetrievePatchedContents()
        {
            ByteSlice originalData = RetrieveOriginalContents();
            ByteSlice patchedData = new ByteSlice(originalData.GetAsArrayCopy());
            foreach (var patch in Patches)
            {
                patchedData = XDelta.ApplyPatch(patch, patchedData);
            }

            return patchedData;
        }

        public virtual int OriginalSize
        {
            get
            {
                var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
                uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
                uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
                return (int)(upperBound - lowerBound);
            }
        }

        public int PatchedSize
        {
            get
            {
                // TODO: OPTIMIZE THIS. THIS IS EXTREMELY SLOW. FINISH THE VCS. PLEASE.
                return RetrievePatchedContents().Size;
            }
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
            throw new Exception("Could not find this file in its parent directory. Malformed filesystem?");
        }
    }

    /// <summary>
    /// Simulates a NDSFile with data that is outside of the ROM.
    /// </summary>
    public sealed class NDSExternalFile : NDSFile
    {
        public string ExternalFilepath { get; private set; }

        public NDSExternalFile(NDSFilesystem fs, ushort entryID, string name, string externalFilepath) : base(fs, entryID, name) => ExternalFilepath = externalFilepath;

        public override ByteSlice RetrieveOriginalContents()
        {
            using(var file = File.OpenRead(ExternalFilepath))
            {
                byte[] contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);
                return new ByteSlice(contents);
            }
        }

        public override int OriginalSize { get => (int)new FileInfo(ExternalFilepath).Length; }
    }
}
