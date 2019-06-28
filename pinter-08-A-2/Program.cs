using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_08_A_2
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var permutation = new GapPerm(0, 4, 9, 2, 5, 1, 7, 6, 8, 3);

                permutation.DisplayAsFunction();

                permutation.ToDisjointCycles().Display();

                WriteLine();
            }

            {
                var permutation = new GapPerm(0, 7, 4, 9, 2, 3, 8, 1, 6, 5);

                permutation.DisplayAsFunction();

                permutation.ToDisjointCycles().Display();

                WriteLine();
            }

            {
                var permutation = new GapPerm(0, 7, 9, 5, 3, 1, 2, 4, 8, 6);

                permutation.DisplayAsFunction();

                permutation.ToDisjointCycles().Display();

                WriteLine();
            }

            {
                var permutation = new GapPerm(0, 9, 8, 7, 4, 3, 6, 5, 1, 2);

                permutation.DisplayAsFunction();

                permutation.ToDisjointCycles().Display();

                WriteLine();
            }
        }
    }
}
