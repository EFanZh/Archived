using System;

namespace ImgProc
{
    internal class AsyncEventArgs : EventArgs
    {
        public AsyncEventArgs(object userState)
        {
            UserState = userState;
        }

        public object UserState
        {
            get;
            set;
        }
    }
}
