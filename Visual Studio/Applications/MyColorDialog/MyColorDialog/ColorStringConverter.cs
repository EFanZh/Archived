using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyColorDialog
{
    internal class ColorStringConverter
    {
        public static Color HexToColor(string strHex)
        {
            string strB, strG, strR;
            int b, g, r;

            if (strHex.StartsWith("#")) strHex = strHex.Substring(1);

            if (strHex.Length == 1)
            {
                strR = strG = strB = strHex.Substring(0, 1) + strHex.Substring(0, 1);
            }
            else if (strHex.Length == 2)
            {
                strR = strG = strB = strHex.Substring(0, 2);
            }
            else if (strHex.Length == 3)
            {
                strR = strHex.Substring(0, 1) + strHex.Substring(0, 1);
                strG = strHex.Substring(1, 1) + strHex.Substring(1, 1);
                strB = strHex.Substring(2, 1) + strHex.Substring(2, 1);
            }
            else if (strHex.Length >= 6)
            {
                strR = strHex.Substring(0, 2);
                strG = strHex.Substring(2, 2);
                strB = strHex.Substring(4, 2);
            }
            else
            {
                return (Color.Empty);
            }
            try
            {
                b = Convert.ToInt32(strB, 16);
                g = Convert.ToInt32(strG, 16);
                r = Convert.ToInt32(strR, 16);
                return (Color.FromArgb(r, g, b));
            }
            catch (Exception)
            {
                return (Color.Empty);
            }
        }

        public static string ColorToHex(Color color)
        {
            return (string.Format("#{0,2:x}{1,2:x}{2,2:x}", color.R, color.G, color.B).Replace(' ', '0'));
        }
    }
}
