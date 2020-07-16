using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace NDSUtils
{
    [Serializable]
    public class NDSFile : ISerializable
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
        /// All the changes done from the original file. First element -> First change, last element -> last change.
        /// </summary>
        Stack<VCSChange> changes = new Stack<VCSChange>();

        /// <summary>
        /// Stores the latest file version data. Not serialized, the individual change patches are stored instead.
        /// </summary>
        [NonSerialized]
        private byte[] latestVersion;

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
            latestVersion = RetrieveOriginalData().GetAsArrayCopy();
        }

        #region Serialization / Deserialization
        protected NDSFile(SerializationInfo info, StreamingContext context)
        {
            Filesystem = info.GetValue(nameof(Filesystem), typeof(NDSFilesystem)) as NDSFilesystem;
            Parent = info.GetValue(nameof(Parent), typeof(NDSDirectory)) as NDSDirectory;
            EntryID = info.GetUInt16(nameof(EntryID));
            Name = info.GetString(nameof(Name));
            changes = info.GetValue(nameof(changes), typeof(Stack<VCSChange>)) as Stack<VCSChange>;
            // If there are no changes, there won't be a latest version patch
            var latestVersionPatch = (XDeltaPatch?)info.GetValue("latestVersionPatch", typeof(XDeltaPatch?));
            if (latestVersionPatch == null)
                latestVersion = RetrieveOriginalData().GetAsArrayCopy();
            else
                latestVersion = latestVersionPatch?.Apply(RetrieveOriginalData().GetAsArrayCopy());
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Filesystem), Filesystem, typeof(NDSFilesystem));
            info.AddValue(nameof(Parent), Parent, typeof(NDSDirectory));
            info.AddValue(nameof(EntryID), EntryID);
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(changes), changes, typeof(Stack<VCSChange>));
            // Small optimization: If there are no changes, don't save latest version patch
            if(changes.Count == 0)
                info.AddValue("latestVersionPatch", null, typeof(XDeltaPatch?));
            else
            {
                var latestVersionPatch = new XDeltaPatch(RetrieveOriginalData().GetAsArrayCopy(), latestVersion);
                info.AddValue("latestVersionPatch", latestVersionPatch, typeof(XDeltaPatch));
            }
        }
        #endregion

        /// <summary>
        /// Returns the original file contents without patching.
        /// </summary>
        /// <returns></returns>
        public virtual ByteSlice RetrieveOriginalData()
        {
            var fatEntry = RawFAT.Slice(EntryID * FATEntrySize, FATEntrySize);
            uint lowerBound = BitConverter.ToUInt32(fatEntry.Slice(0, sizeof(uint)).GetAsArrayCopy(), 0);
            uint upperBound = BitConverter.ToUInt32(fatEntry.Slice(sizeof(uint), sizeof(uint)).GetAsArrayCopy(), 0);
            return Filesystem.ROM.Data.Slice((int)lowerBound, (int)(upperBound - lowerBound));
        }

        /// <summary>
        /// Returns the latest file contents.
        /// </summary>
        /// <returns></returns>
        public byte[] RetrieveLatestVersionData() => latestVersion;

        public IEnumerable<VCSChange> EnumeratePastChanges() => changes.AsEnumerable();

        public void RollbackLastChange()
        {
            VCSChange change = changes.Pop();
            change.RollbackPatch.Apply(latestVersion);
        }

        public void AddNewVersion(VCSCommit parentCommit, byte[] newData)
        {
            // First remove all changes for this file in the parent commit, if any.
            foreach(var otherChange in from ch in parentCommit.Changes where ch.ParentFile == this select ch)
                parentCommit.Changes.Remove(otherChange);

            // Add this change to the parent commit and to the list of local changes.
            var change = new VCSChange(parentCommit, this, new XDeltaPatch(source: latestVersion, target: newData));
            changes.Push(change);
            parentCommit.Changes.Add(change);

            // Update the latest version to this new data.
            latestVersion = newData;
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

        public int LatestVersionSize
        {
            get => changes.Count == 0 ? latestVersion.Length : changes.Peek().RollbackPatch.PatchedFilesize;
        }

        /// <summary>
        /// Replaces this file in the containing directory with another one.
        /// </summary>
        public void ReplaceWith(NDSFile file)
        {
            for (int i = 0; i < Parent.ChildrenFiles.Count; i++)
            {
                if (Parent.ChildrenFiles[i] == this)
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

        public override ByteSlice RetrieveOriginalData()
        {
            using (var file = File.OpenRead(ExternalFilepath))
            {
                byte[] contents = new byte[file.Length];
                file.Read(contents, 0, (int)file.Length);
                return new ByteSlice(contents);
            }
        }

        public override int OriginalSize { get => (int)new FileInfo(ExternalFilepath).Length; }
    }
}
