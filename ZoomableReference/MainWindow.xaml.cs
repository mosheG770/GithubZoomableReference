using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZoomableReference.Model;

namespace ZoomableReference
{
    public partial class MainWindow : Window
    {
        internal ImageHandler imgHandler;

        FocusManager focus;
        DragManager drag;
        PresetsManager presets;
        internal StateManager state;

        internal State PreloadState { get; set; }
        public bool IsShowing { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            IsShowing = false;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IsShowing = true;
            image.MyBorder = border;

            imgHandler = new ImageHandler(image);
            imgHandler.SourceChange += ImgHandler_SourceChange;
            imgHandler.InitatedLoad();


            focus = new FocusManager(this);
            drag = new DragManager(this);
            presets = new PresetsManager(this);

            state = new StateManager(this);

            if (PreloadState != null)
                state.SetState(PreloadState);
        }

        private void ImgHandler_SourceChange()
        {
            image.ResetZoomPan();
        }


        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            imgHandler.BrowseImage();
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UrlBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadUrlBtn.Visibility = Visibility.Visible;
            UrlPathTxt.Visibility = Visibility.Visible;
        }

        private void LoadUrlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(UrlPathTxt.Text))
                return;

            imgHandler.LoadImage(UrlPathTxt.Text);
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            drag.LoadDrag(sender, e, imgHandler.LoadImage);
        }

        private void MoveBtn_Click(object sender, RoutedEventArgs e)
        {
            focus.ToggleStyle();
        }

        private void ColorBtn_Click(object sender, RoutedEventArgs e)
        {
            string color = ColorTxt.Text;
            if (color[0] != '#')
                color = color.Insert(0, "#");

            if (color.Length == 7)
                LayoutRoot.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
        }

        private void SaveSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            presets.SaveNew();
        }

        private void LoadSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            presets.SetPreset();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            image.ResetZoomPan();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            image.HorizontalFlip();
        }



        private void eventBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            image.MouseLeftDown(e);
        }

        private void eventBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image.MouseLeftUp();
        }

        private void eventBorder_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            image.MouseWheelZoom(e);
        }

        private void eventBorder_MouseMove(object sender, MouseEventArgs e)
        {
            image.MouseMoveFunc(e);
        }

        private void VertiFlipBtn_Click(object sender, RoutedEventArgs e)
        {
            image.VerticalFlip();
        }

        private void HideBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                RotateModekCB.IsChecked = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                RotateModekCB.IsChecked = false;
        }

        private void RotateModekCB_Checked(object sender, RoutedEventArgs e)
        {
            image.IsRotateMode = RotateModekCB.IsChecked == true;
        }
    }
}
