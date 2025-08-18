using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        // Query
        Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default);
        Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default);

        // Command
        Task AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task RemoveAsync(T entity, CancellationToken ct = default);
    }
}
