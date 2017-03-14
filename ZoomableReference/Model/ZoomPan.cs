using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ZoomableReference.Model
{
    class ZoomPan
    {
        public double scaleX { get; set; }
        public double scaleY { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }

        public static ZoomPan GetData(ScaleTransform scaleTransform, TranslateTransform translateTransform)
        {
            ZoomPan zp = new ZoomPan();
            zp.scaleX = scaleTransform.ScaleX;
            zp.scaleY = scaleTransform.ScaleY;
            zp.posX = translateTransform.X;
            zp.posY = translateTransform.Y;
            return zp;
        }
    }
}
