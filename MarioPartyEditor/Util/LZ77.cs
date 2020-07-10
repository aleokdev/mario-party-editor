using System;
using System.IO;

namespace MarioPartyEditor.Util
{
    public static class LZ77
    {
        public static byte[] Decompress(ByteSlice data)
        {
            // LZ77 in Mario Party DS always starts with an unknown byte

            // Don't ask how this works, I still have to figure it out myself...
            // Adapted from https://github.com/Dirbaio/NSMB-Editor/blob/master/NSMBe4/ROM.cs
            byte[] uncompressedLengthData = new byte[4];
            Array.Copy(data.Slice(1, 3).GetAsArrayCopy(), uncompressedLengthData, 3);
            int uncompressedLength = BitConverter.ToInt32(uncompressedLengthData, 0);
            var input = new BinaryReader(new MemoryStream(data.GetAsArrayCopy()));
            input.BaseStream.Seek(4, SeekOrigin.Begin);
            var output = new byte[uncompressedLength];
            int outputIndex = 0;
            while (outputIndex < uncompressedLength)
            {
                byte d = input.ReadByte();
                if (d != 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((d & 0x80) != 0)
                        {
                            int data_ = (input.ReadByte() << 8) | input.ReadByte();
                            int length = (data_ >> 12) + 3;
                            int offset = data_ & 0xFFF;
                            int windowOffset = outputIndex - offset - 1;
                            for (int j = 0; j < length; j++)
                            {
                                output[outputIndex++] = output[windowOffset++];

                                if (outputIndex == uncompressedLength)
                                {
                                    return output;
                                }
                            }
                        }
                        else
                        {
                            output[outputIndex++] = input.ReadByte();

                            if (outputIndex == uncompressedLength)
                            {
                                return output;
                            }
                        }
                        d <<= 1;
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        output[outputIndex++] = input.ReadByte();

                        if (outputIndex == uncompressedLength)
                        {
                            return output;
                        }
                    }
                }
            }
            return output;
        }

        public static bool IsCompressed(string filename)
            => filename.EndsWith("LZ.bin");
    }
}
