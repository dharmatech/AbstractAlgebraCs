using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_08_C_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string even_or_odd(GapPerm obj) => obj.ToDisjointCycles().to_transpositions().ls.Count % 2 == 0 ? "even" : "odd";

            even_or_odd(new GapPerm(0, 7, 4, 1, 5, 6, 2, 3, 8).Display()).Display(); WriteLine();
            even_or_odd(new GapPerm("(71864)").Display()).Display(); WriteLine();
            even_or_odd(new GapPerm("(12)(76)(345)").Display()).Display(); WriteLine();
            even_or_odd(new GapPerm("(1276)(3241)(7812)").Display()).Display(); WriteLine();
            even_or_odd(new GapPerm("(123)(2345)(1357)").Display()).Display(); WriteLine();
        }
    }
}
