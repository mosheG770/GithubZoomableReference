using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZoomableReference.Model;

namespace ZoomableReference
{
    /// <summary>
    /// Interaction logic for FutureWindow.xaml
    /// </summary>
    public partial class FutureWindow : Window
    {
        public bool IsShowing { get; set; }
        internal LayoutStateManager state { get; set; }
        internal State PreloadState { get; set; }
        internal ImageHandler imgHandler { get; set; }

        public FutureWindow()
        {
            InitializeComponent();
            ContentRendered += FutureWindow_ContentRendered;
            Closed += FutureWindow_Closed;
            KeyDown += FutureWindow_KeyDown;
            KeyUp += FutureWindow_KeyUp;
        }

        private void FutureWindow_ContentRendered(object sender, EventArgs e)
        {
            imgHandler = new ImageHandler(image);

            IsShowing = true;
            image.MoveBorder = MoveBorder;
            image.MyBorder = border;

            WindowState = WindowState.Maximized;
            this.Topmost = true;

            state = new LayoutStateManager(this);

            if (PreloadState != null)
                state.SetState(PreloadState);
        }

        
        public const int WS_EX_TRANSPARENT = 0x00000020; public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        int standart = 0;
        public void SetSolid()
        {
            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            // Change the extended window style to include WS_EX_TRANSPARENT
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            standart = extendedStyle;
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            ToolsGrid.Visibility = Visibility.Hidden;
        }

        #region events region
        private void FutureWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                border.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void FutureWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                border.Background = new SolidColorBrush(Color.FromArgb(1, 1, 1, 1));
        }


        private void FutureWindow_Closed(object sender, EventArgs e)
        {
            IsShowing = false;
        }
#endregion


        public void SetSoft()
        {
            // Get this window's handle         
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            // Change the extended window style to include WS_EX_TRANSPARENT         
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            SetWindowLong(hwnd, GWL_EXSTYLE, standart);
            ToolsGrid.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetSolid();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            imgHandler.BrowseImage();
        }



        private void border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image.MouseLeftUp();
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            image.MouseLeftDown(e);
        }

        private void border_MouseMove(object sender, MouseEventArgs e)
        {
            image.MouseMoveFunc(e);
        }

        private void border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            image.MouseWheelZoom(e);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            image.ResetZoomPan();
        }

        private void RotateModekCB_Checked(object sender, RoutedEventArgs e)
        {
            image.IsRotateMode = RotateModekCB.IsChecked == true;
        }

        private void HideBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void VertiFlipBtn_Click(object sender, RoutedEventArgs e)
        {
            image.VerticalFlip();
        }

        private void HoriFlipBtn_Click(object sender, RoutedEventArgs e)
        {
            image.HorizontalFlip();
        }
    }
}
