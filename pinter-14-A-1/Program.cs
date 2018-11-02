using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

using static System.Console;

namespace pinter_14_A_1_Z8
{
    class Program
    {
        static void Main(string[] args)
        {
            // IntZ8
            //
            // new IntZ8(4) 

            var Z8 = new Group<int>
            {
                Identity = 0,
                Set = Enumerable.Range(0, 8).ToMathSet(),
                Op = (a, b) => (a + b) % 8
            };

            Z8.ShowOperationTableColored();

            // Z8.Subgroup(new []{ 0, 4 })

            var K = new Group<int>
            {
                Identity = 0,
                Set = new MathSet<int>() { 0, 4 },
                Op = (a, b) => (a + b) % 8
            };

            // var result = Z8.Set.ConvertAll(elt => K.RightCoset(elt));


            // consider - should RightCoset return a group object

            // consider - should group object implement IEnumerable


            // K.AllRightCosets(Z8)

            // 

            WriteLine();

            WriteLine("K: {0}", K.Set); WriteLine();

            WriteLine("cosets of K: {0}", Z8.Set.ConvertAll(elt => K.RightCoset(elt)));

            // Z8.ConvertAll(elt => K.RightCoset(elt)) 
        }
    }
}
