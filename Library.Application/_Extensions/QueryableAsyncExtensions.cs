using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application._Extensions
{
    internal static class QueryableAsyncExtensions
    {
        public static Task<T?> FirstOrDefaultAsync<T>(this IQueryable<T> source, Func<T, bool> predicate, CancellationToken ct = default)
            => Task.FromResult(source.FirstOrDefault(predicate));
    }
}
