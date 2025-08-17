using Library.Domain.Aggregates;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Factories
{
    public static class BookFactory
    {
        public static BookAggregate CreateNewBook(string title, string isbn, int year, Genre genre)
        {
            var isbnValueObject = Isbn.Create(isbn);

      
            var bookAggregate = BookAggregate.Create(title, year, genre, isbnValueObject);

            return bookAggregate;
        }
    }
}
