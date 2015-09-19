using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ColorPicker
{
    internal class ChannelValuesController : GroupBox
    {
        public event EventHandler<ChannelValueChangedEventArgs> ChannelValueChanged;

        public event EventHandler<ControlChannelChangedEventArgs> ControlChannelChanged;

        public ChannelValuesController(ColorSpace colorSpace)
        {
            ColorSpace = colorSpace;
            this.Header = colorSpace.Name;
            this.Content = CreateInnerContainer();
        }

        public ColorSpace ColorSpace
        {
            get;
        }

        public decimal[] Values
        {
            get
            {
                return ((Panel)this.Content).Children
                                            .Cast<UIElement>()
                                            .Where((e, i) => i % 2 != 0)
                                            .Cast<TextBox>()
                                            .Select(t => decimal.Parse(t.Text))
                                            .ToArray();
            }
        }

        private UIElement CreateInnerContainer()
        {
            Grid result = new Grid();

            result.ColumnDefinitions.Add(new ColumnDefinition { SharedSizeGroup = "ChannelNameColumn", Width = GridLength.Auto });
            result.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < ColorSpace.Channels.Count; i++)
            {
                result.RowDefinitions.Add(new RowDefinition());
                result.Children.Add(CreateChannelName(i));
                result.Children.Add(CreateChannelValue(i));
            }

            return result;
        }

        private UIElement CreateChannelName(int i)
        {
            ContentControl result = ColorSpace.Channels.Count == 3 ? CreateRadioButton(i) : new Label();

            result.Content = ColorSpace.Channels[i].Name + ':';
            result.SetValue(Grid.RowProperty, i);
            result.SetValue(Grid.ColumnProperty, 0);

            return result;
        }

        private ContentControl CreateRadioButton(int i)
        {
            RadioButton result = new RadioButton();

            result.Checked += (sender, args) =>
            {
                ControlChannelChanged?.Invoke(this, new ControlChannelChangedEventArgs());
            };

            return result;
        }

        private UIElement CreateChannelValue(int i)
        {
            TextBox result = new TextBox();

            result.SetValue(Grid.RowProperty, i);
            result.SetValue(Grid.ColumnProperty, 1);
            result.TextChanged += (sender, args) =>
            {
                ChannelValueChanged?.Invoke(this, new ChannelValueChangedEventArgs());
            };

            return result;
        }
    }
}
