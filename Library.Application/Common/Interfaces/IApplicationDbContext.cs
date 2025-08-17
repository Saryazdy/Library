using Library.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        // مثال: IQueryable/DbSet نمایی از Aggregate/Entity ها برای Query
        IQueryable<BookAggregate> Books { get; }

        Task AddAsync<T>(T entity, CancellationToken ct) where T : class;
        Task UpdateAsync<T>(T entity, CancellationToken ct) where T : class;
        Task RemoveAsync<T>(T entity, CancellationToken ct) where T : class;

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
