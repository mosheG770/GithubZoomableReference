﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    class FocusManager
    {
        ReferenceWindow main;

        private bool isLocked;
        public bool IsLocked
        {
            get { return isLocked; }
            set
            {
                isLocked = value;
                ModeChanged();
                if (isLocked)
                    main.ResizeMode = ResizeMode.NoResize;
                else
                    main.ResizeMode = ResizeMode.CanResize;
            }
        }

        public FocusManager(ReferenceWindow win)
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
            main.LockBtn.Visibility = visi;
            if (IsLocked)
                visi = Visibility.Hidden;


            main.HoriFlipBtn.Visibility = visi;
            main.VertiFlipBtn.Visibility = visi;
            main.HideBtn.Visibility = visi;
            main.RotateModekCB.Visibility = visi;

            if (SettingsManager.ReferenceWindowMode == ReferenceWindowMode.Minimal)
                visi = Visibility.Hidden;
            //Add canvas or grids to select what dissapear when, 
            //to make sure everything is in order is way more complex

            main.QuitBtn.Visibility = visi;
            main.MoveBtn.Visibility = visi;
            main.ResetBtn.Visibility = visi;


            if (visi == Visibility.Hidden)
            {
                main.LoadUrlBtn.Visibility = visi;
                main.UrlPathTxt.Visibility = visi;
            }

            if (SettingsManager.ReferenceWindowMode == ReferenceWindowMode.Simple)
                visi = Visibility.Hidden;//ugly, but do the job better than run 2 times. with visi:hidden and than with visi:visible

            main.ColorBtn.Visibility = visi;
            main.ColorTxt.Visibility = visi;

            main.LoadBtn.Visibility = visi;
            main.UrlBtn.Visibility = visi;
        }

        internal void ModeChanged()
        {
            SetVisibilty(Visibility.Visible);
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
