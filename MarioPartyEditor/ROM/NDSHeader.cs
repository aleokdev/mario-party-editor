using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MarioPartyEditor.ROM
{

    public class NDSHeader
    {
        public NDSROM ROM { get; private set; }
        public int Size => (int)BitConverter.ToUInt32(ROM.Data.Slice(0x84, sizeof(uint)).GetAsArrayCopy(), startIndex: 0);
        public ByteSlice Data => ROM.Data.Slice(0, Size);

        public NDSHeader(NDSROM rom) => ROM = rom;

        public string GameCode
        {
            get => Encoding.UTF8.GetString(Data.Slice(0x0C, 4).GetAsArrayCopy());
        }

        /// <summary>
        /// Retrieves or sets the File Name Table starting address.
        /// </summary>
        public uint FNTAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x40, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x40, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        /// <summary>
        /// Retrieves or sets the File Name Table size.
        /// </summary>
        public uint FNTSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x44, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x44, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        /// <summary>
        /// Retrieves or sets the File Name Table starting address.
        /// </summary>
        public uint FATAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x48, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x48, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        /// <summary>
        /// Retrieves or sets the File Name Table size.
        /// </summary>
        public uint FATSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x4C, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x4C, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }
    }
}
