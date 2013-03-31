using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SocketNS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                tlStrpCmbBxRequestEncoding.Items.Add(ei.Name);
                tlStrpCmbBxRespondEncoding.Items.Add(ei.Name);
            }
            tlStrpCmbBxRequestEncoding.SelectedIndex = tlStrpCmbBxRequestEncoding.Items.Count - 1;
            tlStrpCmbBxRespondEncoding.SelectedIndex = tlStrpCmbBxRespondEncoding.Items.Count - 1;
        }

        private void tlStrpBtnSendRequest_Click(object sender, EventArgs e)
        {
            byte[] buf, msg;
            int count, bufsz;
            NetworkStream ns;
            TcpClient tc;

            try
            {
                tc = new TcpClient(tlStrpTxtBxServer.Text, int.Parse(tlStrpTxtBxPort.Text));
                tc.ReceiveTimeout = 60000;
                ns = tc.GetStream();
                msg = Encoding.GetEncoding(tlStrpCmbBxRequestEncoding.Text).GetBytes(txtBxRequest.Text);
                ns.Write(msg, 0, msg.Length);
                bufsz = 256;
                buf = new byte[bufsz];
                do
                {
                    count = ns.Read(buf, 0, bufsz);
                    txtBxRespond.AppendText(Encoding.GetEncoding(tlStrpCmbBxRespondEncoding.Text).GetString(buf, 0, count));
                } while (count == bufsz);
                txtBxRespond.AppendText("\r\n");
            }
            catch (Exception ex)
            {
                txtBxRespond.AppendText(ex.Message + "\r\n");
            }
        }

        private void tlStrpBtnClearRespond_Click(object sender, EventArgs e)
        {
            txtBxRespond.Clear();
        }
    }
}
