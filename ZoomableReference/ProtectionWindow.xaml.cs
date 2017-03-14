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
    /// Interaction logic for ProtectionWindow.xaml
    /// </summary>
    public partial class ProtectionWindow : Window
    {
        public bool IsShowing { get; set; }

        public ProtectionWindow()
        {
            InitializeComponent();
            Loaded += ProtectionWindow_Loaded;
            Deactivated += ProtectionWindow_Deactivated;
            Closed += ProtectionWindow_Closed;
        }

        private void ProtectionWindow_Closed(object sender, EventArgs e)
        {
            IsShowing = false;
        }

        private void ProtectionWindow_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
        }

        private void ProtectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IsShowing = true;

            WindowStyle = WindowStyle.None;
            Top = 0;
            Left = 1300;
            Height = 32;
            Width = 32;
        }
    }
}
