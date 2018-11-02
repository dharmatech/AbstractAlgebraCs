using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// consider - Succinct representations of permutations and functions
// https://www.sciencedirect.com/science/article/pii/S0304397512002253

namespace AbstractAlgebraFunctionIntInt
{
    public class FunctionSetComparer : IEqualityComparer<IEnumerable<FunctionIntInt>>
    {
        public bool Equals(IEnumerable<FunctionIntInt> a, IEnumerable<FunctionIntInt> b) => new HashSet<FunctionIntInt>(a).SetEquals(b);

        public int GetHashCode(IEnumerable<FunctionIntInt> seq) => string.Join(" ", seq.Select(elt => elt.GetHashCode())).GetHashCode();
    }

    // https://stackoverflow.com/questions/25461585/operator-overloading-equals
    // https://stackoverflow.com/questions/10790370/whats-wrong-with-defining-operator-but-not-defining-equals-or-gethashcode
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type

    public class FunctionIntInt : IEquatable<FunctionIntInt>
    {
        public readonly List<(int, int)> ls;

        // ----------------------------------------------------------------------

        // public FunctionIntInt() { }

        public FunctionIntInt(IEnumerable<(int, int)> items) { ls = items.ToList(); }

        public FunctionIntInt(params (int, int)[] items) { ls = items.ToList(); }

        public FunctionIntInt(string str)
        {
            ls = Enumerable.Range(1, str.Count())
                .Zip(str.Select(elt => elt - '0'), (k, v) => (k, v))
                .ToList();
        }

        // ----------------------------------------------------------------------

        public override string ToString() => string.Format("new FunctionIntInt({0})", string.Join(", ", ls));

        // ----------------------------------------------------------------------

        public bool Equals(FunctionIntInt obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(obj, this)) return true;

            if (obj.GetType() != GetType()) return false;
            
             return new HashSet<(int, int)>(ls).SetEquals(obj.ls);
        }

        public override bool Equals(object obj) => Equals(obj as FunctionIntInt);

        public static bool operator ==(FunctionIntInt a, FunctionIntInt b) =>
            ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(FunctionIntInt a, FunctionIntInt b) => !(a == b);

        public override int GetHashCode() => string.Join(" ", ls.SelectMany(elt => new[] { elt.Item1, elt.Item2 }).OrderBy(elt => elt)).GetHashCode();

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        var hash = 17;

        //        // ls.ForEach(elt => hash = hash * 23 + elt.GetHashCode());

        //        foreach (var elt in ls.OrderBy(elt => elt.Item1))
        //        {
        //            hash = hash * 23 + elt.GetHashCode();
        //        }

        //        return hash;
        //    }
        //}

        // ----------------------------------------------------------------------

        public static FunctionIntInt FromString(string s) =>
            new FunctionIntInt(
                Enumerable.Range(1, s.Count())
                    .Zip(
                        s.Select(elt => elt - '0'),
                        (k, v) => (k, v)));

        public int Apply(int x) => ls.First(elt => elt.Item1 == x).Item2;

        public FunctionIntInt Compose(FunctionIntInt g) =>
            new FunctionIntInt(g.ls.Select(elt => (elt.Item1, Apply(elt.Item2))));

        public FunctionIntInt Inverse() =>
            new FunctionIntInt(ls.Select(elt => (elt.Item2, elt.Item1)).OrderBy(elt => elt.Item1));

    }
}
