using System.Collections.Generic;
using System.Windows.Media;

namespace ColorPicker
{
    internal class Model
    {
        public ColorSpace CurrentColorSpace
        {
            get;
            set;
        }

        public Channel CurrentControlChannel
        {
            get;
            set;
        }

        public double CurrentControlChannelValue
        {
            get;
            set;
        }

        public double CurrentChannelXValue
        {
            get;
            set;
        }

        public double CurrentChannelYValue
        {
            get;
            set;
        }
    }
}
