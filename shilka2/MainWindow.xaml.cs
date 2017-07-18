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
        Random rand;

        List<Shell> shells = new List<Shell>();
        List<Line> allLines = new List<Line>();

        double ptX = 0;
        double ptY = 0;
        double currentHeight;
        bool Fire = false;
        int FireMutex = 0;

        public MainWindow()
        {
            InitializeComponent();
            rand = new Random();

            System.Timers.Timer Game = new System.Timers.Timer(30);
            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(ShellsFire);
            Game.Elapsed += new ElapsedEventHandler(ShellsFly);
            Game.Start();
        }

        public void ShellsFly(object obj, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                foreach(var line in allLines)
                {
                    canvas.Children.Remove(line);
                }

                allLines.Clear();

                FireMutex++;

                foreach (var shell in shells)
                {
                    Line shellTrace = new Line();

                    shellTrace.X1 = shell.x + shell.cos;
                    shellTrace.Y1 = shell.y - shell.sin;
                    shellTrace.X2 = shell.x + 5 * shell.cos;
                    shellTrace.Y2 = shell.y - 5 * shell.sin;

                    shellTrace.Stroke = Brushes.Black;

                    shell.x = (shell.x + 15 * shell.cos);
                    shell.y = (shell.y - 15 * shell.sin);

                    canvas.Children.Add(shellTrace);

                    allLines.Add(shellTrace);
                }

                FireMutex--;
            }));
        }

        public void ShellsFire(object obj, ElapsedEventArgs e)
        {
            if (Fire)
            {
                FireMutex++;
                if (FireMutex > 1)
                {
                    FireMutex--;
                    return;
                }

                Shell newShell = new Shell();

                newShell.x = rand.Next(-3, 3);
                newShell.y = currentHeight + rand.Next(-3, 3);

                double e1 = Math.Sqrt((ptX * ptX) + (ptY * ptY));
                newShell.cos = ptX / e1;
                newShell.sin = ptY / e1;

                shells.Add(newShell);
                FireMutex--;
            }
        }

        void SetNewTergetPoint(Point pt, object sender)
        {
            ptX = pt.X;
            ptY = (sender as Window).Height - pt.Y;
            currentHeight = (sender as Window).Height;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetNewTergetPoint(e.GetPosition((Window)sender), sender);
            Fire = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Fire = false;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Fire)
            {
                SetNewTergetPoint(e.GetPosition((Window)sender), sender);
            }
        }
    }
}
