using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    class SettingsManager
    {
        private static bool isSimpleMode;

        public static bool IsSimpleMode
        {
            get { return isSimpleMode; }
            set { isSimpleMode = value; ModeChange(); }
        }
        //public static bool IsSimpleMode { get; set; }

        public static event Action ModeChange;
    }
}
