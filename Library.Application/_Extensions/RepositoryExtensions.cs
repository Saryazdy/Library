using Library.Application.Common.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application._Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<T> ApplySpecification<T>(this IRepository<T> repo, ISpecification<T> spec)
            where T : class
        {
            // IRepository<T> شما باید IQueryable<T> بدهد
            // فرض می‌کنیم repo.Query() IQueryable<T> می‌دهد
            var queryable = repo.AsQueryable();

            if (spec.Skip.HasValue)
                queryable = queryable.Skip(spec.Skip.Value);

            if (spec.Take.HasValue)
                queryable = queryable.Take(spec.Take.Value);

            if (spec.OrderBy != null)
                queryable = queryable.OrderBy(spec.OrderBy);

            queryable = spec.Apply(queryable);

            return queryable;
        }
    }

}
