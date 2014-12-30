using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TreeGraphGenerator
{
    internal class TreeNode
    {
        private double offset;
        private SizeD nodeSize;

        private static readonly StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center
        };

        public TreeNode(string label)
        {
            Label = label;
            Children = new List<TreeNode>();
        }

        public string Label
        {
            get;
            set;
        }

        public IList<TreeNode> Children
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Label;
        }

        private double MeasureNode(DrawTreeContext context)
        {
            double borderThickness = context.BorderPen == null ? 0.0 : context.BorderPen.Width;

            nodeSize = new SizeD((context.NodeHorizontalPadding + borderThickness) * 2.0 + context.Graphics.MeasureString(Label, context.LabelFont, PointF.Empty, stringFormat).Width,
                                 (context.NodeVerticalPadding + borderThickness) * 2.0 + context.LabelHeight);

            return nodeSize.Width;
        }

        public TreeLayout Layout(DrawTreeContext context)
        {
            if (Children.Count == 0)
            {
                var nodeWidth = MeasureNode(context);

                return new TreeLayout()
                {
                    Width = nodeWidth,
                    Pivot = nodeWidth / 2.0,
                    RowWidths = { new TreeRowWidth(0.0, 0.0) }
                };
            }
            else
            {
                var joinResult = TreeLayout.Join(Children.Select(n => n.Layout(context)).ToArray(), context.NodeHorizontalSep);
                double pivot = (joinResult.Item4.First() + joinResult.Item4.Last()) / 2.0;
                double nodeWidth = MeasureNode(context);
                double nodeLeft = pivot - nodeWidth / 2.0;
                double nodeRight = pivot + nodeWidth / 2.0;
                double left = Math.Min(nodeLeft, joinResult.Item1);
                double right = Math.Max(nodeRight, joinResult.Item2);

                if (nodeLeft < joinResult.Item1)
                {
                    foreach (var treeRowWidth in joinResult.Item3)
                    {
                        treeRowWidth.Left += joinResult.Item1 - nodeLeft;
                    }
                }

                if (nodeRight > joinResult.Item2)
                {
                    foreach (var treeRowWidth in joinResult.Item3)
                    {
                        treeRowWidth.Right += nodeRight - joinResult.Item2;
                    }
                }

                joinResult.Item3.Insert(0, new TreeRowWidth(nodeLeft - left, right - nodeRight));

                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].offset = joinResult.Item4[i] - pivot;
                }

                return new TreeLayout()
                {
                    Width = right - left,
                    Pivot = pivot - left,
                    RowWidths = joinResult.Item3
                };
            }
        }

        private void DrawNode(DrawTreeContext context, PointD location)
        {
            if (context.BorderPen != null)
            {
                RectangleD borderRect = new RectangleD(location.X - nodeSize.Width / 2.0 + context.BorderPen.Width / 2.0,
                                                       location.Y + context.BorderPen.Width / 2.0,
                                                       nodeSize.Width - context.BorderPen.Width,
                                                       nodeSize.Height - context.BorderPen.Width);

                if (context.BackgroundBrush != null)
                {
                    context.Graphics.FillRectangle(context.BackgroundBrush, borderRect);
                }

                context.Graphics.DrawRectangle(context.BorderPen,
                                               (float)borderRect.X,
                                               (float)borderRect.Y,
                                               (float)borderRect.Width,
                                               (float)borderRect.Height);
            }
            else
            {
                if (context.BackgroundBrush != null)
                {
                    context.Graphics.FillRectangle(context.BackgroundBrush, new RectangleD(location, nodeSize));
                }
            }

            if (context.PreferCjk)
            {
                stringFormat.LineAlignment = StringAlignment.Near;
                context.Graphics.DrawString(Label,
                                            context.LabelFont,
                                            context.LabelBrush,
                                            (float)location.X,
                                            (float)(location.Y + (context.BorderPen == null ? 0.0 : context.BorderPen.Width) + context.NodeVerticalPadding),
                                            stringFormat);
            }
            else
            {
                stringFormat.LineAlignment = StringAlignment.Center;
                context.Graphics.DrawString(Label,
                                            context.LabelFont,
                                            context.LabelBrush,
                                            (float)location.X,
                                            (float)(location.Y + nodeSize.Height / 2.0),
                                            stringFormat);
            }
        }

        public void DrawTree(DrawTreeContext context, PointD location)
        {
            if (context.ConnectorPen != null && Children.Count > 0)
            {
                float nextY = (float)(location.Y + nodeSize.Height + context.NodeVerticalSep);

                if (Children.Count == 1)
                {
                    context.Graphics.DrawLine(context.ConnectorPen,
                                              (float)location.X,
                                              (float)(location.Y + nodeSize.Height),
                                              (float)location.X,
                                              nextY);
                }
                else if (Children.Count >= 2)
                {
                    float branchY = (float)(location.Y + nodeSize.Height + context.NodeVerticalSep / 2.0);

                    context.Graphics.DrawLine(context.ConnectorPen,
                                              (float)location.X,
                                              (float)(location.Y + nodeSize.Height),
                                              (float)location.X,
                                              branchY);
                    context.Graphics.DrawLines(context.ConnectorPen, new[]
                    {
                        new PointF((float)(location.X + Children.First().offset), nextY),
                        new PointF((float)(location.X + Children.First().offset), branchY),
                        new PointF((float)(location.X + Children.Last().offset), branchY),
                        new PointF((float)(location.X + Children.Last().offset), nextY)
                    });

                    foreach (var treeNode in Children.Skip(1).Take(Children.Count - 2))
                    {
                        context.Graphics.DrawLine(context.ConnectorPen,
                                                  (float)(location.X + treeNode.offset),
                                                  branchY,
                                                  (float)(location.X + treeNode.offset),
                                                  nextY);
                    }
                }
            }

            DrawNode(context, location);

            foreach (var treeNode in Children)
            {
                treeNode.DrawTree(context, new PointD(location.X + treeNode.offset, location.Y + nodeSize.Height + context.NodeVerticalSep));
            }
        }
    }
}
