using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImgProc
{
    internal partial class ProcessProgressForm : Form
    {
        object userState;

        public event EventHandler<AsyncEventArgs> CancelProcess;

        public ProcessProgressForm(object userState)
        {
            InitializeComponent();

            this.userState = userState;
        }

        public void Initialize()
        {
            listViewImageList.Items.Clear();
            progressBarProgress.Value = progressBarProgress.Minimum;
        }

        public void AddListViewItems(IEnumerable<string> imageList)
        {
            foreach (var item in imageList)
            {
                listViewImageList.Items.Add(item).SubItems.AddRange(new string[] { "队列", "0%" });
            }
            listViewImageList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void ChangeTaskState(string item, string state)
        {
            ListViewItem lvi = FindListViewItem(item);
            if (lvi != null)
            {
                lvi.SubItems[1].Text = state;
            }
        }

        public void ChangeTaskProgress(string item, int progressPercentage)
        {
            ListViewItem lvi = FindListViewItem(item);
            if (lvi != null)
            {
                lvi.SubItems[2].Text = string.Format("{0}%", progressPercentage);
            }
        }

        public void ChangeProgress(double progress)
        {
            progressBarProgress.Value = Utilities.IntRound(progress * progressBarProgress.Maximum);
        }

        public void ProcessProgressCompleted()
        {
            buttonCancel.Text = "关闭";
            buttonCancel.Enabled = true;
        }

        private ListViewItem FindListViewItem(string item)
        {
            foreach (ListViewItem lvi in listViewImageList.Items)
            {
                if (string.Equals(lvi.Text, item))
                {
                    return lvi;
                }
            }
            return null;
        }

        protected virtual void OnCancelProcess(AsyncEventArgs e)
        {
            if (CancelProcess != null)
            {
                CancelProcess(this, e);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (buttonCancel.Text == "关闭")
            {
                this.Close();
            }
            else
            {
                buttonCancel.Enabled = false;
                OnCancelProcess(new AsyncEventArgs(userState));
            }
        }
    }
}
