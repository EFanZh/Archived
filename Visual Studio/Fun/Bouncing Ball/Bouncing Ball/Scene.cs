using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private Func<double> xFunc;
        private Func<double> yFunc;

        public Scene()
        {
            Background = new SolidColorBrush(Colors.Beige);
            GravitationalAcceleration = 100;
            BallSize = 64;
            BallBrush = new SolidColorBrush(Colors.SkyBlue);
            BallInitialLocationX = 1000;
            BallInitialLocationY = 600;
            BallInitialSpeedX = 100;
            BallInitialSpeedY = 0;
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

        public double Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;

                BallX = xFunc();
                BallY = yFunc();

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

            xFunc = () =>
            {
                double x = PositiveMod(BallInitialLocationX + BallInitialSpeedX * Time, p);

                if (x > halfP)
                {
                    x = p - x;
                }

                return x;
            };

            BallX = xFunc();
        }

        private void UpdateYFunc()
        {
            var e0 = GravitationalAcceleration * BallInitialLocationY + BallInitialSpeedY * BallInitialSpeedY * 0.5;
            var eTop = GravitationalAcceleration * (Height - BallSize);
            var minV = e0 < eTop ? 0.0 : Math.Sqrt((e0 - eTop) * 2.0);
            var maxV = Math.Sqrt(e0 * 2.0);
            var offset = (BallInitialSpeedY + maxV) / GravitationalAcceleration; // (v0 - (-maxV)) / g
            var p = (maxV - minV) * 2.0 / GravitationalAcceleration;

            yFunc = () =>
            {
                var x = (Time + offset) % p - p * 0.5;
                var v = x * GravitationalAcceleration + (x < 0 ? -minV : minV);

                return (e0 - v * v * 0.5) / GravitationalAcceleration;
            };

            BallY = yFunc();
        }

        private static double PositiveMod(double x, double y)
        {
            return (x % y + y) % y;
        }
    }
}