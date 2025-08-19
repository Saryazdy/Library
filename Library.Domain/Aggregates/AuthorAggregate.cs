using Library.Domain.Common;
using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Aggregates
{
    public class AuthorAggregate : BaseEntity
    {
        private AuthorAggregate() { } // EF

        private AuthorAggregate(Author author)
        {
            _author = author ?? throw new ArgumentNullException(nameof(author));
      
            FirstName = author.FirstName;
            LastName = author.LastName;
        }

        private readonly Author _author;

        public static AuthorAggregate FromEntity(Author author) => new AuthorAggregate(author);

        // EF Core باید بتواند propertyها را ببیند
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;

      
        public IReadOnlyCollection<BookAuthor> BookAuthors => _author?.BookAuthors ?? new List<BookAuthor>().AsReadOnly();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        // متدهای تغییر داده
        public void UpdateName(string firstName, string lastName)
        {
            _author?.UpdateName(firstName, lastName);
            FirstName = firstName;
            LastName = lastName;
        }

        // دسترسی مستقیم به entity اگر نیاز داشتی
        public Author Author => _author!;
    }
}
