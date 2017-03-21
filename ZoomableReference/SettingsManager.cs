using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    class SettingsManager
    {
        public static bool ShowGetFocus { get; set; }

        private static bool isSimpleMode;
        public static bool IsSimpleMode
        {
            get { return isSimpleMode; }
            set { isSimpleMode = value; ModeChange?.Invoke(); }
        }

        public static event Action ModeChange;
    }
}
