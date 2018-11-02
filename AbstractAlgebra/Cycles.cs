using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraFunctionIntInt;
// using AbstractAlgebraGapPerm;

namespace AbstractAlgebraCycles
{
    public static class Extensions
    {
        public static Cycle ToCycle(this IEnumerable<int> seq) => new Cycle(seq);

        public static Cycles ToCycles(this IEnumerable<Cycle> seq) => new Cycles(seq);

        public static Cycle to_cycle(this string s) => Cycles.from_string(s).ElementAt(0).ToCycle();

        public static Cycle get_cycle_alt(this FunctionIntInt f, int i)
        {
            var result = new List<int>();

            while (true)
            {
                if (result.Contains(i)) return new Cycle(result);

                result.Add(i);

                i = f.Apply(i);
            }
        }

        public static Cycles to_disjoint_cycles_alt(this FunctionIntInt f) =>
            Enumerable.Range(1, f.ls.Select(elt => elt.Item1).Max())
                .Aggregate(
                    new Cycles(),
                    (cycles, i) =>
                        cycles.SelectMany(elt => elt).Contains(i) ? cycles :
                        f.Apply(i) == i ? cycles :
                        new Cycles(cycles.Concat(new[] { f.get_cycle_alt(i) })));



        //

        //public static Cycle GetCycle(this GapPerm f, int i)
        //{
        //    var result = new List<int>();

        //    while (true)
        //    {
        //        if (result.Contains(i)) return new Cycle(result);

        //        result.Add(i);

        //        i = f.Apply(i);
        //    }
        //}

        //public static Cycles ToDisjointCycles(this GapPerm f)
        //{
        //    return Enumerable.Range(1, f.arr.Count() - 1)
        //        .Aggregate(
        //            new Cycles(),
        //            (cycles, i) =>
        //                cycles.SelectMany(elt => elt).Contains(i) ? cycles :
        //                f.Apply(i) == i ? cycles :
        //                new Cycles(cycles.Concat(new[] { f.GetCycle(i) })));
        //}
    }


    // https://stackoverflow.com/questions/2495791/custom-collection-initializers

    public class Cycle : IEnumerable<int>
    {
        public List<int> ls;

        public IEnumerator<int> GetEnumerator() => ls.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ls.GetEnumerator();

        public Cycle() => ls = new List<int>();

        public Cycle(IEnumerable<int> seq) => ls = seq.ToList();

        public Cycle(params int[] items) => ls = items.ToList();

        public override string ToString() => "(" + String.Join("", ls.Select(elt => elt.ToString())) + ")";

        public void Add(int i) => ls.Add(i);

        public Cycles to_transpositions() => ls.Reverse<int>().Skip(1).Select(elt => new Cycle { ls.Last(), elt }).ToCycles();

        public Cycle inverse() => ls.Reverse<int>().ToCycle();

        public int apply(int a) => ls.Contains(a) ? ls[(ls.IndexOf(a) + 1) % ls.Count] : a;

    }

    public class Cycles : IEnumerable<Cycle>
    {
        public List<Cycle> ls;
        public IEnumerator<Cycle> GetEnumerator() => ls.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ls.GetEnumerator();

        public Cycles() { ls = new List<Cycle>(); }

        public Cycles(IEnumerable<Cycle> seq) => ls = seq.ToList();

        public Cycles(params Cycle[] items) => ls = items.ToList();

        public static Cycles from_string(string s)
        {
            var result = new Cycles();

            foreach (var elt in s)
            {
                if (elt == '(') result.Add(new Cycle());

                if (Char.IsDigit(elt)) result.Last().ls.Add(elt - '0');
            }

            return result;
        }


        // public static Cycles from_FunctionIntInt


        // public override string ToString() => String.Join("", ls.Select(elt => elt.ToString()));

        public override string ToString()
        {
            if (ls.Count() == 0) return "()";

            return String.Join("", ls.Select(elt => elt.ToString()));
        }
            

        public void Add(Cycle cycle) => ls.Add(cycle);

        public Cycles to_transpositions() => ls.SelectMany(cycle => cycle.to_transpositions()).ToCycles();

        public int apply_multiple(int a) => ls.Reverse<Cycle>().Aggregate(a, (elt, cycle) => cycle.apply(elt));

        public FunctionIntInt to_permutation(int n) =>
            new FunctionIntInt(Enumerable.Range(1, n).Select(i => (i, apply_multiple(i))));


    }

}
