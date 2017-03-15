using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<MainWindow> listMainWindow;
        List<FutureWindow> futureWindows = new List<FutureWindow>();

        public ManagerWindow()
        {
            Manager = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            InitializeComponent();
            listMainWindow = new List<MainWindow>();
            Loaded += ManagerWindow_Loaded;
        }

        private void ManagerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            pw = new ProtectionWindow();
            int[] arr = new int[1];
            arr = arr.OrderBy(o => o).ToArray();
        }

        //State Section:

        /// <summary>
        /// -- Save the state of the windows:
        /// </summary>
        private void SaveStateBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
            {
                if (item.IsShowing)
                    item.Show();
            }

            List<State> states = new List<State>();
            foreach (var item in listMainWindow)
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
                foreach (var item in listMainWindow)
                    item.Close();

                listMainWindow.Clear();

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
                        MainWindow mw = new MainWindow();
                        mw.PreloadState = item;
                        mw.Show();
                        listMainWindow.Add(mw);
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
            foreach (var item in listMainWindow)
                item.Close();
            foreach (var item in futureWindows)
                item.Close();

            listMainWindow.Clear();
            futureWindows.Clear();
        }

        /// <summary>
        /// -- Hide all the windows:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
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
            foreach (var item in listMainWindow)
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.Activate();
            listMainWindow.Add(mw);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimpleModeMI_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManager.IsSimpleMode = (SimpleModeMI.IsChecked == true);
        }
    }
}
