using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraStandardGroupZxZ
{
    public class Utils
    {
        public static Group<(int, int)> ZxZ(int a, int b) =>
            new Group<(int, int)>
            {
                Identity = (0, 0),
                Set =
                    Enumerable.Range(0, a).SelectMany(i =>
                        Enumerable.Range(0, b).Select(j =>
                                (i, j))).ToMathSet(),
                Op = (x, y) => ((x.Item1 + y.Item1) % a, (x.Item2 + y.Item2) % b),
                OpString = "+"
            };
    }
}
