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

namespace StickyTimer
{
  
    public partial class MainWindow : Window
    {

        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;
        private bool isDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            _timeLeft = TimeSpan.FromSeconds(600);
            CountdownText.Text = _timeLeft.ToString(@"mm\:ss");


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            if (_timeLeft.TotalSeconds > 0) {
                _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                CountdownText.Text = _timeLeft.ToString(@"mm\:ss");
            }
            else
            {
                _timer.Stop();
                this.Close();
            }
        }

        private void btn_StartTimer_Click(object sender, RoutedEventArgs e)
        {
            //Need to IMMEDIATELY deduct a second - otherwise it feels like play button didn't work.
            _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
            CountdownText.Text = _timeLeft.ToString(@"mm\:ss");

            btn_StartTimer.Visibility = Visibility.Collapsed;
            btn_PauseTimer.Visibility = Visibility.Visible;

            _timer.Start();
        }

        private void btn_PauseTimer_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();

            btn_PauseTimer.Visibility = Visibility.Collapsed;
            btn_StartTimer.Visibility = Visibility.Visible;
        }

        private void rect_HandleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
            
        }
    }
}