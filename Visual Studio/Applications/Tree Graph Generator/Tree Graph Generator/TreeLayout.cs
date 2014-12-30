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

        public override string ToString()
        {
            return string.Format("Width={0}, Pivot={1}, RowWidths={{{2}}}", Width, Pivot, string.Join(", ", RowWidths));
        }

        /// <summary>
        ///   Join TreeLayouts.
        /// </summary>
        /// <param name="treeLayouts">The TreeLayouts to join.</param>
        /// <param name="sep">Horzontal sep between nodes.</param>
        /// <returns>(Left, Right, TreeRowWidths, Pivots)</returns>
        public static Tuple<double, double, IList<TreeRowWidth>, IReadOnlyList<double>> Join(IReadOnlyList<TreeLayout> treeLayouts, double sep)
        {
            double left = 0;
            double right = treeLayouts.First().Width;
            var rowWidths = treeLayouts.First().RowWidths.ToList();
            var pivots = new List<double>() { treeLayouts.First().Pivot };

            foreach (var layout in treeLayouts.Skip(1))
            {
                double offset = rowWidths.Zip(layout.RowWidths, (l, r) => l.Right + r.Left).Min() - sep;

                pivots.Add(right + layout.Pivot - offset);

                if (layout.RowWidths.Count <= rowWidths.Count)
                {
                    double rightSpace = layout.Width - offset;

                    if (rightSpace < 0)
                    {
                        for (int i = 0; i < layout.RowWidths.Count; i++)
                        {
                            rowWidths[i].Right = layout.RowWidths[i].Right - rightSpace;
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
                        for (int i = layout.RowWidths.Count; i < rowWidths.Count; i++)
                        {
                            rowWidths[i].Right += rightSpace;
                        }
                    }
                }
                else
                {
                    double leftSpace = right - left - offset;

                    if (leftSpace <= 0)
                    {
                        // Extend left.
                        left += leftSpace;
                        for (int i = 0; i < rowWidths.Count; i++)
                        {
                            rowWidths[i].Left -= leftSpace;
                            rowWidths[i].Right = layout.RowWidths[i].Right;
                        }
                        rowWidths.AddRange(layout.RowWidths.Skip(rowWidths.Count));
                    }
                    else
                    {
                        for (int i = 0; i < rowWidths.Count; i++)
                        {
                            rowWidths[i].Right = layout.RowWidths[i].Right;
                        }
                        rowWidths.AddRange(layout.RowWidths.Skip(rowWidths.Count).Select(r => new TreeRowWidth(r.Left + leftSpace, r.Right)));
                    }

                    // Extend right.
                    right += layout.Width - offset;
                }
            }

            return Tuple.Create<double, double, IList<TreeRowWidth>, IReadOnlyList<double>>(left, right, rowWidths, pivots);
        }
    }
}
