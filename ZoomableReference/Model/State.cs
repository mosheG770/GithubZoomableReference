using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ZoomableReference.Model
{
    class State
    {
        public bool IsTemplate { get; set; }
        public bool IsFutureWindow { get; set; }
        public bool IsLocked { get; set; }
        public ZoomPan ZoomPan { get; set; }
        public string imageSource { get; set; }
        public Preset Preset { get; set; }
        public Brush BackgroundColor { get; set; }

        [JsonIgnore]
        public IWindowCommander Commander { get; set; }
    }
}
