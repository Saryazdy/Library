using Library.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; protected set; }
        public int? Take { get; protected set; }
        public int? Skip { get; protected set; }
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public bool OrderByDescending { get; protected set; }

        public BaseSpecification() { }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (Criteria != null)
                query = query.Where(Criteria);

            if (OrderBy != null)
                query = OrderByDescending ? query.OrderByDescending(OrderBy) : query.OrderBy(OrderBy);

            if (Skip.HasValue)
                query = query.Skip(Skip.Value);

            if (Take.HasValue)
                query = query.Take(Take.Value);

            return query;
        }

        // متدهای کمکی برای صفحه‌بندی و مرتب‌سازی
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public void ApplyOrderBy(Expression<Func<T, object>> orderBy, bool descending = false)
        {
            OrderBy = orderBy;
            OrderByDescending = descending;
        }
    }
}
