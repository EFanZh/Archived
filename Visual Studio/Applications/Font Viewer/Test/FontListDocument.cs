using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Test
{
    internal class FontListDocument : FlowDocument
    {
        private static readonly string[] FontFamilyTableHeaderCells = { "Stretch", "Weight", "Style", "Name", "Sample Text" };
        private const string SampleText = "ABCDEFGabcdefg0123456 fiff ft ti";

        public FontListDocument(IEnumerable<FontFamily> fontFamilies)
        {
            this.PageWidth = 21 * 96 * 100 / 254.0;
            this.PageWidth = 297 * 96 * 10 / 254.0;
            this.PagePadding = new Thickness(2 * 96 / 2.54, 2 * 96 / 2.54, 2 * 96 / 2.54, 2 * 96 / 2.54);
            this.ColumnWidth = this.PageWidth - (this.PagePadding.Left + this.PagePadding.Right);
            this.Blocks.AddRange(fontFamilies.Select(GenerateFontFamilySection));
        }

        private static Section GenerateFontFamilySection(FontFamily fontFamily)
        {
            Section section = new Section();

            section.Blocks.Add(new Paragraph(new Run(fontFamily.FamilyNames.Values.First())));
            section.Blocks.Add(GenerateFontFamilyTable(fontFamily));

            return section;
        }

        private static Table GenerateFontFamilyTable(FontFamily fontFamily)
        {
            Table table = new Table()
            {
                TextAlignment = TextAlignment.Center,
                CellSpacing = 0,
                BorderThickness = new Thickness(2, 2, 2, 2),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };

            table.RowGroups.Add(GenerateFontFamilyTableRowGroup());

            TableRowGroup tableRowGroup = new TableRowGroup();

            tableRowGroup.Rows.AddRange(fontFamily.GetTypefaces().Where(t => !(t.IsBoldSimulated || t.IsObliqueSimulated)).GroupBy(t => t.Stretch).Select(GenerateStretchTableRowGroup).SelectMany(r => r));
            table.RowGroups.Add(tableRowGroup);

            return table;
        }

        private static TableRowGroup GenerateFontFamilyTableRowGroup()
        {
            TableRowGroup tableRowGroup = new TableRowGroup();
            TableRow tableRow = new TableRow();

            tableRow.Cells.AddRange(FontFamilyTableHeaderCells.Select(s => new TableCell(new Paragraph(new Run(s)))
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            }));
            tableRowGroup.Rows.Add(tableRow);

            return tableRowGroup;
        }

        private static TableRow[] GenerateStretchTableRowGroup(IGrouping<FontStretch, Typeface> stretch)
        {
            var result = stretch.GroupBy(s => s.Weight).Select(GenerateWeightTableRows).SelectMany(t => t).ToArray();

            result.First().Cells.Insert(0, new TableCell(new Paragraph(new Run(stretch.Key.ToString())))
            {
                RowSpan = stretch.Count(),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            });

            return result;
        }

        private static TableRow[] GenerateWeightTableRows(IGrouping<FontWeight, Typeface> weight)
        {
            var result = weight.GroupBy(s => s.Style).Select(GenerateStyleTableRows).SelectMany(t => t).ToArray();

            result.First().Cells.Insert(0, new TableCell(new Paragraph(new Run(weight.Key.ToString())))
            {
                RowSpan = weight.Count(),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            });

            return result;
        }

        private static TableRow[] GenerateStyleTableRows(IGrouping<FontStyle, Typeface> style)
        {
            var result = style.Select(GenerateTypefaceTableRow).ToArray();

            result.First().Cells.Insert(0, new TableCell(new Paragraph(new Run(style.Key.ToString())))
            {
                RowSpan = style.Count(),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            });

            return result;
        }

        private static TableRow GenerateTypefaceTableRow(Typeface typeface)
        {
            TableRow tableRow = new TableRow();

            tableRow.Cells.Add(new TableCell(new Paragraph(new Run(typeface.FaceNames.Values.First())))
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black)
            });

            GlyphTypeface glyphTypeface;

            tableRow.Cells.Add(new TableCell(new Paragraph(new Run(SampleText)))
            {
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                FontFamily = typeface.FontFamily,
                FontStretch = typeface.Stretch,
                FontWeight = typeface.Weight,
                FontStyle = typeface.Style
            });

            return tableRow;
        }
    }
}
