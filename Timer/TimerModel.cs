using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTimer.Timer
{
    class TimerModel
    {
        public TimeSpan TimeLeft { get; set; }
        public bool isPaused { get; set; }
    }
}
