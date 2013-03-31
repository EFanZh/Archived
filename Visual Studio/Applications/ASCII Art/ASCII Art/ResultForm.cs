using System.Drawing;
using System.Windows.Forms;

namespace ASCIIArt
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public string ResultText
        {
            get
            {
                return textBoxResult.Text;
            }
            set
            {
                textBoxResult.Text = value;
            }
        }

        public Font ResultFont
        {
            get
            {
                return textBoxResult.Font;
            }
            set
            {
                textBoxResult.Font = value;
            }
        }
    }
}
