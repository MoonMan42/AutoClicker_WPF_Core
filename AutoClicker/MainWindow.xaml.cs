using Microsoft.VisualStudio.PlatformUI;
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

        int clickCounter = 0;


        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(TickEvent);      

            
        }

        //This is a replacement for Cursor.Position in WinForms
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        private void TickEvent(object sender, EventArgs e)
        {
            LeftMouseClick(0, 0);
            clickCounter += 1;
            clickCountLabel.Content = clickCounter;
        }

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            //SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        private void StartBtn_Clicked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, Int32.Parse(TimeBox.Text));
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

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            clickCounter = 0;
            clickCountLabel.Content = clickCounter;
        }
    }
}
