using System.ComponentModel;

namespace ImgProc
{
    internal class ProcessTaskProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public ProcessTaskProgressChangedEventArgs(int progressPercentage, object userState, object taskUserState)
            : base(progressPercentage, userState)
        {
            TaskUserState = taskUserState;
        }

        public object TaskUserState
        {
            get;
            private set;
        }
    }
}
