using Library.Application._Extensions;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using Library.Application.Common.Specifications;
using Library.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    namespace Library.Application.Commands
    {
        public sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBookCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

            public async Task Handle(DeleteBookCommand request, CancellationToken ct)
            {
                // گرفتن ریپازیتوری
                var bookRepo = _unitOfWork.Repository<BookAggregate>();

                // می‌توان از Specification استفاده کرد
                var spec = new BookByIdSpecification(request.Id);

                var book = await bookRepo.FirstOrDefaultAsync(spec, ct)
                           ?? throw new NotFoundException("Book", request.Id);

                await bookRepo.RemoveAsync(book, ct);

                // Commit کل تغییرات
                await _unitOfWork.CommitAsync(ct);
            }
        }
    }
}
