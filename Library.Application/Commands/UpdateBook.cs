using Library.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands
{
    public sealed record UpdateBookCommand(
        Guid Id,
        string Title,
        int Year,
        Genre Genre,
        string Isbn,
        string? Description
    ) : IRequest;
}
