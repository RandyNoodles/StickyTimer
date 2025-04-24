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
using System.Windows.Input;


namespace StickyTimer
{
  
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private bool _isPaused = true;


        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;


        private ResizeHandler _resizeHandler;

        String digitBoxTemp = String.Empty;


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

        //Re-implementing window resize
        private const int RESIZE_BORDER_WIDTH = 5; //Clickable width along each edge
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource windowSource = HwndSource.FromHwnd(windowHandle);

            _resizeHandler = new ResizeHandler(this, RESIZE_BORDER_WIDTH);
            windowSource.AddHook(_resizeHandler.WndProc);
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

        private void DigitBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = (TextBox)e.Source;
            txtBox.Background = new SolidColorBrush(Colors.Plum);
        }

        private void DigitBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = (TextBox)e.Source;
            txtBox.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void DigitBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DigitBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            //If ':' is pressed, move to next box
            if (e.Text == ":")
            {
                string tag = txtBox.Tag.ToString();
                string nextBoxName = tag.Substring(5);
                var next = (TextBox)FindName(nextBoxName);

                e.Handled = true;

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    next?.Focus();
                }), System.Windows.Threading.DispatcherPriority.Input);
                return;
            }

            if (!char.IsDigit(e.Text, 0))
            {
                //If delete is pressed, reset box to "00"
                if(e.Text[0] == (char)Key.Back || e.Text[0] == (char)Key.Delete)
                {
                    txtBox.Text = "00";
                    txtBox.CaretIndex = txtBox.Text.Length;
                }
                
                //Otherwise, just ignore.

                e.Handled = true;
                return;
            }

            //Mimic digital UI input
            //I.e Input number in rightmost column & shift left
            string current = txtBox.Text.PadLeft(2, '0');
            string newText = (current + e.Text).Substring(current.Length + e.Text.Length - 2);

            txtBox.Text = newText;
            txtBox.CaretIndex = newText.Length;
            e.Handled = true;
        }

        //If delete key is pressed, reset box to 00
        private void DigitBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                TextBox txtBox = (TextBox)sender;
                txtBox.Text = "00";
            }
        }

        private void CountDownSeconds_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            if(!int.TryParse(txtBox.Text, out int currentSeconds))
            {
                //Silently ignore I guess?
                e.Handled = true;
                return;
            }
            currentSeconds += e.Delta;

            //I think I need to reformat my time here to be more of an MVVM-style thing?

        }
    }




}