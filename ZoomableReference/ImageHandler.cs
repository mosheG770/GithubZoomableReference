using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ZoomableReference
{
    /// <summary>
    /// Handle the first load and others loads.
    /// </summary>
    class ImageHandler
    {
        private OpenFileDialog ofd;

        public Image img { get; set; }

        public event Action SourceChange;
        public string LastURI { get; private set; }

        public ImageHandler(Image image)
        {
            img = image;

            ofd = new OpenFileDialog();
            ofd.Filter = "Image file|*.jpg;*.jpeg;*.png";
        }


        public void LoadImage(string filePath)
        {
            try // Temp solution, fix ASAP!
            {
                LastURI = filePath;
                img.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                SourceChange();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Error in the link.");
            }
        }

        internal void InitatedLoad()
        {
            var arguments = Environment.GetCommandLineArgs();
            if (arguments.Length == 2)
            {
                try
                {
                    LoadImage(arguments[1]);
                }
                catch (Exception) { }
            }
        }

        internal void BrowseImage()
        {
            if (ofd.ShowDialog() == true)
            {
                LoadImage(ofd.FileName);
            }
        }
    }
}
