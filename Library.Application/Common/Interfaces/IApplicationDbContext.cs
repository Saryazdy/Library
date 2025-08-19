using Library.Domain.Aggregates;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{

    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }   // برای دسترسی به تراکنش
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        // اینجا DbSetهایی که نیاز داری
        DbSet<Book> Books { get; }
        DbSet<Author> Authors { get; }
        DbSet<BookAuthor> BookAuthors { get; }
    }
}

