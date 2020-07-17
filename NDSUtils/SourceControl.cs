using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NDSUtils
{
    /// <summary>
    /// Represents a file change. Contains a patch to go backwards from the current state and a reference to a commit.
    /// </summary>
    [Serializable]
    public readonly struct VCSChange : ISerializable
    {
        public readonly XDeltaPatch RollbackPatch;
        public readonly VCSCommit ParentCommit;
        public readonly NDSFile ParentFile;

        public VCSChange(VCSCommit parentCommit, NDSFile parentFile, XDeltaPatch rollbackPatch)
        {
            RollbackPatch = rollbackPatch;
            ParentCommit = parentCommit;
            ParentFile = parentFile;
        }
        #region Serialization / Deserialization
        public VCSChange(SerializationInfo info, StreamingContext context)
        {
            RollbackPatch = (XDeltaPatch)info.GetValue(nameof(RollbackPatch), typeof(XDeltaPatch));
            ParentCommit = (VCSCommit)info.GetValue(nameof(ParentCommit), typeof(VCSCommit));
            ParentFile = (NDSFile)info.GetValue(nameof(ParentFile), typeof(NDSFile));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(RollbackPatch), RollbackPatch);
            info.AddValue(nameof(ParentCommit), ParentCommit);
            info.AddValue(nameof(ParentFile), ParentFile);
        }
        #endregion

        public override string ToString()
        {
            return ParentFile.Name;
        }
    }

    [Serializable]
    public class VCSCommit
    {
        /// <summary>
        /// When the commit was finished. Null if the commit hasn't been pushed to the stack yet.
        /// </summary>
        public DateTime? Time { get; set; } = null;

        /// <summary>
        /// A short name describing the changes done in the commit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the changes done in the commit.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of changes done in the commit.
        /// </summary>
        public ObservableCollection<VCSChange> Changes { get; set; } = new ObservableCollection<VCSChange>();

        public override string ToString() => Name;
    }
}
