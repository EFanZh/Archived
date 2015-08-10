using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tuple<int, int, double> currentTask;
        private Tuple<int, int, double> queuedTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            QueueCurrentTask();
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size canvasSize = MainCanvas.RenderSize;

            if (canvasSize.Width > 0 && canvasSize.Height > 0)
            {
                QueueCurrentTask();
            }
        }

        private void QueueCurrentTask()
        {
            Size canvasSize = MainCanvas.RenderSize;
            Matrix matrix = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
            int width = (int)(canvasSize.Width * matrix.M11); // Get horizontal pixel count.
            int height = (int)(canvasSize.Height * matrix.M22); // Get vertical pixel count.

            QueueTask(Tuple.Create(width, height, MainSlider.Value / (MainSlider.Maximum - MainSlider.Minimum)));
        }

        private async void QueueTask(Tuple<int, int, double> task)
        {
            queuedTask = task;

            if (currentTask == null)
            {
                while (queuedTask != null)
                {
                    currentTask = queuedTask;
                    queuedTask = null;

                    await ProcessTask(currentTask);

                    currentTask = null;
                }
            }
        }

        private async Task ProcessTask(Tuple<int, int, double> task)
        {
            int width = task.Item1;
            int height = task.Item2;
            double k = task.Item3;
            WriteableBitmap bitmap = (WriteableBitmap)MainImage.Source;

            // Create new bitmap if current bit is invalid or obsolete.
            if (bitmap == null || bitmap.PixelWidth != width || bitmap.PixelWidth != height)
            {
                bitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Bgr32, null);
            }

            ColorContext cc = new ColorContext(PixelFormats.Bgr32);

            this.Title = cc.ProfileUri.ToString();

            bitmap.Lock();

            IntPtr buffer = bitmap.BackBuffer;
            int stride = bitmap.BackBufferStride;

            // Generate the content of the bitmap.
            await Task.Run(() => FillBitmap(buffer, width, height, stride, k));

            bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            MainImage.Source = bitmap;
        }

        private static void FillBitmap(IntPtr buffer, int width, int height, int stride, double k)
        {
            int rowStart = 0;

            for (int y = 0; y < height; y++)
            {
                int columnStart = rowStart;

                for (int x = 0; x < width; x++)
                {
                    byte v1 = (byte)(x * byte.MaxValue / width);
                    byte v2 = (byte)(y * byte.MaxValue / height);
                    byte v3 = (byte)(byte.MaxValue * k);

                    Marshal.WriteByte(buffer, columnStart, v1);
                    Marshal.WriteByte(buffer, columnStart + 1, v2);
                    Marshal.WriteByte(buffer, columnStart + 2, v3);

                    columnStart += 4;
                }
                rowStart += stride;
            }

            Thread.Sleep(100);
        }
    }
}
