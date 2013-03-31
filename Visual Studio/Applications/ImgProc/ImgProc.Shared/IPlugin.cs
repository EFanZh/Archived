using System;
using System.Windows.Forms;

namespace ImgProc.Shared
{
    public interface IPlugin : IDisposable
    {
        string Name
        {
            get;
        }

        Form ConfigForm
        {
            get;
        }

        string AboutInfo
        {
            get;
        }
    }
}
