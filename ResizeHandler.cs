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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System;

namespace StickyTimer
{
    public class ResizeHandler
    {
        public const int WM_NCHITTEST = 0x84;
        public const int HTCAPTION = 2;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;

        private readonly Window _window;
        private readonly int _borderSize;

        public ResizeHandler(Window window, int borderSize = 5)
        {
            _window = window;
            _borderSize = borderSize;
        }




        //Some guy's code to re-apply resizing for borderless
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCHITTEST)
            {
                Point pos = GetMousePosition(lParam);
                Point rel = _window.PointFromScreen(pos);

                double width = _window.ActualWidth;
                double height = _window.ActualHeight;

                if (rel.Y <= _borderSize)
                {
                    if (rel.X <= _borderSize)
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTTOPLEFT;
                    }
                    else if (rel.X >= width - _borderSize)
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTTOPRIGHT;
                    }
                    else
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTTOP;
                    }
                }
                else if (rel.Y >= height - _borderSize)
                {
                    if (rel.X <= _borderSize)
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTBOTTOMLEFT;
                    }
                    else if (rel.X >= width - _borderSize)
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTBOTTOMRIGHT;
                    }
                    else
                    {
                        handled = true;
                        return (IntPtr)ResizeHandler.HTBOTTOM;
                    }
                }
                else if (rel.X <= _borderSize)
                {
                    handled = true;
                    return (IntPtr)ResizeHandler.HTLEFT;
                }
                else if (rel.X >= width - _borderSize)
                {
                    handled = true;
                    return (IntPtr)ResizeHandler.HTRIGHT;
                }
            }

            return IntPtr.Zero;
        }

        public static Point GetMousePosition(IntPtr lParam)
        {
            int x = unchecked((short)(lParam.ToInt32() & 0xFFFF));
            int y = unchecked((short)(lParam.ToInt32() >> 16));
            return new Point(x, y);
        }

    }

}
