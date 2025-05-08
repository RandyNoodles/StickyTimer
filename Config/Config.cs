using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StickyTimer.Config
{
    using System.Text.Json.Serialization;

    public class RootConfig
    {
        [JsonPropertyName("config")]
        public Config Config { get; set; } = new Config();
    }

    public class Config
    {
        [JsonPropertyName("ui")]
        public Config_UI UI { get; set; } = new Config_UI();

        [JsonPropertyName("saved-state")]
        public Config_SavedState SavedState { get; set; } = new Config_SavedState();

        [JsonPropertyName("settings")]
        public Config_Settings Settings { get; set; } = new Config_Settings();
    }

    public class Config_UI
    {
        [JsonPropertyName("transparency")]
        public double Transparency { get; set; }

        [JsonPropertyName("dark-mode")]
        public bool DarkMode { get; set; }

        [JsonPropertyName("theme")]
        public string Theme { get; set; } = string.Empty;
    }

    public class Config_SavedState
    {
        [JsonPropertyName("time-start")]
        public string TimeStart { get; set; } = "00:01:00";

        [JsonPropertyName("time-remaining")]
        public string TimeRemaining { get; set; } = "00:01:00";
    }

    public class Config_Settings
    {
        [JsonPropertyName("alarm-on")]
        public bool AlarmOn { get; set; }

        [JsonPropertyName("alarm-path")]
        public string AlarmPath { get; set; } = string.Empty;

        [JsonPropertyName("alarm-volume")]
        public int AlarmVolume { get; set; } = 50;

        [JsonPropertyName("pomodoro-mode")]
        public bool PomodoroMode { get; set; }

        [JsonPropertyName("pomodoro-break")]
        public int PomodoroBreak { get; set; }

        [JsonPropertyName("pomodoro-cycles")]
        public int PomodoroCycles { get; set; }
    }
}
