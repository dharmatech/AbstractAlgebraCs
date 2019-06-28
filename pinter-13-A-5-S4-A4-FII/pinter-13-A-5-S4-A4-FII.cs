using AbstractAlgebraShowCosets;

using static System.Console;

using static AbstractAlgebraSymmetricGroupFII.Utils;
using static AbstractAlgebraAlternatingGroupFII.Utils;

namespace pinter_13_A_5_S4_A4
{
    class Program
    {
        static void Main(string[] args)
        {
            var G = SymmetricGroupFII(4);
            var H = AlternatingGroupFII(4);

            WriteLine("S4: {0}", G);
            WriteLine("A4: {0}", H);

            WriteLine();

            G.ShowCoset(H);
        }
    }
}
