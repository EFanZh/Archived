using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Another_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double queuedTask = double.NaN;
        private double currentTask = double.NaN;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            QueueTask(InputSlider.Value);
        }

        private async void QueueTask(double task)
        {
            queuedTask = task;

            if (double.IsNaN(currentTask))
            {
                while (!double.IsNaN(queuedTask))
                {
                    currentTask = queuedTask;
                    queuedTask = double.NaN;

                    ResultTextBlock.Text = await ProcessTask(currentTask);

                    currentTask = double.NaN;
                }
            }
        }

        private static async Task<string> ProcessTask(double task)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);
                return task.ToString(CultureInfo.InvariantCulture);
            });
        }
    }
}
