using System;

namespace ZoomableReference
{
    internal class RadioButtonManager
    {
        private SettingWindow win;
        public bool IsChangingSettings { get; set; }


        public RadioButtonManager(SettingWindow settingWindow)
        {
            this.win = settingWindow;
        }

        internal void SetReferenceMode(ReferenceWindowMode referenceWindowMode)
        {
            IsChangingSettings = true;

            switch (referenceWindowMode)
            {
                case ReferenceWindowMode.Normal:
                    win.NormalModeRB.IsChecked = true;
                    win.MinimalModeRB.IsChecked = false;
                    win.SimpleModeRB.IsChecked = false;
                    break;
                case ReferenceWindowMode.Minimal:
                    win.NormalModeRB.IsChecked = false;
                    win.MinimalModeRB.IsChecked = true;
                    win.SimpleModeRB.IsChecked = false;
                    break;
                case ReferenceWindowMode.Simple:
                    win.NormalModeRB.IsChecked = false;
                    win.MinimalModeRB.IsChecked = false;
                    win.SimpleModeRB.IsChecked = true;
                    break;
                default:
                    win.NormalModeRB.IsChecked = false;
                    win.MinimalModeRB.IsChecked = false;
                    win.SimpleModeRB.IsChecked = false;
                    break;
            }
            IsChangingSettings = false;
        }

        internal ReferenceWindowMode GetReferenceMode()
        {
            if (win.SimpleModeRB.IsChecked == true)
                return ReferenceWindowMode.Simple;
            if (win.NormalModeRB.IsChecked == true)
                return ReferenceWindowMode.Normal;
            else
                return ReferenceWindowMode.Minimal;
        }
    }
}