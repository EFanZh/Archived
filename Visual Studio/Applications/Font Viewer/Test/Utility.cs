using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Test
{
    internal static class Utility
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                collection.Add(item);
            }
        }

        public static string ToHtml(this TableRowGroup tableRowGroup)
        {
            return string.Join("", tableRowGroup.Rows.Select(ToHtml));
        }

        public static string ToHtml(this TableRow tableRow)
        {
            return string.Format("<tr>{0}</tr>", string.Join("", tableRow.Cells.Select(ToHtml)));
        }

        public static string ToHtml(this TableCell tableCell)
        {
            return string.Format("<td colspan=\"{0}\">{1}</td>", tableCell.RowSpan, (new TextRange(tableCell.ContentStart, tableCell.ContentEnd)).Text);
        }
    }
}
