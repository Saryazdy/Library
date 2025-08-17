using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Books.Dtos
{
    public sealed class BookDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = default!;
        public int Year { get; init; }
        public string Genre { get; init; } = default!;
        public string Isbn { get; init; } = default!;
        public List<string> Authors { get; init; } = new();
    }
}
