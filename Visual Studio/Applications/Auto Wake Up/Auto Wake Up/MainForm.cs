using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AutoWakeUp
{
    public partial class MainForm : Form
    {
        private Control[] controls;

        public MainForm()
        {
            InitializeComponent();

            controls = new Control[] { dateTimePickerWakeUpTime, textBoxRunProgram, buttonSelectProgram, textBoxParameter, dateTimePickerRestTime, comboBoxOperation, buttonGoToSleepAndWait };

            if (ConfigManager.LoadConfig())
            {
                IgnoreExceptionDo(() => { dateTimePickerWakeUpTime.Value = ConfigManager.WakeUpTime; });
                IgnoreExceptionDo(() => { textBoxRunProgram.Text = ConfigManager.RunProgram; });
                IgnoreExceptionDo(() => { textBoxParameter.Text = ConfigManager.RunProgramParameter; });
                IgnoreExceptionDo(() => { dateTimePickerRestTime.Value = ConfigManager.WaitUntilTime; });
                IgnoreExceptionDo(() => { comboBoxOperation.Text = ConfigManager.DoStuff; });
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigManager.WakeUpTime = dateTimePickerWakeUpTime.Value;
            ConfigManager.RunProgram = textBoxRunProgram.Text;
            ConfigManager.RunProgramParameter = textBoxParameter.Text;
            ConfigManager.WaitUntilTime = dateTimePickerRestTime.Value;
            ConfigManager.DoStuff = comboBoxOperation.Text;
            ConfigManager.SaveConfig();
        }

        private void buttonSelectProgram_Click(object sender, EventArgs e)
        {
            buttonSelectProgram.Enabled = false;
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                textBoxRunProgram.Text = openFileDialogMain.FileName;
            }
            buttonSelectProgram.Enabled = true;
        }

        private void buttonGoToSleepAndWait_Click(object sender, EventArgs e)
        {
            SetControlEnabled(false);

            DateTime now = DateTime.Now;

            DateTime wake_up_time = new DateTime(now.Year, now.Month, now.Day, dateTimePickerWakeUpTime.Value.Hour, dateTimePickerWakeUpTime.Value.Minute, dateTimePickerWakeUpTime.Value.Second, DateTimeKind.Local);
            if (wake_up_time < now)
            {
                wake_up_time = wake_up_time.AddDays(1.0);
            }

            DateTime suspend_time = new DateTime(now.Year, now.Month, now.Day, dateTimePickerRestTime.Value.Hour, dateTimePickerRestTime.Value.Minute, dateTimePickerRestTime.Value.Second, DateTimeKind.Local);
            if (suspend_time < now)
            {
                suspend_time = suspend_time.AddDays(1.0);
            }

            if (suspend_time < wake_up_time)
            {
                MessageBox.Show("Wait Until Time should be later than Wake Up Time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetControlEnabled(true);
                return;
            }

            Func<Action, Action> get_invoker = (action) =>
            {
                return () =>
                {
                    this.BeginInvoke(action);
                };
            };

            SystemActions.WaitThenDoStuff(wake_up_time, get_invoker(new Action(WakeUpStuff)));
            SystemActions.WaitThenDoStuff(suspend_time, get_invoker(new Action(SuspendStuff)));

            SystemActions.SystemSleep();
        }

        private void WakeUpStuff()
        {
            IgnoreExceptionDo(() => { Process.Start(textBoxRunProgram.Text, textBoxParameter.Text); });
        }

        private void SuspendStuff()
        {
            if (string.Equals(comboBoxOperation.Text, "Sleep"))
            {
                SystemActions.SystemSleep();
            }
            else if (string.Equals(comboBoxOperation.Text, "Shutdown"))
            {
                SystemActions.SystemShutdown();
            }
            SetControlEnabled(true);
        }

        private void SetControlEnabled(bool value)
        {
            foreach (var control in controls)
            {
                control.Enabled = value;
            }
        }

        private static void IgnoreExceptionDo(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
    }
}
