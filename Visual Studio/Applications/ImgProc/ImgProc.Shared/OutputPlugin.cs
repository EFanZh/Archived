using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImgProc.Shared
{
    public abstract class OutputPlugin : IOutputPlugin
    {
        #region IOutputPlugin 成员

        public abstract string TypeName
        {
            get;
            protected set;
        }

        public abstract string SelectedExtension
        {
            get;
            protected set;
        }

        public abstract void Output(Bitmap bitmap, string path);

        #endregion IOutputPlugin 成员

        #region IPlugin 成员

        public abstract string Name
        {
            get;
            protected set;
        }

        public abstract string AboutInfo
        {
            get;
            protected set;
        }

        public abstract Form ConfigForm
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
