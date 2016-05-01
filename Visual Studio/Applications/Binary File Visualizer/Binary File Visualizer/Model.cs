using System;
using System.ComponentModel;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BinaryFileVisualizer
{
    public class Model : INotifyPropertyChanged
    {
        private struct ByteColor
        {
            public byte Red
            {
                get;
                set;
            }

            public byte Green
            {
                get;
                set;
            }

            public byte Blue
            {
                get;
                set;
            }
        }

        private string filePath;
        private IntSize viewSize;
        private WriteableBitmap bitmap;
        private long scrollPosition;
        private long fileLength;
        private MemoryMappedViewAccessor fileAccessor;

        public event PropertyChangedEventHandler PropertyChanged;

        public Model(string filePath)
        {
            this.filePath = filePath;

            CreateFileAccessor();
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;

                CreateFileAccessor();

                var lineBytes = 3 * viewSize.Width;

                ScrollMaximum = (fileLength + (lineBytes - 1)) / lineBytes - 1;
                scrollPosition = 0;

                CreateBitmap();
                DrawBitmap();

                OnPropertyChanged("ScrollMaximum");
                OnPropertyChanged("ScrollPosition");
                OnPropertyChanged("ViewContent");
            }
        }

        public Size ViewSize
        {
            get
            {
                return viewSize.ToSize();
            }
            set
            {
                viewSize = IntSize.FromSize(value);

                var lineBytes = 3 * viewSize.Width;
                var absolutePosition = lineBytes * ScrollPosition;

                ScrollMaximum = (fileLength + (lineBytes - 1)) / lineBytes - 1;
                ScrollViewportSize = viewSize.Height;
                scrollPosition = Math.Min(ScrollMaximum, absolutePosition / lineBytes);

                CreateBitmap();
                DrawBitmap();

                OnPropertyChanged("ScrollMaximum");
                OnPropertyChanged("ScrollViewportSize");
                OnPropertyChanged("ScrollPosition");
                OnPropertyChanged("ViewContent");
            }
        }

        public ImageSource ViewContent
        {
            get
            {
                return bitmap;
            }
        }

        public long ScrollMaximum
        {
            get;
            private set;
        }

        public long ScrollViewportSize
        {
            get;
            private set;
        }

        public long ScrollPosition
        {
            get
            {
                return scrollPosition;
            }
            set
            {
                scrollPosition = Math.Max(0, Math.Min(value, ScrollMaximum));

                DrawBitmap();

                OnPropertyChanged("ViewContent");

                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateFileAccessor()
        {
            var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            fileLength = fileStream.Length;

            fileAccessor = MemoryMappedFile.CreateFromFile(fileStream, null, 0, MemoryMappedFileAccess.Read, HandleInheritability.None, false).CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
        }

        private void CreateBitmap()
        {
            bitmap = new WriteableBitmap(viewSize.Width, viewSize.Height, 96.0, 96.0, PixelFormats.Pbgra32, null);
        }

        private void DrawBitmap()
        {
            bitmap.Lock();

            var bitmapLinePosition = bitmap.BackBuffer;
            var bitmapStride = bitmap.BackBufferStride;
            var bitmapWidth = bitmap.Width;
            var fileLineBytes = 3L * viewSize.Width;
            var filePosition = fileLineBytes * scrollPosition;
            long remainLineBytes;
            var bitmapFullLines = Math.Min(bitmap.Height, Math.DivRem(fileLength, fileLineBytes, out remainLineBytes) - scrollPosition);

            for (var y = 0; y < bitmapFullLines; ++y)
            {
                var bitmapOffset = 0;

                for (var x = 0; x < bitmapWidth; ++x)
                {
                    DrawPixel(bitmapLinePosition, bitmapOffset, filePosition);

                    bitmapOffset += 4;
                    filePosition += 3;
                }

                bitmapLinePosition += bitmapStride;
            }

            if (bitmapFullLines < bitmap.Height && remainLineBytes > 0)
            {
                long remainBytes;
                var fullPixels = Math.DivRem(remainLineBytes, 3, out remainBytes);
                var bitmapOffset = 0;

                for (var x = 0; x < fullPixels; ++x)
                {
                    DrawPixel(bitmapLinePosition, bitmapOffset, filePosition);

                    bitmapOffset += 4;
                    filePosition += 3;
                }

                if (remainBytes == 1)
                {
                    byte r;

                    fileAccessor.Read(filePosition, out r);

                    WriteColor(bitmapLinePosition, bitmapOffset, r, 0, 0, byte.MaxValue / 3);
                }
                else if (remainBytes == 2)
                {
                    byte r, g;

                    fileAccessor.Read(filePosition, out r);
                    fileAccessor.Read(filePosition + 1, out g);

                    WriteColor(bitmapLinePosition, bitmapOffset, r, g, 0, byte.MaxValue / 3 * 2);
                }

                // Set remain pixels to zeroes.
                var bitmapBytesEnd = bitmap.BackBuffer + bitmapStride * bitmap.PixelHeight;

                for (var p = bitmapLinePosition + bitmapOffset + 4; p != bitmapBytesEnd; p += 4)
                {
                    WriteColor(p, 0, 0, 0, 0, 0);
                }
            }

            bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));

            bitmap.Unlock();
        }

        private void DrawPixel(IntPtr bitmapLine, int offset, long filePosition)
        {
            ByteColor color;

            fileAccessor.Read(filePosition, out color);

            WriteColor(bitmapLine, offset, color.Red, color.Green, color.Blue, byte.MaxValue);
        }

        private static void WriteColor(IntPtr position, int offset, byte red, byte green, byte blue, byte alpha)
        {
            Marshal.WriteByte(position, offset, blue);
            Marshal.WriteByte(position, offset + 1, green);
            Marshal.WriteByte(position, offset + 2, red);
            Marshal.WriteByte(position, offset + 3, alpha);
        }

        private static long IntegerCeiling(long x, long y)
        {
            return (x + y - 1) / y;
        }
    }
}
