using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ZoomableReference.Model;

namespace ZoomableReference
{
    class StateManager : IStateManager
    {
        ReferenceWindow main;
        public StateManager(ReferenceWindow mw)
        {
            main = mw;
        }

        public void SetState(State st)
        {
            main.imgHandler.LoadImage(st.imageSource);

            main.Top = st.Preset.WindowPosition.X;
            main.Left = st.Preset.WindowPosition.Y;
            main.Width = st.Preset.WindowSize.X;
            main.Height = st.Preset.WindowSize.Y;

            main.image.SetZoomPan(st.ZoomPan);
            main.LayoutRoot.Background = st.BackgroundColor;
        }

        public string StateToJson()
        {  
            throw new NotImplementedException("We still working on this area.");
        }

        public State GetState()
        {
            State st = new State();
            st.imageSource = main.imgHandler.LastURI;

            st.Preset = new Preset()
            {
                WindowPosition = new Point(main.Top, main.Left),
                WindowSize = new Point(main.Width, main.Height)
            };

            st.ZoomPan = main.image.GetZoomPan();
            st.BackgroundColor = main.LayoutRoot.Background;

            return st;
        }
    }
}
