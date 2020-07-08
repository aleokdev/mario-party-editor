namespace MarioPartyEditor
{
    public static class EditorData
    {
        static string _gamePath = null;
        public static string GamePath
        {
            get => _gamePath;
            set
            {
                OnGamePathChange.Invoke(_gamePath, value);
                _gamePath = value;
            }
        }

        public delegate void OnGamePathChangeHandler(string pathBefore, string pathAfter);
        public static event OnGamePathChangeHandler OnGamePathChange;
    }
}
