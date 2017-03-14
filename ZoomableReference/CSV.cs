using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    /// <summary>
    /// The Format: Name,Top,Left,Height,Width
    /// </summary>
    static class CSV
    {
        public static WindowPositionSizeArgs ToWindowArgs(string[] fragments)
        {
            WindowPositionSizeArgs args = new WindowPositionSizeArgs();

            args.Name = fragments[0];
            args.Top = int.Parse(fragments[1]);
            args.Left = int.Parse(fragments[2]);
            args.Height = int.Parse(fragments[3]);
            args.Width = int.Parse(fragments[4]);

            return args;
        }

        public static WindowPositionSizeArgs ToWindowArgs(string line)
        {
            return ToWindowArgs(line.Split(','));
        }

        internal static string[] FromWindowArgs(WindowPositionSizeArgs args)
        {
            string[] fragments = new string[5];

            fragments[0] = args.Name + ",";
            fragments[1] = args.Top.ToString() + ",";
            fragments[2] = args.Left.ToString() + ",";
            fragments[3] = args.Height.ToString() + ",";
            fragments[4] = args.Width.ToString();

            return fragments;
        }

        internal static string[] ToStringArray(IList<WindowPositionSizeArgs> argsList)
        {
            string[] result = new string[argsList.Count];
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < argsList.Count; i++)
            {
                var fragments = FromWindowArgs(argsList[i]);

                sb.Clear();
                foreach (var item in fragments)
                    sb.Append(item);

                result[i] = sb.ToString();
            }

            return result;
        }
    }
}
