using Library.Domain.Common;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Events
{
    public class BookUpdatedEvent : BaseEvent
    {
        public BookUpdatedEvent(Book book) => Book = book;
        public Book Book { get; }
    }
}
