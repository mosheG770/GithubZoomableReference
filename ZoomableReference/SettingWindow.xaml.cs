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
using System.Windows.Shapes;

namespace ZoomableReference
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        RadioButtonManager radioManager;


        public SettingWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            Loaded += SettingWindow_Loaded;
        }

        /// <summary>
        /// Main use is to set all the settings again.
        /// can't do it with binding because IsChecked could be null and settings are statics
        /// maybe singelton could be useful here.
        /// </summary>
        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            radioManager = new RadioButtonManager(this);

            ActiveFocusCB.IsChecked = SettingsManager.ShowGetFocus;
            radioManager.SetReferenceMode(SettingsManager.ReferenceWindowMode);
        }

        //CheckBox Section.

        private void ActiveFocusCB_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.ShowGetFocus = ActiveFocusCB.IsChecked == true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!radioManager.IsChangingSettings)
            {
                SettingsManager.ReferenceWindowMode = radioManager.GetReferenceMode();
            }
        }
    }
}
