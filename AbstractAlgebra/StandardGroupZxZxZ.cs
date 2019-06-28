using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraStandardGroupZxZxZ
{
    public class Utils
    {
        public static Group<(int, int, int)> ZxZxZ(int a, int b, int c) =>
            new Group<(int, int, int)>
            {
                Identity = (0, 0, 0),
                Set =
                    Enumerable.Range(0, a).SelectMany(i =>
                        Enumerable.Range(0, b).SelectMany(j =>
                            Enumerable.Range(0, c).Select(k =>
                                (i, j, k)))).ToMathSet(),
                Op = (x, y) => ((x.Item1 + y.Item1) % a, (x.Item2 + y.Item2) % b, (x.Item3 + y.Item3) % c),
                OpString = "+"
            };
    }
}
