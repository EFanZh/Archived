using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace PrimeNumberCalculator
{
    public partial class MainForm : Form
    {
        private Calculator calculator = new Calculator();
        private Stopwatch stop_watch_update = new Stopwatch();
        private Stopwatch stop_watch_total = new Stopwatch();
        private int interval = 32;

        public MainForm()
        {
            InitializeComponent();

            PropertyInfo pi = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(listViewPrimeNumberList, true);

            calculator.CalculateCompleted += (sender, e) =>
            {
                buttonCancel.Enabled = false;
                stop_watch_total.Stop();
                buttonCalculate.Enabled = true;
            };

            calculator.OutputResult += (sender, e) =>
            {
                if (stop_watch_update.ElapsedMilliseconds > interval)
                {
                    stop_watch_update.Restart();

                    bool lock_taken = false;
                    calculator.BeginAccessData(ref lock_taken);
                    int count = calculator.PrimeNumberList.Count;
                    calculator.EndAccessData();

                    listViewPrimeNumberList.BeginUpdate();

                    // There may be some bugs.
                    // See http://social.msdn.microsoft.com/Forums/en/winforms/thread/f24ffbc5-59f0-4f18-800a-ff2fbbe418e0
                    try
                    {
                        listViewPrimeNumberList.VirtualListSize = count;
                    }
                    catch (NullReferenceException)
                    {
                    }
                    if (checkBoxScrollToBottom.Checked)
                    {
                        listViewPrimeNumberList.Items[listViewPrimeNumberList.Items.Count - 1].EnsureVisible();
                    }
                    listViewPrimeNumberList.EndUpdate();
                    labelInfo.Text = string.Format("Count: {0}, Time: {1}, Rate: {2}", listViewPrimeNumberList.VirtualListSize, stop_watch_total.Elapsed, 1000.0 * listViewPrimeNumberList.VirtualListSize / stop_watch_total.ElapsedMilliseconds);
                }
            };
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            buttonCalculate.Enabled = false;
            stop_watch_update.Start();
            stop_watch_total.Start();
            calculator.CalculateAsync();
            buttonCancel.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            calculator.CancelCalculate();
        }

        private void listViewPrimeNumberList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(e.ItemIndex.ToString());

            bool lock_taken = false;
            calculator.BeginAccessData(ref lock_taken);
            e.Item.SubItems.Add(calculator.PrimeNumberList[e.ItemIndex].ToString());
            calculator.EndAccessData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (calculator.AsyncResult != null && !calculator.AsyncResult.IsCompleted)
            {
                calculator.CancelCalculate();
                calculator.AsyncResult.AsyncWaitHandle.WaitOne();
            }
        }
    }
}
