using System;
using System.IO;

namespace MarioPartyEditor
{
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FileFormatCheckerAttribute : System.Attribute
    {
        public string FormatName { get; private set; }

        public FileFormatCheckerAttribute(string formatName)
        {
            FormatName = formatName;
        }
    }

    [FileFormatChecker("Text Table")]
    public static class TextTableFileFormat
    {
        public static bool Check(string filepath)
        {
            if (Path.GetExtension(filepath) != ".bin") return false;
            using var file = File.OpenRead(filepath);
            var reader = new BinaryReader(file);
            if (reader.BaseStream.Length < sizeof(uint)) return false;
            uint textCount = reader.ReadUInt32();
            // I sincerely doubt there is any text file in the game with more than 64 text values.
            return textCount < 64;
        }
    }

    [FileFormatChecker("Text")]
    public static class TextFileFormat
    {
        public static bool Check(string filepath)
        {
            return Path.GetExtension(filepath) == ".txt";
        }
    }
}
