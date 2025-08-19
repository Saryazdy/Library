using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        Task BeginAsync(CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
        Task DispatchDomainEventsAsync(IPublisher mediator, CancellationToken ct = default);
    }
}
