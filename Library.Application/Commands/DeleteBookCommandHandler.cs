using Library.Application._Extensions;
using Library.Application.Common.Exceptions;
using Library.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IApplicationDbContext _ctx;

        public DeleteBookCommandHandler(IApplicationDbContext ctx) => _ctx = ctx;

        public async Task Handle(DeleteBookCommand request, CancellationToken ct)
        {
            var agg = await _ctx.Books.FirstOrDefaultAsync(x => x.Book.Id == request.Id, ct)
                      ?? throw new NotFoundException("Book", request.Id);

            await _ctx.RemoveAsync(agg, ct);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}
