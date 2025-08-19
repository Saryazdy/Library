using Library.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Common.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _mediator; // برای dispatch event های دامنه

        public TransactionBehaviour(IUnitOfWork unitOfWork, IPublisher mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // بررسی اینکه آیا قبلاً تراکنش باز هست یا نه
            var hasActiveTransaction = false;

            try
            {
                if (_unitOfWork is null)
                    throw new System.InvalidOperationException("UnitOfWork is not initialized.");

                await _unitOfWork.BeginAsync(cancellationToken);
                hasActiveTransaction = true;

                var response = await next(); // اجرای handler اصلی

                await _unitOfWork.CommitAsync(cancellationToken); // commit تغییرات

                // بعد از commit، event های دامنه رو dispatch کن
                await _unitOfWork.DispatchDomainEventsAsync(_mediator, cancellationToken);

                return response;
            }
            catch
            {
                if (hasActiveTransaction)
                    await _unitOfWork.RollbackAsync(cancellationToken);

                throw;
            }
        }
    }
}
