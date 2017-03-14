using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoomableReference.Model;

namespace ZoomableReference
{
    class WindowPositionSizeArgs
    {
        //public Preset Preset;
        public double Top;
        public double Left;
        public double Height;
        public double Width;

        public string Name;

        public override string ToString()
        {
            return Name;
        }
    }
}
