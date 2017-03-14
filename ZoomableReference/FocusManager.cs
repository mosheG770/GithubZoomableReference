using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    class FocusManager
    {
        MainWindow main;
        public FocusManager(MainWindow win)
        {
            main = win;

            main.Activated += ((e, sender) => { SetVisibilty(Visibility.Visible); });
            main.Deactivated += ((e, sender) =>
            {
                main.Topmost = true;
                SetVisibilty(Visibility.Hidden);
            });


            main.WindowStyle = WindowStyle.None;
            SetVisibilty(Visibility.Hidden);
            SetVisibilty(Visibility.Visible);
        }

        /// <summary>
        /// Set the visibilty of various elements of the main window.
        /// </summary>
        /// <param name="visi"></param>
        public void SetVisibilty(Visibility visi)
        {
            main.QuitBtn.Visibility = visi;
            main.ColorBtn.Visibility = visi;
            main.MoveBtn.Visibility = visi;
            main.ColorTxt.Visibility = visi;

            main.LoadBtn.Visibility = visi;
            main.UrlBtn.Visibility = visi;
            main.ResetBtn.Visibility = visi;

            main.HoriFlipBtn.Visibility = visi;
            main.VertiFlipBtn.Visibility = visi;
            main.HideBtn.Visibility = visi;
            main.RotateClockBtn.Visibility = visi;

            main.SizeCanvas.Visibility = visi;

            if (visi == Visibility.Hidden)
            {
                main.LoadUrlBtn.Visibility = visi;
                main.UrlPathTxt.Visibility = visi;
            }
        }


        /// <summary>
        /// Toggle the style of the window between SingleBorderWindow to None.
        /// </summary>
        public void ToggleStyle()
        {
            if (main.WindowStyle == WindowStyle.None)
                main.WindowStyle = WindowStyle.SingleBorderWindow;
            else
                main.WindowStyle = WindowStyle.None;
        }
    }
}
