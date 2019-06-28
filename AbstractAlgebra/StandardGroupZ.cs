using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraStandardGroupZ
{
    public class Utils
    {
        public static Group<int> Z(int n) =>
            new Group<int>
            {
                Identity = 0,
                Set = Enumerable.Range(0, n).ToMathSet(),
                Op = (a, b) => (a + b) % n,
                OpString = "+"
            };
    }
}
