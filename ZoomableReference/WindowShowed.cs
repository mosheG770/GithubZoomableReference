using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    class WindowShowed : Window
    {
        public bool IsShowed { get; set; }

        protected override void OnClosed(EventArgs e)
        {
            IsShowed = false;
            base.OnClosed(e);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            IsShowed = true;
            base.OnContentRendered(e);
        }
    }
}
