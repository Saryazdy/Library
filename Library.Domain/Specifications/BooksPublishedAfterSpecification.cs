using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Specifications
{
    public class BooksPublishedAfterSpecification
    {
        private readonly DateTime _date;

        public BooksPublishedAfterSpecification(DateTime date)
        {
            _date = date;
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return book => book.PublishedOn > _date;
        }
    }
}
