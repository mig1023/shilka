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
        List<Shell> shells = new List<Shell>();
        List<Line> all_lines = new List<Line>();
        
        public MainWindow()
        {
            InitializeComponent();

            System.Timers.Timer Game = new System.Timers.Timer(50);
            Game.Enabled = true;
            Game.Elapsed += new ElapsedEventHandler(shells_fly);
            Game.Start();
        }

        public void shells_fly(object obj, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                foreach(var line in all_lines)
                {
                    canvas.Children.Remove(line);
                }

                all_lines.Clear();

                foreach(var shell in shells)
                {
                    Line shell_trace = new Line();
                    shell_trace.X1 = shell.x + shell.cos;
                    shell_trace.Y1 = shell.y + shell.sin;
                    shell_trace.X2 = shell.x + 15 * shell.cos;
                    shell_trace.Y2 = shell.y + 15 * shell.sin;
                    shell_trace.Stroke = Brushes.Black;

                    shell.x = (shell.x + 15 * shell.cos);
                    shell.y = (shell.y + 15 * shell.sin);

                    canvas.Children.Add(shell_trace);

                    all_lines.Add(shell_trace);
                }
            }));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Window)sender);

            Shell new_shell = new Shell();
            new_shell.x = 0;
            new_shell.y = 0; //(sender as Window).Height - 50;
            
            double e1 = Math.Sqrt((pt.X * pt.X) + (pt.Y * pt.Y));
            new_shell.cos = pt.X / e1;
            new_shell.sin = pt.Y / e1;

            shells.Add(new_shell);
        }
    }
}
