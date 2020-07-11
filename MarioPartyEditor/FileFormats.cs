using System;
using System.IO;
using NDSUtils;

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

    [FileFormatChecker("Text Table")]
    public static class TextTableFileFormat
    {
        public static bool Check(NDSFile file)
        {
            if (Path.GetExtension(file.Name) != ".bin") return false;
            // The theoretical minimum size for a text table is the text count + first text address
            // + null byte as the first text.
            const uint minimumSize = sizeof(uint) + sizeof(uint) + 1;
            if (file.Size < minimumSize) return false;
            return true;
        }
    }

    [FileFormatChecker("Text")]
    public static class TextFileFormat
    {
        public static bool Check(NDSFile file)
        {
            return Path.GetExtension(file.Name) == ".txt";
        }
    }

    [FileFormatChecker("Sound Data")]
    public static class SoundDataFileFormat
    {
        public static bool Check(NDSFile file)
        {
            return Path.GetExtension(file.Name) == ".sdat";
        }
    }
}
