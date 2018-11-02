using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

using AbstractAlgebraGapPerm;

using static System.Console;

using static AbstractAlgebraSymmetricGroup.Utils;

namespace S4SubgroupsGapPerm
{
    class Program
    {
        static void Main(string[] args)
        {
            bool is_subgroup(IEnumerable<GapPerm> set)
            {
                foreach (var a in set)
                    foreach (var b in set)
                        if (set.Contains(a.Compose(b)) == false) return false;

                return true;
            }

            int NumberOfSetBits(int i)
            {
                i = i - ((i >> 1) & 0x55555555);
                i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
                return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
            }

            List<List<GapPerm>> power_set_divisible_size(IEnumerable<GapPerm> set_)
            {
                var set = set_.ToList();

                var power_set_size = Math.Pow(2, set.Count());

                var subsets = new List<List<GapPerm>>();

                for (var counter = 1; counter < power_set_size; counter++)
                {
                    if (set.Count % NumberOfSetBits(counter) == 0)
                    {
                        var subset = new List<GapPerm>();

                        for (var i = 0; i < set.Count(); i++)
                            if ((counter & (1 << i)) != 0)
                                subset.Add(set[i]);

                        subsets.Add(subset);
                    }
                }

                return subsets;
            }

            var G = SymmetricGroupGapPerm(4);

            var stopwatch = new Stopwatch(); stopwatch.Start();

            var ident = G.ElementAt(0);

            var result =
                power_set_divisible_size(G)
                .AsParallel()
                .Where(set => set.Contains(ident))
                .Where(is_subgroup)
                .Count();

            stopwatch.Stop();

            WriteLine("count: {0}   elapsed: {1}", result, stopwatch.Elapsed); // count: 30   elapsed: 00:00:39.0476801
                                                                               // count: 30   elapsed: 00:00:30.2906193
        }
    }
}
