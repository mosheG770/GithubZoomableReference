using System;
using System.Linq;
using System.Text;

namespace ZoomableReference
{
    internal class ImageRotator
    {
        private ZoomPanImage zoomPanImage;

        public ImageRotator(ZoomPanImage zoomPanImage)
        {
            this.zoomPanImage = zoomPanImage;
        }

        internal void RotateClock(double v)
        {
            zoomPanImage.AddAngle(v);
        }
    }
}