using System;
using System.ComponentModel;
using System.Drawing;

namespace ImgProc.Shared
{
    public class GetBitmapCompletedEventArgs : AsyncCompletedEventArgs
    {
        Bitmap bitmap;

        public GetBitmapCompletedEventArgs(Exception error, bool cancelled, Object userState, Bitmap bitmap)
            : base(error, cancelled, userState)
        {
            this.bitmap = bitmap;
        }

        public Bitmap Bitmap
        {
            get
            {
                RaiseExceptionIfNecessary();
                return bitmap;
            }
        }
    }
}
