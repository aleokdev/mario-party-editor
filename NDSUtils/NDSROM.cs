using System;

namespace NDSUtils
{
    [Serializable]
    public class NDSROM
    {
        public ByteSlice Data { get; set; }

        public NDSHeader Header { get => new NDSHeader(this); }
        public NDSFilesystem Filesystem { get; }

        public NDSROM() { Filesystem = new NDSFilesystem(this); }
    }
}
