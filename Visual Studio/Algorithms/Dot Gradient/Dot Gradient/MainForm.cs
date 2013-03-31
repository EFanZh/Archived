using System;
using System.Drawing;
using System.Windows.Forms;

namespace DotGradient
{
    public partial class MainForm : Form
    {
        private ResultForm result_form = new ResultForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialogMain.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialogMain.Color;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double x, y;
            byte alpha;

            if (double.TryParse(textBoxX.Text, out x) && double.TryParse(textBoxY.Text, out y) && byte.TryParse(textBoxAlpha.Text, out alpha))
            {
                listViewData.BeginUpdate();
                var lvi = listViewData.Items.Add(x.ToString());
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(y.ToString());
                lvi.SubItems.Add(buttonColor.BackColor.ToString()).BackColor = buttonColor.BackColor;
                lvi.SubItems.Add(alpha.ToString());
                listViewData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewData.EndUpdate();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            listViewData.BeginUpdate();
            while (listViewData.SelectedItems.Count > 0)
            {
                listViewData.SelectedItems[0].Remove();
            }
            listViewData.EndUpdate();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int width, height;
            if (int.TryParse(textBoxWidth.Text, out width) && int.TryParse(textBoxHeight.Text, out height))
            {
                if (width > 0 && height > 0 && width <= 10000 && height < 10000)
                {
                    int data_count = listViewData.Items.Count;
                    var data = new Tuple<Tuple<double, double>, Color>[data_count];
                    for (int i = 0; i < data_count; i++)
                    {
                        var subitems = listViewData.Items[i].SubItems;
                        data[i] = new Tuple<Tuple<double, double>, Color>(new Tuple<double, double>(double.Parse(subitems[0].Text), double.Parse(subitems[1].Text)), Color.FromArgb(int.Parse(subitems[3].Text), subitems[2].BackColor));
                    }
                    result_form.Image = MyGradientGenerator.GenerateGradient(width, height, data);
                    result_form.ShowDialog();
                }
            }
        }
    }
}
