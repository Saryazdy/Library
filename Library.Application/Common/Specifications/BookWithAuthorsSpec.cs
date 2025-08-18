using Library.Application.Common.Interfaces;
using Library.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Specifications
{
    public sealed class BookWithAuthorsSpec : ISpecification<BookAggregate>
    {
        private readonly Guid _bookId;

        public BookWithAuthorsSpec(Guid bookId)
        {
            _bookId = bookId;
        }

        public IQueryable<BookAggregate> Apply(IQueryable<BookAggregate> query)
        {
            return query
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Where(b => b.Book.Id == _bookId);
        }
        public int? Take => null;
        public int? Skip => null;
        public Expression<Func<BookAggregate, object>>? OrderBy => null;
    }
}
