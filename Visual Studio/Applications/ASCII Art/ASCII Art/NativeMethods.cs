using System;
using System.Runtime.InteropServices;

namespace ASCIIArt
{
    internal class NativeMethods
    {
        public const Int32 LF_FACESIZE = 32;
        public const Int32 LF_FULLFACESIZE = 64;
        public const Int32 DEFAULT_CHARSET = 1;
        public const Int32 FIXED_PITCH = 1;
        public const Int32 TRUETYPE_FONTTYPE = 0x0004;

        public delegate Int32 FONTENUMPROC(ref ENUMLOGFONT lpelf, ref NEWTEXTMETRIC lpntm, UInt32 FontType, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LOGFONT
        {
            public Int32 lfHeight;
            public Int32 lfWidth;
            public Int32 lfEscapement;
            public Int32 lfOrientation;
            public Int32 lfWeight;
            public Byte lfItalic;
            public Byte lfUnderline;
            public Byte lfStrikeOut;
            public Byte lfCharSet;
            public Byte lfOutPrecision;
            public Byte lfClipPrecision;
            public Byte lfQuality;
            public Byte lfPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public String lfFaceName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TEXTMETRIC
        {
            public Int32 tmHeight;
            public Int32 tmAscent;
            public Int32 tmDescent;
            public Int32 tmInternalLeading;
            public Int32 tmExternalLeading;
            public Int32 tmAveCharWidth;
            public Int32 tmMaxCharWidth;
            public Int32 tmWeight;
            public Int32 tmOverhang;
            public Int32 tmDigitizedAspectX;
            public Int32 tmDigitizedAspectY;
            public Char tmFirstChar;
            public Char tmLastChar;
            public Char tmDefaultChar;
            public Char tmBreakChar;
            public Byte tmItalic;
            public Byte tmUnderlined;
            public Byte tmStruckOut;
            public Byte tmPitchAndFamily;
            public Byte tmCharSet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ENUMLOGFONT
        {
            public LOGFONT elfLogFont;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FULLFACESIZE)]
            public String elfFullName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public String elfStyle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NEWTEXTMETRIC
        {
            public Int32 tmHeight;
            public Int32 tmAscent;
            public Int32 tmDescent;
            public Int32 tmInternalLeading;
            public Int32 tmExternalLeading;
            public Int32 tmAveCharWidth;
            public Int32 tmMaxCharWidth;
            public Int32 tmWeight;
            public Int32 tmOverhang;
            public Int32 tmDigitizedAspectX;
            public Int32 tmDigitizedAspectY;
            public Char tmFirstChar;
            public Char tmLastChar;
            public Char tmDefaultChar;
            public Char tmBreakChar;
            public Byte tmItalic;
            public Byte tmUnderlined;
            public Byte tmStruckOut;
            public Byte tmPitchAndFamily;
            public Byte tmCharSet;
            public UInt32 ntmFlags;
            public UInt32 ntmSizeEM;
            public UInt32 ntmCellHeight;
            public UInt32 ntmAvgWidth;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SIZE
        {
            public UInt32 cx;
            public UInt32 cy;
        }

        [DllImport("gdi32.dll")]
        public extern static IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("gdi32.dll")]
        public extern static Boolean DeleteDC([In] IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public extern static Int32 EnumFontFamiliesEx([In] IntPtr hdc, [In] ref LOGFONT lpLogfont, [In] FONTENUMPROC lpEnumFontFamExProc, [In] IntPtr lParam, UInt32 dwFlags);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public extern static Boolean GetTextExtentPoint32([In] IntPtr hdc, [In] String lpString, [In] Int32 c, [Out] out SIZE lpSize);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public extern static Boolean GetTextMetrics([In] IntPtr hdc, [Out] out TEXTMETRIC lptm);

        [DllImport("gdi32.dll")]
        public extern static IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);
    }
}
