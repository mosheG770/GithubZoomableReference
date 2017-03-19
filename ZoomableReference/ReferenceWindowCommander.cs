﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    }
}
