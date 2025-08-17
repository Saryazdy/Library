using Library.Application._Extensions;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using Library.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IApplicationDbContext _ctx;

        public UpdateBookCommandHandler(IApplicationDbContext ctx) => _ctx = ctx;

        public async Task Handle(UpdateBookCommand request, CancellationToken ct)
        {
            var bookAgg = await _ctx.Books.FirstOrDefaultAsync(b => b.Book.Id == request.Id, ct)
                          ?? throw new NotFoundException("Book", request.Id);

            var isbn = Isbn.Create(request.Isbn);
            bookAgg.Update(request.Title, request.Year, request.Genre, isbn, request.Description);

            await _ctx.UpdateAsync(bookAgg, ct);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}
