namespace ImgProc
{
    internal class ProcessStateChangedEventArgs : AsyncEventArgs
    {
        public ProcessStateChangedEventArgs(object userState, string item, string state)
            : base(userState)
        {
            Item = item;
            State = state;
        }

        public string Item
        {
            get;
            private set;
        }

        public string State
        {
            get;
            private set;
        }
    }
}
