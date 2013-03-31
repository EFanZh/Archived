using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ImgProc.Shared
{
    public interface IProcessingPlugin : IPlugin
    {
        void ProcessAsync(Bitmap bitmap, object userState);

        void ProcessAsyncCancel(object userState);

        IEnumerable<string> SetupPath
        {
            get;
        }

        event ProgressChangedEventHandler ProcessProgressChanged;
        event EventHandler<GetBitmapCompletedEventArgs> ProcessCompleted;
    }
}
