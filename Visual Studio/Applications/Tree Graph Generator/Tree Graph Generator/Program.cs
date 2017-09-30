using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TreeGraphGenerator
{
    internal class Program
    {
        private static void DrawTree(TreeNode treeNode, string file)
        {
            using (Metafile mf = new Metafile(file, Graphics.FromHwnd(IntPtr.Zero).GetHdc(), EmfType.EmfOnly))
            {
                using (Graphics graphics = Graphics.FromImage(mf))
                {
                    graphics.PageUnit = GraphicsUnit.Point;

                    Font labelFont = new Font("SimSun", 9.0f, FontStyle.Regular, GraphicsUnit.Point);
                    DrawTreeContext context = new DrawTreeContext()
                    {
                        Graphics = graphics,
                        BorderPen = new Pen(Color.Black, 0.75f),
                        ConnectorPen = new Pen(Color.Black, 0.5f),
                        LabelBrush = Brushes.Black,
                        LabelFont = labelFont,
                        LabelHeight = labelFont.Size,
                        NodeHorizontalSep = 9.0,
                        NodeVerticalSep = 24.0,
                        NodeHorizontalPadding = 3.75,
                        NodeVerticalPadding = 3.75,
                        PreferCjk = true
                    };

                    treeNode.DrawTree(context, new PointD(treeNode.Layout(context).Pivot, 0.0));
                }
            }
        }

        private static void Main()
        {
            TreeNode root = new TreeNode("root")
            {
                Children =
                {
                    new TreeNode("a")
                    {
                        Children =
                        {
                            new TreeNode("b"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c")
                        }
                    },
                    new TreeNode("b"),
                    new TreeNode("a")
                    {
                        Children =
                        {
                            new TreeNode("b"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c"),
                            new TreeNode("c")
                        }
                    },
                }
            };

            DrawTree(root, @"D:\1.wmf");
        }
    }
}
