using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioPartyEditor.ROM
{
    public class NDSROM
    {
        public ByteSlice Data { get; set; }
        public NDSHeader Header { get; }
        public NDSFilesystem Filesystem { get; }

        public NDSROM() { Header = new NDSHeader(this); Filesystem = new NDSFilesystem(this); }
    }

    public static class ArrayHelpers
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
