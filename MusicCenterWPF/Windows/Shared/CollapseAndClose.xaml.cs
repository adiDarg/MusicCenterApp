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

namespace MusicCenterWPF.Windows.Shared
{
    /// <summary>
    /// Interaction logic for CollapseAndClose.xaml
    /// </summary>
    public partial class CollapseAndClose : UserControl
    {
        public CollapseAndClose()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentwWindow = Window.GetWindow(this);
            parentwWindow.WindowState = WindowState.Minimized;
        }
    }
}
