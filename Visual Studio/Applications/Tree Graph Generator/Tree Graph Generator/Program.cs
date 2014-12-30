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
            TreeNode root = new TreeNode("供热站虚拟仿真系统")
            {
                Children =
                {
                    new TreeNode("框架模块")
                    {
                        Children =
                        {
                            new TreeNode("应用程序类"),
                            new TreeNode("主窗口")
                        }
                    },
                    new TreeNode("组件管理模块")
                    {
                        Children =
                        {
                            new TreeNode("组件数据结构"),
                            new TreeNode("用户交互模块")
                        }
                    },
                    new TreeNode("装配流程管理模块")
                    {
                        Children =
                        {
                            new TreeNode("装配流程定义"),
                            new TreeNode("装配流程的加载")
                        }
                    },
                    new TreeNode("装配控制模块")
                    {
                        Children =
                        {
                            new TreeNode("自动装配控制"),
                            new TreeNode("手动装配控制"),
                        }
                    }
                }
            };

            DrawTree(root, @"D:\1.wmf");
        }
    }
}
