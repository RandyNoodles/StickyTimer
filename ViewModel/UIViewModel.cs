using StickyTimer.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StickyTimer.ViewModel
{
    class UIViewModel : INotifyPropertyChanged
    {

        private Config_UI _uiConfig;

        public UIViewModel(Config_UI ui)
        {
            _uiConfig = ui;
        }

        public String ThemeUri { get; set; }

        //This one is weird, since they're internal resources?
        //public String ThemeUri
        //{
        //    get { return _uiConfig.Theme; }
        //    set
        //    {
        //        if (_uiConfig.Theme != value)
        //        {
        //            _uiConfig.Theme = value;
        //            OnPropertyChanged(nameof());
        //        }
        //    }
        //}
        public bool DarkMode
        {
            get { return _uiConfig.DarkMode; }
            set
            {
                if (_uiConfig.DarkMode != value)
                {
                    _uiConfig.DarkMode = value;
                    OnPropertyChanged(nameof(DarkMode));
                }
            }
        }
        public double Transparency
        {
            get { return _uiConfig.Transparency; }
            set
            {
                if (_uiConfig.Transparency != value)
                {
                    _uiConfig.Transparency = value;
                    OnPropertyChanged(nameof(Transparency));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
