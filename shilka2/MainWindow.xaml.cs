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
            Game.Elapsed += new ElapsedEventHandler(Aircraft.AircraftFly);
            Game.Start();

            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;

            double heightForShilka = System.Windows.SystemParameters.PrimaryScreenHeight -
                ShilkaImg.ActualHeight - Shilka.SHILKA_HEIGHT_CORRECTION;

            ShilkaImg.Margin = new Thickness(0, heightForShilka, 0, 0);

            Aircraft.AircraftStart();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);
            Shell.Fire = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Shell.Fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Shell.Fire)
                Shilka.SetNewTergetPoint(e.GetPosition((Window)sender), sender);
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
