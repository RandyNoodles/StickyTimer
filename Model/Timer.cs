using StickyTimer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTimer.Model
{
    public class Timer
    {
        public TimeSpan TimeLeft { get; set; }
        public bool isPaused { get; set; }
        public bool isPomodoroMode { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan BreakStartingTime { get; set; }
        public int PomodoroCycles { get; set; }
        public bool isAlarmOn { get; set; }
        public string AlarmFileUri { get; set; }

        public Timer(Config_SavedState savedState)
        {
            bool success = TimeSpan.TryParse(savedState.TimeStart, out TimeSpan temp);
            if (success) {
                TimeLeft = temp 
                
            }
        }

    }
}
