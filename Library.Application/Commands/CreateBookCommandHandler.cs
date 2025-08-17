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
        private readonly IApplicationDbContext _ctx;

        public CreateBookCommandHandler(IApplicationDbContext ctx) => _ctx = ctx;

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken ct)
        {
            var isbn = Isbn.Create(request.Isbn);
            var agg = BookAggregate.Create(request.Title, request.Year, request.Genre, isbn, request.Description);

            await _ctx.AddAsync(agg, ct);
            await _ctx.SaveChangesAsync(ct);

            return agg.Book.Id; // فرضاً Book داخل Aggregate شناسه دارد
        }
    }
}
