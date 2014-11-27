using System;
using System.Drawing;
using System.Drawing.Imaging;

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

                    Font labelFont = new Font("Times New Roman", 9.0f, FontStyle.Regular, GraphicsUnit.Pixel);
                    DrawTreeContext context = new DrawTreeContext()
                    {
                        Graphics = graphics,
                        BorderPen = new Pen(Color.Black, 0.75f),
                        ConnectorPen = new Pen(Color.Black, 0.5f),
                        LabelBrush = Brushes.Black,
                        LabelFont = labelFont,
                        LabelHeight = labelFont.Size,
                        NodeHorizontalSep = 9.0,
                        NodeVerticalSep = 18.0,
                        NodeHorizontalPadding = 4.5,
                        NodeVerticalPadding = 4.5
                    };

                    treeNode.DrawTree(context, new PointD(treeNode.Layout(context).Pivot, 0.0));
                }
            }
        }

        private static void Main()
        {
            TreeNode root = new TreeNode("根节点")
            {
                Children =
                {
                    new TreeNode("参考线开关")
                    {
                        Children = {new TreeNode("参考线")}
                    },
                    new TreeNode("工具摄像机")
                    {
                        Children =
                        {
                            new TreeNode("工具容器")
                            {
                                Children =
                                {
                                    new TreeNode("平移工具"),
                                    new TreeNode("旋转工具"),
                                }
                            }
                        }
                    },
                    new TreeNode("组件实例占位符组"),
                    new TreeNode("组件实例组")
                }
            };

            DrawTree(root, @"D:\test.emf");
        }
    }
}
