using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoomableReference.Model;

namespace ZoomableReference
{
    class LayoutStateManager : IStateManager
    {
        FutureWindow win;
        public LayoutStateManager(FutureWindow future)
        {
            win = future;
        }

        public State GetState()
        {
            State st = new State();
            st.IsFutureWindow = true;
            st.imageSource = win.imgHandler.LastURI;
            st.ZoomPan = win.image.GetZoomPan();

            return st;
        }

        public void SetState(State st)
        {
            win.imgHandler.LoadImage(st.imageSource);

            win.image.SetZoomPan(st.ZoomPan);
        }

        public string StateToJson()
        {
            throw new NotImplementedException("We still working on this area.");
        }
    }
}
