using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    public class ReferenceWindowCommander : IWindowCommander
    {
        private ReferenceWindow win;
        public ReferenceWindowCommander(ReferenceWindow rw)
        {
            win = rw;
        }


        public void Close()
        {
            win.Close();
        }

        public void Minimize()
        {
            win.WindowState = System.Windows.WindowState.Minimized;
        }

        public void Show()
        {
            win.WindowState = System.Windows.WindowState.Normal;
        }

        internal void ToggleLock()
        {
            if (win.LockBorder.Visibility == Visibility.Hidden)
            {
                Lock();
            }
            else
            {
                Unlock();
            }
        }

        internal void Lock()
        {
            win.LockBtn.Content = "Unlock";
            win.LockBorder.Visibility = Visibility.Visible;
            win.focus.IsLocked = true;
            win.LockBtn.Visibility = Visibility.Hidden;
        }

        internal void Unlock()
        {
            win.LockBtn.Content = "Lock";
            win.LockBorder.Visibility = Visibility.Hidden;
            win.focus.IsLocked = false;
        }
    }
}
