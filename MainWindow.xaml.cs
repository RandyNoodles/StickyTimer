using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System;


namespace StickyTimer
{
  
    public partial class MainWindow : Window
    {

        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;
        private bool isDragging = false;
        private bool _isPaused = true;



        public MainWindow()
        {
            InitializeComponent();

            _timeLeft = TimeSpan.FromSeconds(600);



            CountDownMinutes.Text = $"{_timeLeft.Minutes:D2}";
            CountDownSeconds.Text = _timeLeft.ToString(@"ss");


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
        }

 


        private void TimerTick(object? sender, EventArgs e)
        {
            if (_timeLeft.TotalSeconds > 0)
            {
                _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));

                CountDownMinutes.Text = $"{_timeLeft.Minutes:D2}";
                CountDownSeconds.Text = _timeLeft.ToString(@"ss");
            }
            else
            {
                _timer.Stop();
                this.Close();
            }
        }

        private void btn_PauseOrPlayTimer(object sender, RoutedEventArgs e)
        {
            if (_isPaused)
            {
                
                btn_StartTimer.Content = "\uE769;";//Change to pause symbol
                CountDownMinutes.Text = $"{_timeLeft.Minutes:D2}";
                CountDownSeconds.Text = _timeLeft.ToString(@"ss");
                _timer.Start();
                _isPaused = false;
            }
            else
            {
                btn_StartTimer.Content = "\uE768;";//Change back to play symbol
                _timer.Stop();
                _isPaused = true;
            }
        }

        ////////////////////////////
        ///Top-Right Corner Buttons
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Owner = this;
            settings.ShowDialog();
        }



        private void DragArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                if(this.WindowState == WindowState.Maximized)
                {
                    ExitMaximizedAndMoveToMousePosition(e);
                }
                this.DragMove();
            }
        }

        private void ExitMaximizedAndMoveToMousePosition(MouseButtonEventArgs e)
        {
            //Get mouse position relative to window
            var mousePosition = e.GetPosition(this);
            //Convert mouse position to screen coordinates
            Point screenPos = PointToScreen(mousePosition);

            //Get mouse position relative as a % of X within the window
            double relativeX = mousePosition.X / this.ActualWidth;


            //Exit Maximized mode
            this.WindowState = WindowState.Normal;

            //Wait for screen to update
            this.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);

            //Snap window to where the mouse is
            this.Left = screenPos.X - (this.ActualWidth * relativeX);
            this.Top = 2;
        }

        //Re-implementing window resize
        private const int GripSize = 16; //Size from the corner inward to detect corner resize
        private const int BorderSize = 5; //Thickness along each edge
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource windowSource = HwndSource.FromHwnd(windowHandle);
            windowSource.AddHook(WndProc);
        }


        //Some guy's code to re-apply resizing for borderless
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_NCHITTEST)
            {
                Point pos = GetMousePosition(lParam);
                Point rel = PointFromScreen(pos);

                double width = ActualWidth;
                double height = ActualHeight;

                if (rel.Y <= BorderSize)
                {
                    if (rel.X <= BorderSize)
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTTOPLEFT;
                    }
                    else if (rel.X >= width - BorderSize)
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTTOPRIGHT;
                    }
                    else
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTTOP;
                    }
                }
                else if (rel.Y >= height - BorderSize)
                {
                    if (rel.X <= BorderSize)
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTBOTTOMLEFT;
                    }
                    else if (rel.X >= width - BorderSize)
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTBOTTOMRIGHT;
                    }
                    else
                    {
                        handled = true;
                        return (IntPtr)NativeMethods.HTBOTTOM;
                    }
                }
                else if (rel.X <= BorderSize)
                {
                    handled = true;
                    return (IntPtr)NativeMethods.HTLEFT;
                }
                else if (rel.X >= width - BorderSize)
                {
                    handled = true;
                    return (IntPtr)NativeMethods.HTRIGHT;
                }
            }

            return IntPtr.Zero;
        }

        private static Point GetMousePosition(IntPtr lParam)
        {
            int x = unchecked((short)(lParam.ToInt32() & 0xFFFF));
            int y = unchecked((short)(lParam.ToInt32() >> 16));
            return new Point(x, y);
        }


    }




}