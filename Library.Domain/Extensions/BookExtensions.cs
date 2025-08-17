using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Extensions
{
    public static class BookExtensions
    {
        public static bool IsClassic(this Book book)
        {
            return book.PublishedOn.Year < 1970;
        }
    }
}
