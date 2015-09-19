using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ColorPicker
{
    public struct FastColorTransformMatrix
    {
        public FastColorTransformMatrix(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public double M11
        {
            get;
            set;
        }

        public double M12
        {
            get;
            set;
        }

        public double M13
        {
            get;
            set;
        }

        public double M21
        {
            get;
            set;
        }

        public double M22
        {
            get;
            set;
        }

        public double M23
        {
            get;
            set;
        }

        public double M31
        {
            get;
            set;
        }

        public double M32
        {
            get;
            set;
        }

        public double M33
        {
            get;
            set;
        }
    }
}
