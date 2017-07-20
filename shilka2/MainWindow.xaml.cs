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
        System.Timers.Timer Game;

        public MainWindow()
        {
            InitializeComponent();

            Game = new System.Timers.Timer(30);
            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFire);
            Game.Elapsed += new ElapsedEventHandler(Shell.ShellsFly);
            Game.Start();

            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;
        }

        void SetNewTergetPoint(Point pt, object sender)
        {
            Shell.ptX = pt.X;
            Shell.ptY = (sender as Window).Height - pt.Y - 50;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Shell.AnimationStop = true;
            Game.Stop();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
