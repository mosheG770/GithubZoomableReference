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
        public List<MainWindow> listMainWindow;
        List<State> tempStates;
        List<FutureWindow> futureWindow = new List<FutureWindow>();

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
        }

        private void CreateWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.Activate();
            listMainWindow.Add(mw);
        }

        private void CloseAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
            {
                item.Close();
            }
            listMainWindow.Clear();
        }

        private void ProtectionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!pw.IsShowing)
            {
                pw = new ProtectionWindow();
                pw.Show();
            }
            else
            {
                pw.Close();
            }
        }

        private void SaveStateBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
            {
                if (item.IsShowing)
                    item.Show();
            }

            List<State> states = new List<State>();
            foreach (var item in listMainWindow)
            {
                if (item.IsShowing)
                    states.Add(item.state.GetState());
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ZoomableReferenceFile .zrf|*.zrf";
            if (sfd.ShowDialog() == true)
            {
                var lines = states.Select(j => JsonConvert.SerializeObject(j));
                File.WriteAllLines(sfd.FileName, lines);
            }

            //write all the list to file, with json/.
            tempStates = states;

        }

        private void LoadStateBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ZoomableReferenceFile .zrf|*.zrf";
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
                    MainWindow mw = new MainWindow();
                    mw.PreloadState = item;
                    mw.Show();
                    listMainWindow.Add(mw);
                }
            }
        }

        private void HideAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
            {
                if (item.IsShowing)
                    item.WindowState = WindowState.Minimized;
            }
        }

        private void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listMainWindow)
            {
                if (item.IsShowing)
                    item.WindowState = WindowState.Normal;
            }
        }

        private void TestWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            FutureWindow fw = new FutureWindow();
            fw.Show();
            fw.Activate();
            futureWindow.Add(fw);
        }

        private void TestWindowSoftBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in futureWindow)
            {
                item.SetSoft();
            }
        }
    }
}
