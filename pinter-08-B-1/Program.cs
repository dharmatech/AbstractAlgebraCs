using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_08_B_1
{
    class Program
    {
        static void Main(string[] args)
        {
            void proc(GapPerm a)
            {
                WriteLine("a    = {0}", a);
                WriteLine("a^-1 = {0}", a.Inverse());
                WriteLine("a^2  = {0}", a.Compose(a));
                WriteLine("a^3  = {0}", a.Compose(a).Compose(a));
                WriteLine("a^4  = {0}", a.Compose(a).Compose(a).Compose(a));
                WriteLine("a^5  = {0}", a.Compose(a).Compose(a).Compose(a).Compose(a));
            }

            proc(new GapPerm("(123)")); WriteLine("----------------------------------------");
            proc(new GapPerm("(1234)")); WriteLine("----------------------------------------");
            proc(new GapPerm("(123456)")); WriteLine("----------------------------------------");
        }
    }
}
