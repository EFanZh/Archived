﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ChannelValuesWrapPanel.Children.Add(new ChannelValuesController(new LinearColorSpace("sRGB",
                                                                                                 Channel.CreateStandardChannel("Red"),
                                                                                                 Channel.CreateStandardChannel("Green"),
                                                                                                 Channel.CreateStandardChannel("Blue"))));

            ChannelValuesWrapPanel.Children.Add(new ChannelValuesController(new LinearColorSpace("scRGB",
                                                                                                 Channel.CreateStandardChannel("Reddddddddddd"),
                                                                                                 Channel.CreateStandardChannel("Green"),
                                                                                                 Channel.CreateStandardChannel("Blue"))));
        }

        public Model Model
        {
            get;
        }
    }
}
