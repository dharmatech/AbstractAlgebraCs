using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;

using AbstractAlgebraFunctionIntInt;

namespace AbstractAlgebra.permutation_
{
    public static class Extensions
    {
        public static string to_string(this List<int> cycle) => "(" + String.Join("", cycle) + ")";

        public static string to_string(this List<List<int>> cycles) => String.Join("", cycles.Select(elt => elt.to_string()));

        public static List<List<int>> display(this List<List<int>> cycles) { cycles.to_string().display(); return cycles; }

        public static List<List<int>> long_display(this List<List<int>> cycles)
        {
            String.Join("   ", cycles.Select(cycle => "(" + String.Join(" ", cycle) + ")")).display();

            return cycles;
        }

        // ----------------------------------------------------------------------

        public static List<List<int>> to_cycles(this string s)
        {
            var result = new List<List<int>>();

            foreach (var elt in s)
            {
                if (elt == '(') result.Add(new List<int>());

                if (Char.IsDigit(elt)) result.Last().Add(elt - '0');
            }

            return result;
        }

        //public static List<int> to_cycle(this string s) => s.to_cycles()[0];

        public static int apply(this List<int> cycle, int a) => cycle.Contains(a) ? cycle[(cycle.IndexOf(a) + 1) % cycle.Count] : a;

        public static int apply_multiple(this List<List<int>> cycles, int a) => cycles.Reverse<List<int>>().Aggregate(a, (elt, cycle) => cycle.apply(elt));

        //public static IEnumerable<(int, int)> to_permutation(this List<List<int>> cycles, int n) =>
        //    Enumerable.Range(1, n).Select(x => (x, cycles.apply_multiple(x)));

        public static FunctionIntInt to_permutation(this List<List<int>> cycles, int n) => 
            new FunctionIntInt(Enumerable.Range(1, n).Select(x => (x, cycles.apply_multiple(x))));

        //public static IEnumerable<(int, int)> to_permutation(this List<List<int>> cycles) => cycles.to_permutation(cycles.SelectMany(elt => elt).Max());

        public static FunctionIntInt to_permutation(this List<List<int>> cycles) => cycles.to_permutation(cycles.SelectMany(elt => elt).Max());

        //public static List<int> get_cycle(this IEnumerable<(int, int)> f, int i)
        //{
        //    var result = new List<int>();

        //    while (true)
        //    {
        //        if (result.Contains(i)) return result;

        //        result.Add(i);

        //        i = f.apply(i);
        //    }
        //}

        public static List<int> get_cycle(this FunctionIntInt f, int i)
        {
            var result = new List<int>();

            while (true)
            {
                if (result.Contains(i)) return result;

                result.Add(i);

                i = f.Apply(i);
            }
        }
        
        //public static List<List<int>> to_disjoint_cycles(this IEnumerable<(int, int)> f) =>

        //    Enumerable.Range(1, f.Select(elt => elt.Item1).Max())
        //        .Aggregate(
        //            new List<List<int>>(),
        //            (cycles, i) =>
        //                cycles.SelectMany(elt => elt).Contains(i) ? cycles :
        //                f.apply(i) == i ? cycles :
        //                cycles.Concat(new[] { f.get_cycle(i) }).ToList());

        public static List<List<int>> to_disjoint_cycles(this FunctionIntInt f) =>
            Enumerable.Range(1, f.ls.Select(elt => elt.Item1).Max())
                .Aggregate(
                    new List<List<int>>(),
                    (cycles, i) =>
                        cycles.SelectMany(elt => elt).Contains(i) ? cycles :
                        f.Apply(i) == i ? cycles :
                        cycles.Concat(new[] { f.get_cycle(i) }).ToList());



        



        public static List<List<int>> to_transpositions(this List<int> cycle) => cycle.Reverse<int>().Skip(1).Select(elt => new List<int> { cycle.Last(), elt }).ToList();

        // public static List<List<int>> to_transpositions(this List<List<int>> cycles) => cycles.SelectMany(cycle => cycle.to_transpositions()).ToList();

        public static List<List<int>> to_transpositions(this List<List<int>> cycles) => cycles.to_disjoint_cycles().SelectMany(cycle => cycle.to_transpositions()).ToList();

        public static List<int> inverse(this List<int> cycle) => cycle.Reverse<int>().ToList();

        public static List<List<int>> pow(this List<int> cycle, int n) => Enumerable.Repeat(cycle, n).ToList();

        public static List<List<int>> to_disjoint_cycles(this List<List<int>> cycles) => cycles.to_permutation().to_disjoint_cycles();

        public static IEnumerable<(int, int)> to_permutation(this string s) =>
                    Enumerable.Range(1, s.Count())
                    .Zip(
                        s.Select(elt => elt - '0'),
                        (k, v) => (key: k, value: v));

        public static IEnumerable<(int, int)> conjugate_permutation(List<int> α, List<int> β) =>
            α.Concat(β.Except(α))
                .Zip(β.Concat(α.Except(β)), (a, b) => (a, b));
    }
}