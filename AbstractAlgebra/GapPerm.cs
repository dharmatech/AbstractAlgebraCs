using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Immutable;

using AbstractAlgebraCycles;

namespace AbstractAlgebraGapPerm
{
    public class GapPerm : IEquatable<GapPerm>
    {
        public readonly ImmutableArray<int> arr;

        // ------------------------------------------------------------

        public GapPerm(params int[] items) => arr = items.ToImmutableArray();

        public GapPerm(IEnumerable<int> seq) => arr = seq.ToImmutableArray();

        public GapPerm Simplify()
        {
            if (arr.Count() == 1 && arr[0] == 0) return this;

            if (arr.Last() == arr.Count() - 1)   
                return new GapPerm(arr.Take(arr.Count() - 1)).Simplify();
            
            return this;
        }

        public GapPerm(string s)
        {
            var cycles = Cycles.from_string(s);

            var n = cycles.Select(cycle => cycle.Max()).Max();

            var ls = new List<int>();

            // Enumerable.Range(0, n+1).Select(i => cycles.apply_multiple(i))

            for (var i = 0; i <= n; i++)
            {
                ls.Add(cycles.apply_multiple(i));
            }

            arr = ls.ToImmutableArray();
        }

        // ------------------------------------------------------------

        public bool Equals(GapPerm obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            if (obj.GetType() != GetType()) return false;

            return Enumerable.SequenceEqual(arr, obj.arr);
        }

        public override bool Equals(object obj) => Equals(obj as GapPerm);

        public static bool operator ==(GapPerm a, GapPerm b) =>
            ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(GapPerm a, GapPerm b) => !(a == b);

        public override int GetHashCode()
        {
            unchecked
            {
                return
                    arr.Aggregate(17, (acc, elt) => acc * 23 + elt.GetHashCode());
            }
        }

        public override string ToString()
        {
            return ToDisjointCycles().ToString();
        }

        // ------------------------------------------------------------
        public int Apply(int i) => i >= arr.Count() ? i : arr[i];

        public GapPerm Compose(GapPerm perm) => 
            new GapPerm(
                Enumerable.Range(0, Math.Max(arr.Count(), perm.arr.Count()))
                .Select(i => Apply(perm.Apply(i))))
            .Simplify()
            ;

        public GapPerm Inverse()
        {
            if (arr.IsEmpty) return this;
            
            var tmp = Enumerable.Range(0, arr.Max() + 1).ToArray();

            arr.Select((elt, i) => tmp[elt] = i).ToList();

            // return new GapPerm(tmp);

            return new GapPerm(tmp).Simplify();

            // consider
            // return new GapPerm(tmp).Simplify();
        }
        // ------------------------------------------------------------

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


        public Cycle GetCycle(int i)
        {
            var result = new List<int>();

            while (true)
            {
                if (result.Contains(i)) return new Cycle(result);

                result.Add(i);

                i = Apply(i);
            }
        }

        public Cycles ToDisjointCycles()
        {
            return Enumerable.Range(1, arr.Count() - 1)
                .Aggregate(
                    new Cycles(),
                    (cycles, i) =>
                        cycles.SelectMany(elt => elt).Contains(i) ? cycles :
                        Apply(i) == i ? cycles :
                        new Cycles(cycles.Concat(new[] { GetCycle(i) })));
        }


    }
}
