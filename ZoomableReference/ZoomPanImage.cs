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
        TransformGroup transformGroup = new TransformGroup();
        ScaleTransform scaleTransform = new ScaleTransform();
        TranslateTransform translateTransform = new TranslateTransform();

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

        public ZoomPanImage()
        {
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);

            this.RenderTransform = transformGroup;

            MouseWheel += image_MouseWheel;
        }


        public void HorizontalFlip()
        {
            scaleTransform.ScaleX = -scaleTransform.ScaleX;
        }

        public void VerticalFlip()
        {
            scaleTransform.ScaleY = -scaleTransform.ScaleY;
        }

        public void SetZoomPan(double scaleX, double scaleY, double posX, double posY)
        {

            scaleTransform.ScaleX = scaleX;
            scaleTransform.ScaleY = scaleY;

            translateTransform.X = posX;
            translateTransform.Y = posY;
        }

        public void SetZoomPan(ZoomPan zp)
        {
            SetZoomPan(zp.scaleX, zp.scaleY, zp.posX, zp.posY);
        }

        public ZoomPan GetZoomPan()
        {
            return ZoomPan.GetData(scaleTransform, translateTransform);
        }


        public void ResetZoomPan()
        {
            SetZoomPan(1, 1, 0, 0);
        }

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


        public void MouseWheelZoom(MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? 0.05 : -0.05;
            if (scaleTransform.ScaleX > 0)
                scaleTransform.ScaleX += zoom;
            else
                scaleTransform.ScaleX -= zoom;
            scaleTransform.ScaleY += zoom;
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
    }
}
