using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractAlgebraZipMany
{
    public static class Extensions
    {
        public static IEnumerable<TResult> ZipMany<TSource, TResult>(
                    this IEnumerable<IEnumerable<TSource>> source,
                    Func<IEnumerable<TSource>, TResult> selector)
        {
            // ToList is necessary to avoid deferred execution
            var enumerators = source.Select(seq => seq.GetEnumerator()).ToList();
            try
            {
                while (true)
                {
                    foreach (var e in enumerators)
                    {
                        bool b = e.MoveNext();
                        if (!b) yield break;
                    }
                    // Again, ToList (or ToArray) is necessary to avoid deferred execution
                    yield return selector(enumerators.Select(e => e.Current).ToList());
                }
            }
            finally
            {
                foreach (var e in enumerators)
                    e.Dispose();
            }
        }
    }
}
