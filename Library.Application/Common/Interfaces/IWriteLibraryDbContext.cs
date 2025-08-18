using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IWriteLibraryDbContext
    {
        Task AddAsync<T>(T entity, CancellationToken ct = default) where T : class;
        Task UpdateAsync<T>(T entity, CancellationToken ct = default) where T : class;
        Task RemoveAsync<T>(T entity, CancellationToken ct = default) where T : class;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
