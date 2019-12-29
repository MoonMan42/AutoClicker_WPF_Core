using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(TickEvent);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0, Int32.Parse(TimeBox.Text));        
            
        }

        private void TickEvent(object sender, EventArgs e)
        {
            Console.Beep();
        }

        private void StartBtn_Clicked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();

        }

        private void StopBtn_Clicked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
