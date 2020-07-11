using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace System
{
    public static class PathHelpers
    {
        public static string GetRelativePath(string origin, string target)
        {
            // The path combination with "." is needed because relative Uris for some reason take the last directory of
            // the relative and add it to the target (i.e. C:\th\ing\test to C:\th\ing is ing\test instead of just test)
            return Uri.UnescapeDataString(
                        new Uri(Path.Combine(origin, ".")).MakeRelativeUri(new Uri(target))
                            .ToString()
                            .Replace('/', Path.DirectorySeparatorChar)
                        );
        }

        public static string GetParent(string path)
        {
            string pathUntilLastSeparator = "";
            string pathUntilNow = "";
            for(int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/') pathUntilLastSeparator = pathUntilNow;
                pathUntilNow += path[i];
            }
            return pathUntilLastSeparator;
        }

        /// <summary>
        /// Returns either the last directory or file to appear in a path.
        /// </summary>
        /// <returns></returns>
        public static string GetLastComponent(string path)
        {
            string pathFromLastSeparator = "";
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/') pathFromLastSeparator = "";
                pathFromLastSeparator += path[i];
            }
            return pathFromLastSeparator;
        }

        /// <summary>
        /// Returns the first directory or file to appear in a path.
        /// </summary>
        /// <returns></returns>
        public static string GetFirstComponent(string path)
        {
            string pathUntilNow = "";
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/') return pathUntilNow;
                pathUntilNow += path[i];
            }
            return pathUntilNow;
        }
    }
}
