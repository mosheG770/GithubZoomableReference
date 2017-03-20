using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZoomableReference.Model;

namespace ZoomableReference
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public static ManagerWindow Manager { get; private set; }

        ProtectionWindow pw;
        List<State> tempStates;
        public List<ReferenceWindow> referenceWindows;
        List<LayoutWindow> futureWindows = new List<LayoutWindow>();
        long listTimeStamp = 0;
        Dictionary<State, Window> windowDictionary = new Dictionary<State, Window>();

        public ManagerWindow()
        {
            Topmost = true;
            Manager = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();

            ContentRendered += ManagerWindow_ContentRendered;
            this.Activated += ManagerWindow_Activated;
        }

        private void ManagerWindow_ContentRendered(object sender, EventArgs e)
        {
            referenceWindows = new List<ReferenceWindow>();


            pw = new ProtectionWindow();
            WindowListBox.ItemsSource = referenceWindows;

            //Handle when start the program by drag .zrf on the icon.
            var lines = Environment.GetCommandLineArgs();
            if (lines.Length == 2)
            {
                FileInfo f = new FileInfo(lines[1]);
                if (f.Extension.ToLower() == ".zrf")
                    LoadState(lines[1]);
            }
        }

        /// <summary>
        /// -- every time the manager is in focus again, refresh the list of the windows.
        /// </summary>
        private void ManagerWindow_Activated(object sender, EventArgs e)
        {
            RefreshList();
        }


        /// <summary>
        /// -- Clear the list, making new items for the list.
        /// Checking with time stamp if there is already updated data (useful if there is lot of windiws)
        /// </summary>
        private void RefreshList()
        {
            RefreshBtn.IsEnabled = false;
            var syncCont = SynchronizationContext.Current;
            WindowListBox.ItemsSource = null;
            Task.Run(() =>
            {
                long timeStamp = DateTime.Now.Ticks;


                var results = referenceWindows.Where(o => o.IsShowing).Select(o => o.state.GetState())
                            .Concat(futureWindows.Where(t => t.IsShowing).Select(t => t.state.GetState()));

                syncCont.Post(o =>
                {
                    if (timeStamp >= listTimeStamp)
                    {
                        WindowListBox.ItemsSource = results;
                        listTimeStamp = timeStamp;
                    }

                    RefreshBtn.IsEnabled = true;
                }, null);
            });

        }


        //State Section:

        /// <summary>
        /// -- Save the state of the windows:
        /// </summary>
        private void SaveStateBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
            {
                if (item.IsShowing)
                {
                    item.Commander.Show();
                    item.Show();
                }
            }

            List<State> states = new List<State>();
            foreach (var item in referenceWindows)
                if (item.IsShowing)
                    states.Add(item.state.GetState());

            foreach (var item in futureWindows)
                if (item.IsShowing)
                    states.Add(item.state.GetState());

            HideAllBtn_Click(null, null);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ZoomableReferenceFile .zrf|*.zrf";
            if (sfd.ShowDialog() == true)
            {
                var lines = states.Select(j => JsonConvert.SerializeObject(j));
                File.WriteAllLines(sfd.FileName, lines);
            }

            ShowAllBtn_Click(null, null);//Show all the windows again
            //write the list to file, with json/.
            tempStates = states;

        }

        /// <summary>
        /// -- Save state as Template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
            {
                if (item.IsShowing)
                {
                    item.Commander.Show();
                    item.Show();
                }
            }

            List<State> states = new List<State>();
            foreach (var item in referenceWindows)
                if (item.IsShowing)
                {
                    var state = item.state.GetState();
                    state.IsTemplate = true;
                    state.imageSource = "";
                    states.Add(state);
                }

            foreach (var item in futureWindows)
                if (item.IsShowing)
                {
                    var state = item.state.GetState();
                    state.IsTemplate = true;
                    states.Add(state);
                }

            HideAllBtn_Click(null, null);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ZoomableReferenceFile .zrf|*.zrf";
            sfd.Title = "Save .zrf Template";
            if (sfd.ShowDialog() == true)
            {
                var lines = states.Select(j => JsonConvert.SerializeObject(j));
                File.WriteAllLines(sfd.FileName, lines);
            }

            ShowAllBtn_Click(null, null);
        }

        /// <summary>
        /// -- Load state:
        /// </summary>
        private void LoadStateBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "ZoomableReferenceFile .zrf|*.zrf"
            };

            if (ofd.ShowDialog() == true)
            {
                LoadState(ofd.FileName);
            }

            ShowAllBtn_Click(null, null);
        }

        private void LoadState(string filePath)
        {
            foreach (var item in referenceWindows)
                item.Close();

            referenceWindows.Clear();

            List<State> states = new List<State>();

            var lines = File.ReadAllLines(filePath);
            states = lines.Select(o => JsonConvert.DeserializeObject<State>(o)).ToList();


            foreach (var item in states)
            {
                if (item.IsFutureWindow)
                {
                    LayoutWindow fw = new LayoutWindow();
                    fw.PreloadState = item;
                    fw.Show();
                    futureWindows.Add(fw);
                }
                else
                {
                    ReferenceWindow mw = new ReferenceWindow();
                    mw.PreloadState = item;
                    mw.Show();
                    referenceWindows.Add(mw);
                }
            }
        }


        //Command section:

        /// <summary>
        /// -- Close all the windows:
        /// </summary>
        private void CloseAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
                item.Close();
            foreach (var item in futureWindows)
                item.Close();

            referenceWindows.Clear();
            futureWindows.Clear();
        }

        /// <summary>
        /// -- Hide all the windows:
        /// </summary>
        private void HideAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
                if (item.IsShowing)
                    item.WindowState = WindowState.Minimized;

            foreach (var item in futureWindows)
                if (item.IsShowing)
                    item.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// -- Show all the windows:
        /// </summary>
        private void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
                if (item.IsShowing)
                    item.WindowState = WindowState.Normal;

            foreach (var item in futureWindows)
                if (item.IsShowing)
                    item.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// -- Set all the layout windows status to: Soft
        /// </summary>
        private void TestWindowSoftBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in futureWindows)
                item.SetSoft();
        }

        /// <summary>
        /// -- Lock all windows
        /// </summary>
        private void LockAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
                item.Commander.Lock();
        }

        /// <summary>
        /// -- Unlock all windows
        /// </summary>
        private void UnlockAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in referenceWindows)
                item.Commander.Unlock();
        }


        //Create window Section:

        /// <summary>
        /// -- Create Layout window:
        /// </summary>
        private void TestWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            LayoutWindow fw = new LayoutWindow();
            fw.Show();
            fw.Activate();
            futureWindows.Add(fw);
            RefreshList();
        }

        /// <summary>
        /// -- Create Reference window:
        /// </summary>
        private void CreateWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            ReferenceWindow mw = new ReferenceWindow();
            mw.Show();
            mw.Activate();
            referenceWindows.Add(mw);
            RefreshList();
        }


        /// <summary>
        /// -- Create Protection window:
        /// </summary>
        private void ProtectionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!pw.IsShowing)
            {
                pw = new ProtectionWindow();
                pw.Show();
            }
            else
                pw.Close();
        }


        /// <summary>
        /// -- Simple mode: toggle by the CheckBox
        /// </summary>
        private void SimpleModeMI_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.IsSimpleMode = (SimpleModeMI.IsChecked == true);
        }

        /// <summary>
        /// -- Refresh the list of the windows
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }


        //Item commands section
        /// <summary>
        /// -- Close selected window
        /// </summary>
        private void CloseItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowListBox.SelectedItem != null)
            {
                ((State)WindowListBox.SelectedItem).Commander.Close();
                RefreshList();
            }
        }

        /// <summary>
        /// -- Minimize selected window
        /// </summary>
        private void MinimizeItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowListBox.SelectedItem != null)
                ((State)WindowListBox.SelectedItem).Commander.Minimize();
        }

        /// <summary>
        /// -- Show selected window
        /// </summary>
        private void ShowItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowListBox.SelectedItem != null)
                ((State)WindowListBox.SelectedItem).Commander.Show();
        }

        /// <summary>
        /// -- Toggle lock on selected item
        /// </summary>
        private void ToggleItemLockBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowListBox.SelectedItem != null)
            {
                var selectedState = ((State)WindowListBox.SelectedItem);
                if (selectedState.Commander is ReferenceWindowCommander)
                    ((ReferenceWindowCommander)selectedState.Commander).ToggleLock();
            }
        }


        //Drag and drop in manager window Section.

        private void Border_Drop(object sender, DragEventArgs e)
        {

        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
