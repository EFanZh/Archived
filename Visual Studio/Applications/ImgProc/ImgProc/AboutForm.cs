using System.Reflection;
using System.Windows.Forms;

namespace ImgProc
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.Text = string.Format(this.Text, Application.ProductName);
            labelInfo.Text = string.Format(labelInfo.Text, Application.ProductName, Application.ProductVersion, (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute).Copyright);
        }
    }
}
