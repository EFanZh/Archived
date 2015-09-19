using System.Windows.Media;

namespace ColorPicker
{
    internal class LinearColorSpace : ColorSpace
    {
        public LinearColorSpace(string name, Channel channel1, Channel channel2, Channel channel3)
        {
            this.Name = name;
            this.Channels = new[] { channel1, channel2, channel3 };
        }

        public ColorVector RedPointXyy
        {
            get;
            set;
        }

        public ColorVector GreenPointXyy
        {
            get;
            set;
        }

        public ColorVector BluePointXyy
        {
            get;
            set;
        }

        public ColorVector RedPointXyz
        {
            get
            {
                ColorVector result = RedPointXyy;

                result.XyyToXyz();

                return result;
            }
            set
            {
                value.XyzToXyy();

                RedPointXyy = value;
            }
        }

        public ColorVector GreenPointXyz
        {
            get
            {
                ColorVector result = GreenPointXyy;

                result.XyyToXyz();

                return result;
            }
            set
            {
                value.XyzToXyy();

                GreenPointXyy = value;
            }
        }

        public ColorVector BluePointXyz
        {
            get
            {
                ColorVector result = BluePointXyy;

                result.XyyToXyz();

                return result;
            }
            set
            {
                value.XyzToXyy();

                BluePointXyy = value;
            }
        }

        public ColorVector WhitePointXyz
        {
            get
            {
                ColorVector result = new ColorVector(1.0m, 1.0m, 1.0m);

                result.Transform(CalculateFromXyzTransformMatrix());

                return result;
            }
        }

        public ColorVector WhitePointXyy
        {
            get
            {
                ColorVector result = WhitePointXyz;

                result.XyzToXyy();

                return result;
            }
        }

        public override Color GetColor(params double[] values)
        {
            throw new System.NotImplementedException();
        }

        public ColorTransformMatrix CalculateToXyzTransformMatrix()
        {
            return new ColorTransformMatrix(RedPointXyz.Component1, GreenPointXyz.Component1, BluePointXyz.Component1,
                                            RedPointXyz.Component2, GreenPointXyz.Component2, BluePointXyz.Component2,
                                            RedPointXyz.Component3, GreenPointXyz.Component3, BluePointXyz.Component3);
        }

        public ColorTransformMatrix CalculateFromXyzTransformMatrix()
        {
            decimal m11 = RedPointXyz.Component1;
            decimal m12 = GreenPointXyz.Component1;
            decimal m13 = BluePointXyz.Component1;
            decimal m21 = RedPointXyz.Component2;
            decimal m22 = GreenPointXyz.Component2;
            decimal m23 = BluePointXyz.Component2;
            decimal m31 = RedPointXyz.Component3;
            decimal m32 = GreenPointXyz.Component3;
            decimal m33 = BluePointXyz.Component3;
            decimal newM11Top = m23 * m32 - m22 * m33;
            decimal newM12Top = m13 * m32 - m12 * m33;
            decimal newM13Top = m12 * m23 - m13 * m22;
            decimal newM21Top = m11 * m33 - m13 * m31;
            decimal newM22Top = m13 * m21 - m11 * m23;
            decimal newM23Top = m13 * m21 - m11 * m23;
            decimal newM31Top = m21 * m32 - m22 * m31;
            decimal newM32Top = m12 * m31 - m11 * m32;
            decimal newM33Top = m11 * m22 - m12 * m21;
            decimal det = m13 * newM31Top + m23 * newM32Top + m33 * newM33Top;

            return new ColorTransformMatrix(newM11Top / det, newM12Top / det, newM13Top / det,
                                            newM21Top / det, newM22Top / det, newM23Top / det,
                                            newM31Top / det, newM32Top / det, newM33Top / det);
        }

        public static ColorTransformMatrix CreateColorTransformMatrix(LinearColorSpace from, LinearColorSpace to)
        {
            ColorTransformMatrix matrix = from.CalculateToXyzTransformMatrix();

            matrix.Transform(to.CalculateFromXyzTransformMatrix());

            return matrix;
        }
    }
}
