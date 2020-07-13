using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDSUtils
{
    /// <summary>
    /// Represents a file change. Contains a patch and a reference to a commit.
    /// </summary>
    public class SourceControlChange
    {
        public ByteSlice Patch { get; set; }
        public SourceControlCommit ParentCommit { get; set; }
    }

    public class SourceControlCommit
    {
        /// <summary>
        /// When the commit was finished.
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// A short description of the changes done in the commit.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// A list of changes done in the commit.
        /// </summary>
        List<SourceControlChange> Changes { get; set; } = new List<SourceControlChange>();
    }
}
