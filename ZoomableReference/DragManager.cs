using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ZoomableReference
{
    class DragManager
    {
        Window main;

        public DragManager(Window win)
        {
            main = win;
        }

        public void LoadDrag(object sender, DragEventArgs e, Action<string> ImageLoad)
        {
            var formats = e.Data.GetFormats();
            //MessageBox.Show(formats.ToOneString());

            if (formats.Contains(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                ImageLoad(files[0]);

            }
            else if (formats.Contains("text/html"))
            {
                if (TryGetLinkTextHtml(e.Data.GetData("text/html"), out var link))
                    ImageLoad(link);

            }
            else if (formats.Contains(DataFormats.StringFormat))
            {
                string data = (string)e.Data.GetData(DataFormats.StringFormat);
                ImageLoad(data);
            }
            else
            {
                var something = new StringBuilder();
                something.AppendFormat(Environment.NewLine, something);

                MessageBox.Show(something.ToString());
            }
        }


        private bool TryGetLinkTextHtml(object obj, out string result)
        {
            string html = string.Empty;
            if (obj is string)
            {
                html = (string)obj;
            }
            else if (obj is MemoryStream)
            {
                MemoryStream ms = (MemoryStream)obj;
                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, (int)ms.Length);
                if (buffer[1] == (byte)0)  // Detecting unicode
                {
                    html = Encoding.Unicode.GetString(buffer);
                }
                else
                {
                    html = Encoding.ASCII.GetString(buffer);
                }
            }

            var match = Regex.Match(html, @"src=""([^ "">]+)""").Groups[1];
            MessageBox.Show(match.Value);

            if (match.Success)
            {
                result = match.Value;
                return true;
            }

            result = "";
            return false;
        }
    }
}
