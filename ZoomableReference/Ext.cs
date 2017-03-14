using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomableReference
{
    public static class Ext
    {
        public static string ToOneString(this string[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in arr)
            {
                sb.Append(item);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
