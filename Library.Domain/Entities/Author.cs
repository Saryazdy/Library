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

        private Author() { } // برای EF

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
