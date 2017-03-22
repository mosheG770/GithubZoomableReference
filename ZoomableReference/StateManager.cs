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

            if (st.IsLocked)
                main.Commander.Lock();

            main.Top = st.Preset.WindowPosition.X;
            main.Left = st.Preset.WindowPosition.Y;
            main.Width = st.Preset.WindowSize.X;
            main.Height = st.Preset.WindowSize.Y;

            if (!st.IsTemplate)
                main.image.SetZoomPan(st.ZoomPan);
            main.LayoutRoot.Background = st.BackgroundColor;
        }

        public string StateToJson()
        {
            throw new NotImplementedException("We still working on this area.");
        }

        public State GetState()
        {
            State st = new State()
            {
                IsLocked = main.focus.IsLocked,
                imageSource = main.imgHandler.LastURI,

                Preset = new Preset()
                {
                    WindowPosition = new Point(main.Top, main.Left),
                    WindowSize = new Point(main.Width, main.Height)
                },

                ZoomPan = main.image.GetZoomPan(),
                BackgroundColor = main.LayoutRoot.Background,

                Commander = main.Commander
            };
            return st;
        }
    }
}
