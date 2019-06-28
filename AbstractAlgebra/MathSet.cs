using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractAlgebraMathSet
{
    public static class Extensions
    {
        public static MathSet<T> ToMathSet<T>(this IEnumerable<T> seq) => new MathSet<T>(seq);
    }

    public sealed class MathSet<T> : HashSet<T>, IEquatable<MathSet<T>>
    {
        // public override int GetHashCode() => this.Select(elt => elt.GetHashCode()).Sum().GetHashCode();

        private static readonly IEqualityComparer<HashSet<T>> Unique = CreateSetComparer();

        public override int GetHashCode() => Unique.GetHashCode(this);

        public bool Equals(MathSet<T> obj) => SetEquals(obj);

        public override bool Equals(object obj) => Equals(obj as MathSet<T>);

        public static bool operator ==(MathSet<T> a, MathSet<T> b) =>
            ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(MathSet<T> a, MathSet<T> b) => !(a == b);

        // ----------------------------------------------------------------------

        public MathSet() { }
        public MathSet(IEnumerable<T> col) : base(col) { }

        // ----------------------------------------------------------------------

        // public override string ToString() => string.Format("new [] {{ {0} }}.ToMathSet()", string.Join(", ", this));

        // public string AsString() => string.Format("{{ {0} }}", string.Join(" ", this));

        // public override string ToString() => string.Format("{{ {0} }}", string.Join(" ", this));

        public override string ToString() => string.Format("S{{ {0} }}", string.Join(" ", this));

        public string ToLiteral() => string.Format("new [] {{ {0} }}.ToMathSet()", string.Join(", ", this));


        public MathSet<T1> ConvertAll<T1>(Func<T, T1> func) => this.Select(elt => func(elt)).ToMathSet();
    }

}
