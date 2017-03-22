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

        private static ReferenceWindowMode referenceWindowMode = ReferenceWindowMode.Normal;
        public static ReferenceWindowMode ReferenceWindowMode
        {
            get { return referenceWindowMode; }
            set { referenceWindowMode = value; ModeChange?.Invoke(); }
        }

        public static event Action ModeChange;
    }
}
