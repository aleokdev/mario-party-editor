using NDSUtils;

namespace MarioPartyEditor
{
    public static class EditorData
    {
        public static NDSROM _romEditing;
        public static NDSROM ROMEditing
        {
            get => _romEditing;
            set
            {
                OnROMEditingChange?.Invoke(_romEditing, value);
                _romEditing = value;
            }
        }

        public delegate void OnROMEditingChangeHandler(NDSROM romBefore, NDSROM romAfter);
        public static event OnROMEditingChangeHandler OnROMEditingChange;
    }
}
