using Library.Application.Common.Interfaces;
using Library.Domain.Aggregates;
using Library.Domain.Factories;
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
           
            var bookAgg = BookFactory.CreateNewBook(
                request.Title,
                request.Isbn,
                request.Year,
                request.Genre
            );
            bookAgg.Id = new Guid();
          
            await _unitOfWork.Repository<BookAggregate>().AddAsync(bookAgg, ct);
            await _unitOfWork.CommitAsync(ct);

            return bookAgg.Book.Id;
        }
    }
}

