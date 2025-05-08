using StickyTimer.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace StickyTimer.ViewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private readonly Config_Settings _settingsConfig;
        private readonly Config_SavedState _savedStateConfig;

        private Timer _timer;
        


        public TimerViewModel(Config_Settings settingsConfig, Config_SavedState savedStateConfig)
        {
            _settingsConfig = settingsConfig;
            _savedStateConfig = savedStateConfig;
            
        }

        public bool AlarmOn
        {
            get { return _settingsConfig.AlarmOn; }
            set
            {
                if(_settingsConfig.AlarmOn != value)
                {
                    _settingsConfig.AlarmOn = value;
                    OnPropertyChanged(nameof(AlarmOn));
                }
            }
        }
        public string AlarmPath
        {
            get { return _settingsConfig.AlarmPath; }
            set
            {
                if (_settingsConfig.AlarmPath != value)
                {
                    _settingsConfig.AlarmPath = value;
                    OnPropertyChanged(nameof(AlarmPath));
                }
            }
        }
        public int AlarmVolume
        {
            get { return _settingsConfig.AlarmVolume; }
            set
            {
                if (_settingsConfig.AlarmVolume != value)
                {
                    _settingsConfig.AlarmVolume = value;
                    OnPropertyChanged(nameof(AlarmVolume));
                }
            }
        }

        public bool PomodoroMode
        {
            get { return _settingsConfig.PomodoroMode; }
            set
            {
                if(_settingsConfig.PomodoroMode != value)
                {
                    _settingsConfig.PomodoroMode = value;
                    OnPropertyChanged(nameof(PomodoroMode));
                }
            }
        }

        public int PomodoroBreak
        {
            get { return _settingsConfig.PomodoroBreak; }
            set
            {
                if(_settingsConfig.PomodoroBreak != value)
                {
                    _settingsConfig.PomodoroBreak = value;
                    OnPropertyChanged(nameof(PomodoroBreak));
                }
            }
        }
        public int PomodoroCycles
        {
            get { return _settingsConfig.PomodoroCycles; }
            set
            {
                if (_settingsConfig.PomodoroCycles != value)
                {
                    _settingsConfig.PomodoroCycles = value;
                    OnPropertyChanged(nameof(PomodoroCycles));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
