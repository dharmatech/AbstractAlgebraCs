using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_08_F_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find the order of each of the following permutations

            int proc(GapPerm a)
            {
                var result = new GapPerm();

                foreach (var n in Enumerable.Range(1, 100))
                {
                    result = result.Compose(a);

                    WriteLine("a^{0} = {1}", n, result);

                    if (result == new GapPerm(0))
                        return n;
                }

                throw new Exception();
            }

            proc(new GapPerm("(12)(345)")).Display(); WriteLine();
            proc(new GapPerm("(12)(3456)")).Display(); WriteLine();
            proc(new GapPerm("(1234)(56789)")).Display(); WriteLine();

        }
    }
}
