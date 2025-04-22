using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;


namespace StickyTimer
{
    internal class Timer
    {

        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;
        public TimeSpan TimeLeft { get { return _timeLeft; } }
        public TimeSpan ResetValue { get; set; }


        private const String _TAG = "Timer()";

        ///////////
        ///Events

        public event EventHandler Tick;
        public void OnTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
        
        public event EventHandler TimeUp;
        public void OnTimeUp()
        {
            TimeUp?.Invoke(this, EventArgs.Empty);
        }


        //Pomodoro settings?
        //Behaviour on time up?

        ////////////////
        ///Constructors
        public Timer(TimeSpan timerLength)
        {
            _timer = new DispatcherTimer();
            _timeLeft = timerLength;
            ResetValue = timerLength;
            Logger.d(_TAG, "Timer created");
        }


        ///////////
        ///Methods

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Reset()
        {
            _timer.Stop();
            _timeLeft = ResetValue;
        }

        

    }
}
