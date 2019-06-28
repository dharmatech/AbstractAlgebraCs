using System;
using System.Text;

using AbstractAlgebraShowCosets;

using static AbstractAlgebraStandardGroupZ.Utils;

namespace pinter_13_A_3_Z15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            Z(15).ShowCosets();
        }
    }
}
