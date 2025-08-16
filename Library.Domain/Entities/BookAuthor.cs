using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class BookAuthor
    {
        // EF Core معمولاً به این ctor نیاز داره
        private BookAuthor() { }

        public BookAuthor(Book book, Author author)
        {
            Book = book;
            BookId = book.Id;
            Author = author;
            AuthorId = author.Id;
        }

        public int BookId { get; private set; }
        public Book Book { get; private set; } = default!;

        public int AuthorId { get; private set; }
        public Author Author { get; private set; } = default!;

    }
}
