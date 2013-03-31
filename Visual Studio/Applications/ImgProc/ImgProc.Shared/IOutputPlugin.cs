using System.Drawing;

namespace ImgProc.Shared
{
    public interface IOutputPlugin : IPlugin
    {
        string TypeName
        {
            get;
        }

        string SelectedExtension
        {
            get;
        }

        void Output(Bitmap bitmap, string path);
    }
}
