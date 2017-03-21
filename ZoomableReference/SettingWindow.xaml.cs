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
        public SettingWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            ContentRendered += SettingWindow_ContentRendered;
        }

        private void SettingWindow_ContentRendered(object sender, EventArgs e)
        {
            ActiveFocusCB.IsChecked = SettingsManager.ShowGetFocus;
            SimpleModeCB.IsChecked = SettingsManager.IsSimpleMode;
        }

        private void SimpleModeCB_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.IsSimpleMode = SimpleModeCB.IsChecked == true;
        }

        private void ActiveFocusCB_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.ShowGetFocus = ActiveFocusCB.IsChecked == true;
        }
    }
}
