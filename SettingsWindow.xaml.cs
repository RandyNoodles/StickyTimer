using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StickyTimer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        ResizeHandler _resizeHandler;//Re-implement resizing for borderless window


        public SettingsWindow()
        {
            InitializeComponent();
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


    }
}
