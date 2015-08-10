using System.Collections.Generic;
using System.Windows.Media;

namespace ColorPicker
{
    internal class Model
    {
        public Model(IEnumerable<ColorContext> profiles)
        {
            Profiles = profiles;
        }

        public int MainAreaWidth
        {
            get;
            set;
        }

        public int MainAreaHeight
        {
            get;
            set;
        }

        public int SelectedChannelWidth
        {
            get;
            set;
        }

        public int SelectedChannelHeight
        {
            get;
            set;
        }

        public bool FilterDeviceColors
        {
            get;
            set;
        }

        public bool FilterTargetColors
        {
            get;
            set;
        }

        public IEnumerable<ColorContext> Profiles
        {
            private set;
            get;
        }

        public ColorContext DeviceColorProfile
        {
            get;
            set;
        }

        public ColorContext TargetColorProfile
        {
            get;
            set;
        }

        public ImageSource MainImageSource
        {
            get;
            set;
        }

        public ImageSource SelectedChannelImageSource
        {
            get;
            set;
        }
    }
}
