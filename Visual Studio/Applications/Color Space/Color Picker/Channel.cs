using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker
{
    internal class Channel
    {
        public Channel(string name, decimal minValue, decimal maxValue)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal MinValue
        {
            get;
            set;
        }

        public decimal MaxValue
        {
            get;
            set;
        }

        public static Channel CreateStandardChannel(string name)
        {
            return new Channel(name, 0.0m, 1.0m);
        }
    }
}
