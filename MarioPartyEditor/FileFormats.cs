using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [FileFormatChecker("Text")]
    public static class TextFileFormat
    {
        public static bool Check(BinaryReader reader)
        {
            return true;
        }
    }
}
