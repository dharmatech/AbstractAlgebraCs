using System;
using System.Linq;
using System.Text;

using AbstractAlgebraMathSet;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_16_A_3_S3_Z2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            Write("S3 "); S3.ShowOperationTableColored(); WriteLine();

            var εβσ = S3.Subgroup(new[] { ε, β, σ });

            foreach (var elt in S3.CosetGrouping(εβσ, "εβσ"))
                WriteLine("{0}   {1}", elt.ToMathSet(), elt.Key.ConvertAll(S3.Lookup));

            WriteLine();

            Write("S3/εβσ ");

            S3
                .QuotientGroup(εβσ, coset => new[] { ε, α, β, γ, σ, κ }.ToList().IndexOf(coset.Element).ToString(), "εβσ")
                .ShowOperationTableColored();
            
        }
    }
}
