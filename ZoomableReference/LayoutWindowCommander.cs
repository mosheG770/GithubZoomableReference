using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    public class LayoutWindowCommander : IWindowCommander
    {
        LayoutWindow win;
        public LayoutWindowCommander(LayoutWindow lw)
        {
            win = lw;
        }

        public void Close()
        {
            win.Close();
        }

        public void Minimize()
        {
            win.WindowState = WindowState.Minimized;
        }

        public void Show()
        {
            win.WindowState = WindowState.Maximized;
        }
    }
}
