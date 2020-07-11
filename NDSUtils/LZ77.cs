using System;
using System.IO;

namespace NDSUtils
{
    public static class LZ77
    {
        public static byte[] Decompress(ByteSlice inputData)
        {
            // LZ77 in Mario Party DS always starts with an unknown byte

            // Don't ask how this works, I still have to figure it out myself...
            // Adapted from https://github.com/Dirbaio/NSMB-Editor/blob/master/NSMBe4/ROM.cs
            byte[] uncompressedLengthData = new byte[4];
            Array.Copy(inputData.Slice(1, 3).GetAsArrayCopy(), uncompressedLengthData, 3);
            int uncompressedLength = BitConverter.ToInt32(uncompressedLengthData, 0);
            var input = new BinaryReader(new MemoryStream(inputData.GetAsArrayCopy()));
            input.BaseStream.Seek(4, SeekOrigin.Begin);
            var output = new byte[uncompressedLength];
            int outputIndex = 0;
            while (outputIndex < uncompressedLength)
            {
                byte decisionByte = input.ReadByte();
                for (int bit = 7; bit >= 0; bit--)
                {
                    if ((decisionByte & (1 << bit)) != 0)
                    {
                        int pointerData = (input.ReadByte() << 8) | input.ReadByte();
                        int length = (pointerData >> 12) + 3;
                        int offset = pointerData & 0xFFF;
                        int windowOffset = outputIndex - offset - 1;
                        for (int pointByte = 0; pointByte < length; pointByte++)
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
                }
            }
            return output;
        }

        public static bool IsCompressed(NDSFile file)
            => file.Name.ToLower().EndsWith("lz.bin");
    }
}
