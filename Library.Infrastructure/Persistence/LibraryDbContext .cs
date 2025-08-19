using Library.Application.Common.Interfaces;
using Library.Domain.Aggregates;
using Library.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application._Extensions;

namespace Library.Infrastructure.Data
{
    public class LibraryDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {  }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).HasMaxLength(200);

                b.OwnsOne(x => x.Isbn, isbn =>
                {
                    isbn.Property(i => i.Value).HasColumnName("Isbn");
                });

                b.Metadata.FindNavigation(nameof(Book.BookAuthors))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            // Author
            modelBuilder.Entity<Author>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.FirstName).HasMaxLength(300);
                a.Property(x => x.LastName).HasMaxLength(300);

                a.Metadata.FindNavigation(nameof(Author.BookAuthors))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            // BookAuthor (many-to-many)
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}


