﻿using System;

namespace NDSUtils
{
    [Serializable]
    public class NDSROM
    {
        public ByteSlice Data { get; set; }

        [field: NonSerialized]
        public NDSHeader Header { get; }
        public NDSFilesystem Filesystem { get; }

        public NDSROM() { Header = new NDSHeader(this); Filesystem = new NDSFilesystem(this); }
    }
}
