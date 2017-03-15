using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZoomableReference.Model;

namespace ZoomableReference
{
    interface IStateManager
    {
        void SetState(State st);

        string StateToJson();

        State GetState();
    }
}
