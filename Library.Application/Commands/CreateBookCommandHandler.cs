using Library.Application.Common.Interfaces;
using Library.Domain.Aggregates;
using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken ct)
        {
            // ایجاد Aggregate
            var isbn = Isbn.Create(request.Isbn);
            var bookAgg = BookAggregate.Create(
                request.Title,
                request.Year,
                request.Genre,
                isbn,
                request.Description
            );

            // اضافه کردن به Repository
            await _unitOfWork.Repository<BookAggregate>().AddAsync(bookAgg, ct);

            // ذخیره تغییرات با UnitOfWork
            await _unitOfWork.CommitAsync(ct);

            return bookAgg.Book.Id;
        }
    }
}

