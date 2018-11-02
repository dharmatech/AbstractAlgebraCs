using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractAlgebraCartesianProduct
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> seqs)
        {
            IEnumerable<IEnumerable<T>> empty_product = new[] { Enumerable.Empty<T>() };

            return
                seqs.Aggregate(
                    empty_product,
                    (acc, seq) =>
                        from accseq in acc
                        from item in seq
                        select accseq.Concat(new[] { item }));
        }
    }
}
