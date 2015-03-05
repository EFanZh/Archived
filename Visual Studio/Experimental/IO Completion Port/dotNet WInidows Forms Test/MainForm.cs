using System;
using System.Threading;
using System.Windows.Forms;

namespace dotNetWInidowsFormsTest
{
    public partial class MainForm : Form
    {
        private IntPtr iocp;

        private static Random random = new Random();

        public MainForm()
        {
            InitializeComponent();

            iocp = NativeMethods.CreateIoCompletionPort(NativeMethods.INVALID_HANDLE_VALUE, IntPtr.Zero, IntPtr.Zero, 0);

            for (int i = 0; i < Environment.ProcessorCount * 2; i++)
            {
                ThreadPool.QueueUserWorkItem(WorkThread);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Thread.Sleep(10);
                    NativeMethods.PostQueuedCompletionStatus(iocp, 0, IntPtr.Zero, new IntPtr(random.Next()));
                }
            });
        }

        private void WorkThread(object state)
        {
            uint ignoredUInt;
            IntPtr ignoredIntPtr;
            IntPtr data;

            while (NativeMethods.GetQueuedCompletionStatus(iocp, out ignoredUInt, out ignoredIntPtr, out data, NativeMethods.INFINITE))
            {
                int k = data.ToInt32();
                this.Invoke(new Action(() => textBoxLog.AppendText(string.Format("{0} is {1}.\r\n", k, k % 2 == 0 ? "even" : "odd"))));
            }
        }
    }
}
