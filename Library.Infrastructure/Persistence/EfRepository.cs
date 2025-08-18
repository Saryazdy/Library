using Library.Application.Common.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> AsQueryable() => _context.Set<T>().AsQueryable();

        // Queries
        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken ct = default)
            => await spec.Apply(_context.Set<T>()).FirstOrDefaultAsync(ct);

        public async Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken ct = default)
            => await spec.Apply(_context.Set<T>()).ToListAsync(ct);

        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken ct = default)
            => await spec.Apply(_context.Set<T>()).CountAsync(ct);

        public async Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken ct = default)
            => await spec.Apply(_context.Set<T>()).AnyAsync(ct);

        // Commands
        public async Task AddAsync(T entity, CancellationToken ct = default)
            => await _context.Set<T>().AddAsync(entity, ct);

        public Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity, CancellationToken ct = default)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}


