using Library.Application.Common.Interfaces;
using Library.Domain.Common;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly LibraryDbContext _context;
        private IDbContextTransaction? _transaction;
        private bool _completed = false;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(LibraryDbContext context) => _context = context;

        // Generic repository
        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new EfRepository<T>(_context);

            return (IRepository<T>)_repositories[type]!;
        }

        // Transaction management
        public async Task BeginAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started.");

            _transaction = await _context.Database.BeginTransactionAsync(ct);
            _completed = false;
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            _transaction ??= _context.Database.CurrentTransaction ??
                await _context.Database.BeginTransactionAsync(ct);

            if (_transaction == null)
                throw new InvalidOperationException("No transaction started.");

            try
            {
                await _context.SaveChangesAsync(ct);
                await _transaction.CommitAsync(ct);
                _completed = true;
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        public async Task DispatchDomainEventsAsync(IPublisher mediator, CancellationToken ct = default)
        {
            var entitiesWithEvents = _context.ChangeTracker
                .Entries<IHasDomainEvent>() 
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var domainEvents = entitiesWithEvents
                .SelectMany(e => e.DomainEvents)
                .ToList();

            // پاک کردن event ها بعد از جمع‌آوری
            entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, ct);
            }
        }
    
        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_transaction == null) return;

            try
            {
                await _transaction.RollbackAsync(ct);
                _completed = true;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_completed && _transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }

            await _context.DisposeAsync();
        }
    }

}



