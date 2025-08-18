using Library.Application._Extensions;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using Library.Application.Common.Specifications;
using Library.Domain.Aggregates;
using Library.Domain.ValueObjects;
using MediatR;

namespace Library.Application.Commands
{
    public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken ct)
        {
            // گرفتن Repository مخصوص BookAggregate از UnitOfWork
            var bookRepo = _unitOfWork.Repository<BookAggregate>();

            // گرفتن کتاب
            var bookAgg = await bookRepo.FirstOrDefaultAsync(
                new BookWithAuthorsSpec(request.Id), ct)
                ?? throw new NotFoundException("Book", request.Id);

            // به‌روز رسانی
            var isbn = Isbn.Create(request.Isbn);
            bookAgg.Update(request.Title, request.Year, request.Genre, isbn, request.Description);

            // ثبت تغییرات
            await bookRepo.UpdateAsync(bookAgg, ct);

            // ذخیره سازی یکجا با UnitOfWork
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
