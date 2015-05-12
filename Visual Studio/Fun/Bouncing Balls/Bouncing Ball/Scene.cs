using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace BouncingBall
{
    internal class Scene : INotifyPropertyChanged
    {
        private double width;
        private double height;
        private double gravitationalAcceleration;
        private double ballSize;
        private double ballInitialLocationX;
        private double ballInitialLocationY;
        private double ballInitialSpeedX;
        private double ballInitialSpeedY;
        private double ballX;
        private double ballY;
        private double time;

        private Func<double, double> xFunc;
        private Func<double, double> yFunc;
        private readonly FixedSizeQueue<KeyValuePair<int, double>> afterimageListX = new FixedSizeQueue<KeyValuePair<int, double>>();
        private readonly FixedSizeQueue<KeyValuePair<int, double>> afterimageListY = new FixedSizeQueue<KeyValuePair<int, double>>();
        private int afterimageCount;
        private double afterimageInterval;
        private int lastAfterimage = 0;

        public Scene()
        {
            Background = new SolidColorBrush(Colors.Beige);
            GravitationalAcceleration = 10.0;
            BallSize = 64;
            BallBrush = new SolidColorBrush(Colors.SkyBlue);
            BallStrokeThickness = 2.0;
            BallStroke = new SolidColorBrush(Colors.DarkCyan);
            BallInitialLocationX = 0.0;
            BallInitialLocationY = 800.0;
            BallInitialSpeedX = 40.0;
            BallInitialSpeedY = 0;
            AfterimageCount = 60;
            AfterimageInterval = 0.25;
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;

                UpdateXFunc();
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;

                UpdateYFunc();
            }
        }

        public Brush Background
        {
            get;
            set;
        }

        public double GravitationalAcceleration
        {
            get
            {
                return gravitationalAcceleration;
            }
            set
            {
                gravitationalAcceleration = value;

                UpdateYFunc();
            }
        }

        public int AfterimageCount
        {
            get
            {
                return afterimageCount;
            }
            set
            {
                afterimageCount = value;

                UpdateAfterimageX();
                UpdateAfterimageY();
            }
        }

        public double AfterimageInterval
        {
            get
            {
                return afterimageInterval;
            }
            set
            {
                afterimageInterval = value;

                UpdateAfterimageX();
                UpdateAfterimageY();
            }
        }

        public double BallSize
        {
            get
            {
                return ballSize;
            }
            set
            {
                ballSize = value;

                UpdateXFunc();
                UpdateYFunc();
            }
        }

        public Brush BallBrush
        {
            get;
            set;
        }

        public double BallStrokeThickness
        {
            get;
            set;
        }

        public Brush BallStroke
        {
            get;
            set;
        }

        public double BallInitialLocationX
        {
            get
            {
                return ballInitialLocationX;
            }
            set
            {
                ballInitialLocationX = value;

                UpdateXFunc();
            }
        }

        public double BallInitialLocationY
        {
            get
            {
                return ballInitialLocationY;
            }
            set
            {
                ballInitialLocationY = value;

                UpdateYFunc();
            }
        }

        public double BallInitialSpeedX
        {
            get
            {
                return ballInitialSpeedX;
            }
            set
            {
                ballInitialSpeedX = value;

                UpdateYFunc();
            }
        }

        public double BallInitialSpeedY
        {
            get
            {
                return ballInitialSpeedY;
            }
            set
            {
                ballInitialSpeedY = value;

                UpdateYFunc();
            }
        }

        public double BallX
        {
            get
            {
                return ballX;
            }
            private set
            {
                ballX = value;

                OnPropertyChanged();
            }
        }

        public double BallY
        {
            get
            {
                return ballY;
            }
            private set
            {
                ballY = value;

                OnPropertyChanged();
            }
        }

        public IEnumerable<KeyValuePair<int, Point>> Afterimages
        {
            get
            {
                return afterimageListX.Zip(afterimageListY, (x, y) => new KeyValuePair<int, Point>(x.Key, new Point(x.Value, y.Value)));
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;

                UpdateAfterimageX();
                BallX = xFunc(Time);

                UpdateAfterimageY();
                BallY = yFunc(Time);

                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateXFunc()
        {
            double halfP = Width - BallSize;
            double p = halfP * 2.0;

            xFunc = t =>
            {
                double x = PositiveMod(BallInitialLocationX + BallInitialSpeedX * t, p);

                if (x > halfP)
                {
                    x = p - x;
                }

                return x;
            };

            BallX = xFunc(Time);
        }

        private void UpdateYFunc()
        {
            var e0 = GravitationalAcceleration * BallInitialLocationY + BallInitialSpeedY * BallInitialSpeedY * 0.5;
            var eTop = GravitationalAcceleration * (Height - BallSize);
            var minV = e0 < eTop ? 0.0 : Math.Sqrt((e0 - eTop) * 2.0);
            var maxV = Math.Sqrt(e0 * 2.0);
            var offset = (BallInitialSpeedY + maxV) / GravitationalAcceleration; // (v0 - (-maxV)) / g
            var p = (maxV - minV) * 2.0 / GravitationalAcceleration;

            yFunc = t =>
            {
                var x = (t + offset) % p - p * 0.5;
                var v = x * GravitationalAcceleration + (x < 0 ? -minV : minV);

                return (e0 - v * v * 0.5) / GravitationalAcceleration;
            };

            BallY = yFunc(Time);
        }

        private void UpdateAfterimageX()
        {
            UpdateAfterimage(afterimageListX, xFunc);
        }

        private void UpdateAfterimageY()
        {
            UpdateAfterimage(afterimageListY, yFunc);
        }

        private void UpdateAfterimage(FixedSizeQueue<KeyValuePair<int, double>> afterimages, Func<double, double> func)
        {
            afterimages.MaxCount = AfterimageCount;

            int n = (int)Math.Floor(Time / afterimageInterval);

            for (int i = lastAfterimage + 1; i <= n; i++)
            {
                afterimages.Enqueue(new KeyValuePair<int, double>(i, func(afterimageInterval * i)));
            }
        }

        private static double PositiveMod(double x, double y)
        {
            return (x % y + y) % y;
        }
    }
}
