using MarioPartyEditor.Util;

namespace MarioPartyEditor.ROM
{
    public class NDSROM
    {
        public ByteSlice Data { get; set; }
        public NDSHeader Header { get; }
        public NDSFilesystem Filesystem { get; }

        public NDSROM() { Header = new NDSHeader(this); Filesystem = new NDSFilesystem(this); }
    }
}
