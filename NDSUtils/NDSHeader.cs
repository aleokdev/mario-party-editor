using System;
using System.Text;

namespace NDSUtils
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

        public uint ARM9CodeAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x20, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x20, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM9CodeSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x2C, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x2C, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM7CodeAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x30, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x30, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM7CodeSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x3C, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x3C, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM9OverlayTableAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x50, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x50, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM9OverlayTableSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x54, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x54, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM7OverlayTableAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x30, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x30, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint ARM7OverlayTableSize
        {
            get => BitConverter.ToUInt32(Data.Slice(0x58, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x58, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }

        public uint BannerAddress
        {
            get => BitConverter.ToUInt32(Data.Slice(0x68, sizeof(uint)).GetAsArrayCopy(), 0);
            set => Data.Slice(0x68, sizeof(uint)).ReplaceWith(new ByteSlice(BitConverter.GetBytes(value)));
        }
        public uint BannerSize => 0x840;
    }
}
