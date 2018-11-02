
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraGroup;
using AbstractAlgebraMathSet;
using AbstractAlgebraQuotientGroup;
using static System.Console;

namespace pinter_15_A_5_Z4xZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            var Z4xZ2 = new Group<(int, int)>
            {
                Identity = (0, 0),
                Set = new[] { (0, 0), (0, 1), (1, 0), (1, 1), (2, 0), (2, 1), (3, 0), (3, 1) }.ToMathSet(),
                Op = (a, b) => ((a.Item1 + b.Item1) % 4, (a.Item2 + b.Item2) % 2),
                OpString = "+"
            };

            Write("Z4xZ2 "); Z4xZ2.ShowOperationTableColored(); WriteLine();

            // <(0,1)> -> { (0,1), (0,0) }

            var H = Z4xZ2.Subgroup(new[] { (0,0), (0,1) });

            WriteLine("Elements of H: {0}\n", H.Set);

            WriteLine("Elements of quotient group Z4xZ2/H:\n");

            foreach (var elt in Z4xZ2.CosetGrouping(H, "H"))
                WriteLine($"{ elt.ToMathSet() }   { elt.Key }");

            WriteLine();

            Write("Z4xZ2/{ (0, 1), (0, 0) } ");

            Z4xZ2.QuotientGroup(H).ShowOperationTableColored();
        }
    }
}
