using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeGraphGenerator
{
    internal class TreeLayout
    {
        public TreeLayout()
        {
            RowWidths = new List<TreeRowWidth>();
        }

        public double Width
        {
            get;
            set;
        }

        public double Pivot
        {
            get;
            set;
        }

        public IList<TreeRowWidth> RowWidths
        {
            get;
            set;
        }

        /// <summary>
        ///   Join TreeLayouts.
        /// </summary>
        /// <param name="treeLayouts">The TreeLayouts to join.</param>
        /// <param name="sep">Horzontal sep between nodes.</param>
        /// <returns>(Left, Right, TreeRowWidths, Pivots)</returns>
        public static Tuple<double, double, IList<TreeRowWidth>, IReadOnlyList<double>> Join(IReadOnlyList<TreeLayout> treeLayouts, double sep)
        {
            var firstLayout = treeLayouts.First();
            double left = 0;
            double right = firstLayout.Width;
            var rowWidths = firstLayout.RowWidths.ToList();
            var pivots = new List<double>() { firstLayout.Pivot };

            foreach (var layout in treeLayouts.Skip(1))
            {
                double offset = rowWidths.Zip(layout.RowWidths, (l, r) => l.Right + r.Left).Min() - sep;
                pivots.Add(right - offset + layout.Pivot);

                if (layout.RowWidths.Count <= rowWidths.Count)
                {
                    double rightSpace = layout.Width - offset;

                    if (rightSpace < 0)
                    {
                        for (int i = 0; i < layout.RowWidths.Count; i++)
                        {
                            rowWidths[i].Right = layout.RowWidths[i].Right - rightSpace;
                        }

                        foreach (var rowWidth in rowWidths.Zip(layout.RowWidths, (x, y) => new { Left = x, Right = y.Right - rightSpace }))
                        {
                            rowWidth.Left.Left = rowWidth.Right;
                        }
                    }
                    else
                    {
                        // Extend right.
                        right += rightSpace;
                        for (int i = 0; i < layout.RowWidths.Count; i++)
                        {
                            rowWidths[i].Right = layout.RowWidths[i].Right;
                        }
                    }
                }
                else
                {
                    double leftSpace = right - left - offset;

                    if (leftSpace <= 0)
                    {
                        // Extend left.
                        left -= leftSpace;
                        foreach (TreeRowWidth rowWidth in rowWidths)
                        {
                            rowWidth.Right = rowWidth.Left - leftSpace;
                        }
                        rowWidths.AddRange(layout.RowWidths.Skip(rowWidths.Count));
                    }
                    else
                    {
                        // Extend right.
                        right += layout.Width - offset;
                        for (int i = 0; i < rowWidths.Count; i++)
                        {
                            rowWidths[i].Right = layout.RowWidths[i].Right;
                        }
                        rowWidths.AddRange(layout.RowWidths.Skip(rowWidths.Count).Select(r => new TreeRowWidth(r.Left + leftSpace, r.Right)));
                    }
                }
            }

            return Tuple.Create<double, double, IList<TreeRowWidth>, IReadOnlyList<double>>(left, right, rowWidths, pivots);
        }
    }
}
