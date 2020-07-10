using System;
using System.IO;

namespace MarioPartyEditor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FileFormatCheckerAttribute : Attribute
    {
        public string FormatName { get; private set; }

        public FileFormatCheckerAttribute(string formatName)
        {
            FormatName = formatName;
        }
    }

    [FileFormatChecker("Text Database")]
    public static class TextTableFileFormat
    {
        public static bool Check(string filepath)
        {
            if (Path.GetExtension(filepath) != ".bin") return false;
            // The theoretical minimum size for a text table is the text count + first text address
            // + null byte as the first text.
            const uint minimumSize = sizeof(uint) + sizeof(uint) + 1;
            if (new FileInfo(filepath).Length < minimumSize) return false;
            return true;
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
