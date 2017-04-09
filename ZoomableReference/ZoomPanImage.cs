using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ZoomableReference.Model;

namespace ZoomableReference
{
    class ZoomPanImage : Image
    {
        ImageRotator rotator;

        TransformGroup transformGroup = new TransformGroup();

        ScaleTransform scaleTransform = new ScaleTransform();
        TranslateTransform translateTransform = new TranslateTransform();
        RotateTransform rotateTransform = new RotateTransform();

        public bool? IsRotateMode { get; set; } = false;

        private Point origin;
        private Point start;

        private Border border;
        public Border MyBorder
        {
            private get { return border; }
            set
            {
                border = value;

                MouseLeftButtonDown += image_MouseLeftButtonDown;
                MouseLeftButtonUp += image_MouseLeftButtonUp;
                MouseMove += image_MouseMove;
            }
        }

        private Border moveBorder;
        public Border MoveBorder
        {
            get
            {
                return moveBorder;
            }
            set
            {
                moveBorder = value;
                MoveBorder.RenderTransform = translateTransform;
            }
        }

        /// <summary>
        /// Remember to add MoveBorder before MyBorder. 
        /// That ugly again, but it's only me here.
        /// </summary>
        public ZoomPanImage()
        {
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(rotateTransform);


            RenderTransform = transformGroup;

            rotator = new ImageRotator(this);

            MouseWheel += image_MouseWheel;
        }


        public void HorizontalFlip()
        {
            scaleTransform.ScaleX = -scaleTransform.ScaleX;
            translateTransform.X = -translateTransform.X;
        }

        public void VerticalFlip()
        {
            scaleTransform.ScaleY = -scaleTransform.ScaleY;
            translateTransform.Y = -translateTransform.Y;
        }

        public void SetZoomPan(double scaleX, double scaleY, double posX, double posY, double angle)
        {
            scaleTransform.ScaleX = scaleX;
            scaleTransform.ScaleY = scaleY;

            translateTransform.X = posX;
            translateTransform.Y = posY;

            rotateTransform.Angle = angle;
        }

        public void SetZoomPan(ZoomPan zp)
        {
            SetZoomPan(zp.scaleX, zp.scaleY, zp.posX, zp.posY, zp.angle);
        }

        public ZoomPan GetZoomPan()
        {
            return ZoomPan.GetData(scaleTransform, translateTransform, rotateTransform);
        }


        public void ResetZoomPan()
        {
            SetZoomPan(1, 1, 0, 0, 0);
        }

        /// <summary>
        /// -- Zoom or Rotate
        /// </summary>
        /// <param name="e"></param>
        public void MouseWheelZoom(MouseWheelEventArgs e)
        {
            if (IsRotateMode == true)
                rotator.RotateClock((e.Delta > 0) ? 5.0 : -5.0);
            else
            {
                double zoom = e.Delta > 0 ? 0.05 : -0.05;
                if (scaleTransform.ScaleX > 0)
                    scaleTransform.ScaleX += zoom;
                else
                    scaleTransform.ScaleX -= zoom;


                if (scaleTransform.ScaleY > 0)
                    scaleTransform.ScaleY += zoom;
                else
                    scaleTransform.ScaleY -= zoom;
            }
        }

        public void MouseLeftDown(MouseButtonEventArgs e)
        {
            CaptureMouse();
            start = e.GetPosition(border);
            origin = new Point(translateTransform.X, translateTransform.Y);
        }

        public void MouseLeftUp()
        {
            ReleaseMouseCapture();
        }

        public void MouseMoveFunc(MouseEventArgs e)
        {
            if (!IsMouseCaptured) return;

            Vector v = start - e.GetPosition(border);
            translateTransform.X = origin.X - v.X;
            translateTransform.Y = origin.Y - v.Y;
        }

        internal void AddAngle(double angle)
        {
            rotateTransform.Angle += angle;
        }


        #region mosue events
        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseWheelZoom(e);
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MouseLeftUp();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveFunc(e);
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseLeftDown(e);
        }
        #endregion
    }
}
