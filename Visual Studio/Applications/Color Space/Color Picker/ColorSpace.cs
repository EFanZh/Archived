using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    internal abstract class ColorSpace
    {
        public string Name
        {
            get;
            protected set;
        }

        public IReadOnlyList<Channel> Channels
        {
            get;
            protected set;
        }

        public abstract Color GetColor(params double[] values);
    }
}
