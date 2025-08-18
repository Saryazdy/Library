using Library.Application.Common.Interfaces;
using Library.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Specifications
{
    public sealed class BooksWithPaginationSpecification : ISpecification<BookAggregate>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public int? Take => PageSize;
        public int? Skip => (PageNumber - 1) * PageSize;

        // اگر بخواهید ترتیب مشخصی داشته باشید
        public Expression<Func<BookAggregate, object>>? OrderBy => b => b.Book.Title;

        public BooksWithPaginationSpecification(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }

        public IQueryable<BookAggregate> Apply(IQueryable<BookAggregate> query)
        {
            if (OrderBy != null)
            {
                query = query.OrderBy(OrderBy);
            }

            if (Skip.HasValue)
                query = query.Skip(Skip.Value);

            if (Take.HasValue)
                query = query.Take(Take.Value);

            return query;
        }
    }
}
