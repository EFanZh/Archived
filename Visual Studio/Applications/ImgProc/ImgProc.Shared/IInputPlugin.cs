using System.Collections.Generic;
using System.Drawing;

namespace ImgProc.Shared
{
    public interface IInputPlugin : IPlugin
    {
        IDictionary<string, string[]> SupportedExtensions
        {
            get;
        }

        Bitmap GetBitmap(string path);
    }
}
