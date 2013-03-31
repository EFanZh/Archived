using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImgProc.Shared
{
    public abstract class InputPlugin : IInputPlugin
    {
        #region IInputPlugin 成员

        public abstract IDictionary<string, string[]> SupportedExtensions
        {
            get;
            protected set;
        }

        public abstract Bitmap GetBitmap(string path);

        #endregion IInputPlugin 成员

        #region IPlugin 成员

        public abstract string Name
        {
            get;
            protected set;
        }

        public abstract Form ConfigForm
        {
            get;
            protected set;
        }

        public abstract string AboutInfo
        {
            get;
            protected set;
        }

        #endregion IPlugin 成员

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable 成员

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ConfigForm != null)
                {
                    ConfigForm.Dispose();
                }
            }
        }
    }
}
