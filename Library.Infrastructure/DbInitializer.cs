using Bogus;
using Library.Domain.Aggregates;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using Library.Infrastructure.Data;
using Library.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public static class DbInitializer
    {
       
            public static void Seed(LibraryDbContext context, int authorCount = 20, int bookCount = 50)
            {
                var rnd = new Random();

            // فرض: Author دارای سازنده عمومی Author(string firstName, string lastName)
            var authorFaker = FakerHelper.CreateFaker<Author>(f => new object[]
                          {
                        f.Name.FirstName(),
                        f.Name.LastName()
                          });


            var authors = authorFaker.Generate(authorCount);

            // ساخت Aggregateها
            var authorAggregates = authors
                .Select(a => AuthorAggregate.FromEntity(a))
                .ToList();

            // 2️⃣ Books
            var bookAggregatesFaker = new Faker<BookAggregate>()
           .CustomInstantiator(f => BookAggregate.Create(
               f.Lorem.Sentence(3),
               f.Random.Int(1900, 2025),
               f.PickRandom<Genre>(),
               IsbnFaker.RandomIsbn(),
               f.Lorem.Paragraph()
           ));

            var booksAggregates = bookAggregatesFaker.Generate(bookCount);
              

                // 3️⃣ ایجاد روابط تصادفی Book ↔ Authors
                foreach (var bookAgg in booksAggregates)
                {
                    int authorsPerBook = rnd.Next(1, Math.Min(4, authors.Count));
                    var selectedAuthors = authors.OrderBy(a => rnd.Next()).Take(authorsPerBook);

                    foreach (var author in selectedAuthors)
                    {
                        bookAgg.AddAuthor(author); // اینجا BookAuthor ایجاد می‌شود
                    }
                }
       
                // 4️⃣ افزودن به DbContext
                context.Authors.AddRange(authors);
            
                context.Books.AddRange(booksAggregates.Select(b => b.Book));
                context.SaveChanges();
            }
        }
    }
