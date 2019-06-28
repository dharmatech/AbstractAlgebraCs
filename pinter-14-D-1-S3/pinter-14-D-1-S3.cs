using System;
using System.Text;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_14_D_1_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            S3.ShowOperationTableColored();

            WriteLine("subgroups       : {0}", S3.Subgroups());
            WriteLine("normal subgroups: {0}", S3.NormalSubgroups());
        }
    }
}
