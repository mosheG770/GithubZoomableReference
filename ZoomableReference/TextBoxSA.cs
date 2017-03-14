using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZoomableReference
{
    /// <summary>
    /// TextBox that select all text on double click on it.
    /// </summary>
    class TextBoxSA : TextBox
    {
        public TextBoxSA()
        {
            MouseDoubleClick += TextBoxSA_MouseDoubleClick;
        }

        private void TextBoxSA_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                tb.SelectAll();
            }));
        }
    }
}
