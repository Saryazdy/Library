using Library.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IReadLibraryDbContext
    {
        Task<IReadOnlyList<BookAggregate>> GetBooksAsync(CancellationToken ct = default);
        Task<IReadOnlyList<AuthorAggregate>> GetAuthorsAsync(CancellationToken ct = default);
    }

}
