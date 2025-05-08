using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace StickyTimer.Config
{
    public class ConfigService
    {

        private readonly object _configFileLock;

        private bool _configLoaded;

        private string _appDataFolder;
        private string _configPath;

        public RootConfig RootConfig { get; set; }

        private const string DEFAULT_CONFIG = @"
        {
          ""config"": {
            ""saved-state"": {
              ""time-start"": ""00:01:00"",
              ""time-remaining"": ""00:01:00""
            },
            ""ui"": {
              ""transparency"": 5.0,
              ""dark-mode"": true,
              ""theme"": ""vampire""
            },
            ""settings"": {
              ""alarm-on"": true,
              ""alarm-path"": ""bongos.wav"",
              ""alarm-volume"": 100,
              ""pomodoro-mode"": false,
              ""pomodoro-break"": 10,
              ""pomodoro-cycles"": 3
            }
          }
        }
        ";

        public ConfigService()
        {
            _configLoaded = false;
            try
            {
                _appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StickyTimer");

                //Make appdata dir exists
                Directory.CreateDirectory(_appDataFolder);

                _configPath = Path.Combine(_appDataFolder, "config.json");

                if (!File.Exists(_configPath))
                {
                    File.WriteAllText(_configPath, DEFAULT_CONFIG);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error creating appdata folder: ${e.Message}");
            }
        }

        public RootConfig? Load()
        {
            try
            {
                string json = File.ReadAllText(_configPath);
                return JsonSerializer.Deserialize<RootConfig>(json);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading config data from ${_configPath}: ${e.Message}");
                return null;
            }
        }

        public void Save()
        {
            try
            {
                lock (_configFileLock)
                {
                    string json = JsonSerializer.Serialize(RootConfig);
                    File.WriteAllText(_configPath, json);
                }   
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error saving data to ${_configPath}: ${e.Message}");
            }
        }

    }
}
