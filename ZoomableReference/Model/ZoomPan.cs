﻿using System;
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
        public double angle { get; set; }

        public static ZoomPan GetData(ScaleTransform scaleTransform, TranslateTransform translateTransform, RotateTransform rotateTransform)
        {
            ZoomPan zp = new ZoomPan() {
                scaleX = scaleTransform.ScaleX,
                scaleY = scaleTransform.ScaleY,
                posX = translateTransform.X,
                posY = translateTransform.Y,
                angle = rotateTransform.Angle
            };
            return zp;
        }
    }
}
