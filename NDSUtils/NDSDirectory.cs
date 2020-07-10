using System.Collections.Generic;

namespace NDSUtils
{
    public class NDSDirectory
    {
        public NDSFilesystem Filesystem { get; private set; }

        /// <summary>
        /// The parent directory's entry ID.
        /// </summary>
        public ushort ParentID { get; private set; }
        /// <summary>
        /// The parent of this directory, or null if this directory is the root one.
        /// </summary>
        public NDSDirectory Parent { get => Filesystem.Directories.TryGetValue(ParentID, out var val) ? val : null; }
        public string FullPath => (Parent?.FullPath ?? "") + "/" + Name;
        public List<NDSDirectory> ChildrenDirectories { get; private set; } = new List<NDSDirectory>();
        public List<NDSFile> ChildrenFiles { get; private set; } = new List<NDSFile>();

        /// <summary>
        /// The entry ID of this directory in the FAT.
        /// </summary>
        public ushort EntryID { get; private set; }

        /// <summary>
        /// The name of this directory.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Locate and create a directory from the root of a ROM file.
        /// </summary>
        /// <param name="rom">The ROM to use.</param>
        public NDSDirectory(NDSFilesystem fs, ushort entryID, string name, ushort parentID)
        {
            Filesystem = fs;
            ParentID = parentID;
            EntryID = entryID;
            Name = name;
            ParentID = parentID;
        }
    }
}
