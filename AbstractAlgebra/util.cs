using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace AbstractAlgebraUtil
{
    public static class Extensions
    {
        public static T call<T>(this T obj, Action<T> proc)
        {
            proc(obj);

            return obj;
        }

        public static T Display<T>(this T obj) { WriteLine(obj); return obj; }
    }
}
