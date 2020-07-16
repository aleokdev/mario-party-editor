using NDSUtils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MarioPartyEditor
{
    [Serializable]
    public class EditorProject
    {
        public NDSROM _romEditing;
        public NDSROM ROMEditing
        {
            get => _romEditing;
            set
            {
                OnROMEditingChange?.Invoke(_romEditing, value);
                _romEditing = value;
            }
        }
        public delegate void OnROMEditingChangeHandler(NDSROM romBefore, NDSROM romAfter);
        [field: NonSerialized]
        public event OnROMEditingChangeHandler OnROMEditingChange;

        private VCSCommit _headCommit = new VCSCommit();
        public VCSCommit HeadCommit { get => _headCommit; set { _headCommit = value; OnHeadCommitChange?.Invoke(null, null); } }
        [field: NonSerialized]
        public event EventHandler OnHeadCommitChange;

        public Stack<VCSCommit> PastCommits { get; } = new Stack<VCSCommit>();
    }
}
