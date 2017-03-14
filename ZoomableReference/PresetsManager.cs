using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZoomableReference
{
    class PresetsManager
    {
        MainWindow main;
        List<WindowPositionSizeArgs> argsList;
        PresetsFile presetsFile;

        public PresetsManager(MainWindow win)
        {
            main = win;
            presetsFile = new PresetsFile();
            
            argsList = presetsFile.LoadFromFile().ToList();
            argsList.ForEach(o => { main.SizeCB.Items.Add(o); });
        }

        public void SetPreset()
        {
            var args = (WindowPositionSizeArgs)main.SizeCB.SelectedItem;
            if (args == null)
                return;


            main.Top = args.Top;
            main.Left = args.Left;
            main.Height = args.Height;
            main.Width = args.Width;
        }

        public void SaveNew()
        {
            var args = new WindowPositionSizeArgs();
            args.Name = main.SizeNameTxt.Text;
            args.Top = main.Top;
            args.Left = main.Left;
            args.Height = main.Height;
            args.Width = main.Width;

            //args.Preset = new Model.Preset()
            //{
            //    WindowPosition = new Point(main.Top, main.Left),
            //    WindowSize = new Point(main.Width, main.Height)
            //};

            argsList.Add(args);
            main.SizeCB.Items.Add(args);

            presetsFile.SaveToFile(argsList);
        }
    }
}
