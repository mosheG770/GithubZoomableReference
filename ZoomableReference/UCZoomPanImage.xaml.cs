using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZoomableReference
{
    /// <summary>
    /// Interaction logic for UCZoomPanImage.xaml
    /// </summary>
    public partial class UCZoomPanImage : UserControl
    {
        TransformGroup transformGroup = new TransformGroup();
        ScaleTransform scaleTransform = new ScaleTransform();
        TranslateTransform translateTransform = new TranslateTransform();

        private Point origin;
        private Point start;

        public ImageSource Source { get { return image.Source; } set { image.Source = value; } }

        public UCZoomPanImage()
        {
            InitializeComponent();

            Loaded += UCZoomPanImage_Loaded;

        }

        private void UCZoomPanImage_Loaded(object sender, RoutedEventArgs e)
        {
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);

            image.RenderTransform = transformGroup;

            image.MouseWheel += image_MouseWheel;
            image.MouseLeftButtonDown += image_MouseLeftButtonDown;
            image.MouseLeftButtonUp += image_MouseLeftButtonUp;
            image.MouseMove += image_MouseMove;
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? .2 : -.2;
            scaleTransform.ScaleX += zoom;
            scaleTransform.ScaleY += zoom;
        }


        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMouseCaptured) return;

            Vector v = start - e.GetPosition(border);
            translateTransform.X = origin.X - v.X;
            translateTransform.Y = origin.Y - v.Y;
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            start = e.GetPosition(border);
            origin = new Point(translateTransform.X, translateTransform.Y);
        }
    }
}
