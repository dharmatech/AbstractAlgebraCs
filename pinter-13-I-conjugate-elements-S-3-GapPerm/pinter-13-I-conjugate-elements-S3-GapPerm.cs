using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_13_I_conjugate_elements_S3_GapPerm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            foreach (var elt in S3.Set) WriteLine("{0} :   {1}", S3.Lookup(elt), elt); WriteLine();

            S3.ShowOperationTable(); WriteLine();

            foreach (var item in S3.Set)
                WriteLine("{0} inverse: {1}",
                    S3.Lookup(item),
                    S3.Lookup(S3.Inverse(item)));

            WriteLine();

            WriteLine("conjugacy classes (partition of group):");

            WriteLine(S3.Set.Select(a => S3.ConjugacyClass(a).ConvertAll(S3.Lookup)).ToMathSet()); WriteLine();

            foreach (var a in S3.Set)
                WriteLine("conjugacy class of {0,4}: {1}",
                    S3.Lookup(a),
                    S3.ConjugacyClass(a).ConvertAll(S3.Lookup));

            WriteLine();

            foreach (var a in S3.Set)
                WriteLine("centralizer of {0,4}: {1}",
                    S3.Lookup(a),
                    S3.Centralizer(a).ConvertAll(S3.Lookup));

            WriteLine();

            S3.ShowConjugates();

            S3.ShowCentralizers();

        }
    }
}
