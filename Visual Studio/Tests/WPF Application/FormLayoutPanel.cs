using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFApplication
{
    internal class FormLayoutPanel : Panel
    {
        private double label_width;
        private readonly List<Double> row_heights = new List<double>();

        public static readonly DependencyProperty column_spacing_property = DependencyProperty.Register("ColumnSpacing", typeof(double), typeof(FormLayoutPanel), new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty row_spacing_property = DependencyProperty.Register("RowSpacing", typeof(double), typeof(FormLayoutPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty padding_property = DependencyProperty.Register("Padding", typeof(Thickness), typeof(FormLayoutPanel), new FrameworkPropertyMetadata(new Thickness(), FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(padding_property);
            }
            set
            {
                SetValue(padding_property, value);
            }
        }

        public double ColumnSpacing
        {
            get
            {
                return (double)GetValue(column_spacing_property);
            }
            set
            {
                this.SetValue(column_spacing_property, value);
            }
        }

        public double RowSpacing
        {
            get
            {
                return (double)GetValue(row_spacing_property);
            }
            set
            {
                this.SetValue(row_spacing_property, value);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int row_count = this.InternalChildren.Count / 2;
            double field_left = Padding.Left + label_width + ColumnSpacing;
            double field_width = finalSize.Width - Padding.Right - field_left;
            for (int i = 0; i < row_count; i++)
            {
                double top = Padding.Top + row_heights.Take(i).Sum() + RowSpacing * i;
                double row_height = row_heights[i];
                this.InternalChildren[i * 2].Arrange(new Rect(Padding.Left, top, label_width, row_height));
                this.InternalChildren[i * 2 + 1].Arrange(new Rect(field_left, top, field_width, row_height));
            }

            return new Size(finalSize.Width, row_heights.Sum() + RowSpacing * (row_count - 1) + Padding.Top + Padding.Bottom);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            label_width = double.MinValue;
            double field_width = double.MinValue;
            row_heights.Clear();

            for (int i = 0; i < this.InternalChildren.Count; i += 2)
            {
                var label = this.InternalChildren[i];

                label.Measure(availableSize);
                if (label.DesiredSize.Width > label_width)
                {
                    label_width = label.DesiredSize.Width;
                }

                var field = this.InternalChildren[i + 1];
                field.Measure(availableSize);
                if (field.DesiredSize.Width > field_width)
                {
                    field_width = field.DesiredSize.Width;
                }
                row_heights.Add(Math.Max(label.DesiredSize.Height, field.DesiredSize.Height));
            }

            return new Size(label_width + field_width + ColumnSpacing + Padding.Left + Padding.Right, row_heights.Sum() + RowSpacing * (row_heights.Count - 1) + Padding.Top + Padding.Bottom);
        }
    }
}
