using Library.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Author : AuditableEntity
    {
      
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        private readonly List<BookAuthor> _bookAuthors = new();
        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();

<<<<<<< HEAD
        private Author() { } 
=======
        private Author() { } // برای EF
>>>>>>> d3c0b503dfc5f415fa69e49ae0bc5629030139fc

        public Author(string firstName, string lastName)
        {
            UpdateName(firstName, lastName);
        }

        public void UpdateName(string firstName, string lastName)
        {
            FirstName = firstName?.Trim() ?? string.Empty;
            LastName = lastName?.Trim() ?? string.Empty;
        }

        public string FullName => string.Join(" ",
            new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}
