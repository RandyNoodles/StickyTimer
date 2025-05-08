using StickyTimer;
using StickyTimer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTimer.ViewModel
{
    class MainViewModel
    {
        public UIViewModel UI { get; }
        public TimerViewModel Timer { get; }
        

        private readonly ConfigService _configService;


        public MainViewModel()
        {
            _configService = new ConfigService();
            _configService.Load();

            UI = new UIViewModel(_configService.RootConfig.Config.UI);
            Timer = new TimerViewModel(_configService.RootConfig.Config.Settings, _configService.RootConfig.Config.SavedState);
        }

        public void SaveConfig()
        {
            _configService.Save();
        }




    }
}