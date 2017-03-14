using System;
using System.Linq;
using System.Text;

namespace ZoomableReference
{
    internal class ImageRotator
    {
        private MainWindow main;

        public ImageRotator(MainWindow mainWindow)
        {
            this.main = mainWindow;
        }

        public void RotateClock()
        {
            main.image.RotateClock();
        }
    }
}