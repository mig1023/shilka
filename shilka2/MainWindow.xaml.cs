using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;

namespace shilka2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            System.Timers.Timer Game = new System.Timers.Timer(30);
            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
            Game.Start();
        }

        void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X;
            Shell.ptY = (sender as Window).Height - pt.Y;
            Shell.currentHeight = (sender as Window).Height;
            Shell.currentWidth = (sender as Window).Width;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetNewTergetPoint(e.GetPosition((Window)sender), sender);
            Shell.Fire = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shell.Fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Shell.Fire)
                SetNewTergetPoint(e.GetPosition((Window)sender), sender);
        }
    }
}
