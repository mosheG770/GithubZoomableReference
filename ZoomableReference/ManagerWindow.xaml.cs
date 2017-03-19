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
        List<FutureWindow> futureWindows = new List<FutureWindow>();
        long listTimeStamp = 0;

        public ManagerWindow()
        {
            Topmost = true;
            Manager = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();
            referenceWindows = new List<ReferenceWindow>();
            Loaded += ManagerWindow_Loaded;
            this.Activated += ManagerWindow_Activated;
        }

        private void ManagerWindow_Activated(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void ManagerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            pw = new ProtectionWindow();
            WindowListBox.ItemsSource = referenceWindows;
        }


        /// <summary>
        /// -- Clear the list, making new items for the list.
        /// Checking with time stamp if there is already updated data (useful if there is lot of windiws)
        /// </summary>
        private void RefreshList()
        {
            var syncCont = SynchronizationContext.Current;
            WindowListBox.ItemsSource = null;
            Task.Run(() =>
            {
                long timeStamp = DateTime.Now.Ticks;
                var results = referenceWindows.Select(o => o.state.GetState())
                    .Concat(futureWindows.Select(t => t.state.GetState()));

                syncCont.Post(o =>
                {
                    if(timeStamp >= listTimeStamp)
                    {
                    WindowListBox.ItemsSource = results;
                        listTimeStamp = timeStamp;
                    }
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
                    item.Show();
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

            //write the list to file, with json/.
            tempStates = states;

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
                foreach (var item in referenceWindows)
                    item.Close();

                referenceWindows.Clear();

                List<State> states = new List<State>();

                var lines = File.ReadAllLines(ofd.FileName);
                states = lines.Select(o => JsonConvert.DeserializeObject<State>(o)).ToList();


                foreach (var item in states)
                {
                    if (item.IsFutureWindow)
                    {
                        FutureWindow fw = new FutureWindow();
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


        //Create section:

        /// <summary>
        /// -- Create Layout window:
        /// </summary>
        private void TestWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            FutureWindow fw = new FutureWindow();
            fw.Show();
            fw.Activate();
            futureWindows.Add(fw);
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
    }
}
