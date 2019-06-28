using System.Linq;

using AbstractAlgebraGroup;
using AbstractAlgebraMathSet;
using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraShowCosets;

namespace cosets_D5
{
    class Program
    {
        static void Main(string[] args)
        {
            // D5 - group of symmetries of the pentagon - page 133

            var R0 = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4), (5, 5));
            var R1 = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 5), (5, 1));
            var R2 = new FunctionIntInt((1, 3), (2, 4), (3, 5), (4, 1), (5, 2));
            var R3 = new FunctionIntInt((1, 4), (2, 5), (3, 1), (4, 2), (5, 3));
            var R4 = new FunctionIntInt((1, 5), (2, 1), (3, 2), (4, 3), (5, 4));
            var Ra = new FunctionIntInt((1, 1), (2, 5), (3, 4), (4, 3), (5, 2));
            var Rb = new FunctionIntInt((1, 2), (2, 1), (3, 5), (4, 4), (5, 3));
            var Rc = new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 5), (5, 4));
            var Rd = new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1), (5, 5));
            var Re = new FunctionIntInt((1, 5), (2, 4), (3, 3), (4, 2), (5, 1));

            var items = new[] { (R0, "R0"), (R1, "R1"), (R2, "R2"), (R3, "R3"), (R4, "R4"), (Ra, "Ra"), (Rb, "Rb"), (Rc, "Rc"), (Rd, "Rd"), (Re, "Re") };

            string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

            var D5 = new Group<FunctionIntInt>
            {
                Identity = R0,
                Set = new[] { R0, R1, R2, R3, R4, Ra, Rb, Rc, Rd, Re }.ToMathSet(),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup,
                OpString = "·"
            };

            D5.ShowCosets();
        }
    }
}
