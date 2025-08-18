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
    public sealed class BookByIdSpecification : ISpecification<BookAggregate>
    {
        private readonly Guid _id;

        public BookByIdSpecification(Guid id) => _id = id;

        public IQueryable<BookAggregate> Apply(IQueryable<BookAggregate> query)
        {
            return query.Where(b => b.Book.Id == _id);
        }

        public int? Take => null;
        public int? Skip => null;
        public Expression<Func<BookAggregate, object>>? OrderBy => null;

    }
}
