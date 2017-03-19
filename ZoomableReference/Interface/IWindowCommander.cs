using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    interface IWindowCommander
    {
        void Minimize();
        void Show();
        void Close();
    }
}
