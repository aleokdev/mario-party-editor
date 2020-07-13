using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace NDSUtils
{
    public static class LZ77
    {
        public static byte[] Decompress(ByteSlice inputData)
        {
            // LZ77 in Mario Party DS always starts with an unknown byte

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
                    bool referencePastData = (decisionByte & (1 << bit)) != 0;
                    if (referencePastData)
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

        /// <summary>
        /// Searches for the largest data match in a byte array from position pos onwards.
        /// </summary>
        /// <param name="data">The data to search.</param>
        /// <param name="pos">The starting position for where to search what to match against, as an index to the data parameter.</param>
        /// <param name="maxDistance">The maximum distance to move from pos backwards to search for a match.</param>
        /// <param name="maxLength">The maximum length that the match can have. Inclusive.</param>
        /// <param name="matchPos">The position of the largest match, as an index to the data parameter. If no match was found, this paramter will return zero.</param>
        /// <param name="matchLength">The length of the largest match (Starting from parameter matchPos). If no match was found, this parameter will return zero.</param>
        private static void SearchForMatch(ByteSlice data, int pos, int maxDistance, int maxLength, out int matchPos, out int matchLength)
        {
            // Initialize output parameters
            matchPos = 0;
            matchLength = 0;

            // Search for matches from the farthest we can go backwards
            for (int index = Math.Max(0, pos - maxDistance); index < pos; index++)
            {
                // Calculate the match length from the `index` position
                int thisMatchLength = 0;
                for (;
                    pos + thisMatchLength < data.Size &&
                    index + thisMatchLength < pos &&
                    thisMatchLength < maxLength &&
                    data[index + thisMatchLength] == data[pos + thisMatchLength];
                    thisMatchLength++) ;

                // If this match was the largest we've seen yet, set the corresponding output parameters
                if (thisMatchLength > matchLength)
                {
                    matchPos = index;
                    matchLength = thisMatchLength;
                }

                // As an optimization, return if this match is already the largest we can obtain
                if (thisMatchLength == maxLength) return;
            }
        }

        public static byte[] Compress(ByteSlice inputData)
        {
            // I recommend you read the LZ77 COMPRESSION chapter in the README to understand better what's happening.
            // For a brief explanation about how the actual compression method works, take a brief look at
            // https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-wusp/fb98aa28-5cd7-407f-8869-a6cef1ff1ccb.

            BinaryWriter writer = new BinaryWriter(new MemoryStream());

            // First, write the uncompressed size (First byte is unknown, so we write 0x10, next three bytes are the size)
            writer.Write((uint)((inputData.Size << 8) | 0x10));

            for (int inputIndex = 0; inputIndex < inputData.Size; )
            {
                // Remember where to place the decision byte once we do 8 iterations of the compression algorithm.
                long decisionByteOffset = writer.BaseStream.Position;

                // Write a placeholder for the decision byte
                writer.Write((byte)0);
                byte decisionByte = 0;
                for (int decisionBit = 7; decisionBit >= 0; decisionBit--)
                {
                    // The maximum LZ77 length we can have is 0xF + matchMinLength and the maximum offset is 0xFFF,
                    // because the information is encoded in a short like `F FFF` where the first F is the length
                    // and the three other Fs are the offset.
                    const int matchMinLength = 3;
                    const int matchMaxOffset = 0xFFF;
                    const int matchMaxLength = 0xF + matchMinLength;
                    SearchForMatch(inputData, inputIndex, matchMaxOffset, matchMaxLength, out int matchPos, out int matchLength);

                    if(matchLength >= matchMinLength)
                    {
                        // We found a proper match, so we can reference previous data.
                        // First, let's set the decision bit so we know we're referencing data.
                        decisionByte |= (byte)(1 << decisionBit);
                        int relativeMatchPos = inputIndex - matchPos - 1;

                        ushort refPointer = (ushort)(((matchLength - matchMinLength) << 12) | (relativeMatchPos & 0xFFF));
                        refPointer = (ushort)((refPointer << 8) | (refPointer >> 8)); // Convert to Big Endian
                        writer.Write(refPointer);
                        inputIndex += matchLength;
                    } else
                    {
                        // We didn't find any match, so we just copy a byte from the input data.
                        writer.Write(inputData[inputIndex++]);
                    }

                    if (inputIndex >= inputData.Size) break;
                }

                // Write the decision byte and go back to the end of the stream.
                writer.Seek((int)decisionByteOffset, SeekOrigin.Begin);
                writer.Write(decisionByte);
                writer.Seek(0, SeekOrigin.End);
            }

            return ((MemoryStream)writer.BaseStream).ToArray();
        }

        public static bool IsCompressed(NDSFile file)
            => file.Name.ToLower().EndsWith("lz.bin");
    }
}
