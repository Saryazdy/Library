using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application._Extensions;
using Library.Application.Books.Dtos;
using Library.Application.Common.Interfaces;
using Library.Application.Common.Models;
using Library.Application.Common.Specifications;
using Library.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.GetBooksWithPagination
{
    public sealed class GetBooksWithPaginationQueryHandler
         : IRequestHandler<GetBooksWithPaginationQuery, PaginatedList<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetBooksWithPaginationQuery request, CancellationToken ct)
        {
            var spec = new BooksWithPaginationSpecification(request.PageNumber, request.PageSize);

            // 1. گرفتن IQueryable با Specification
            var queryable = _unitOfWork.Repository<Book>().ApplySpecification(spec);

            // 2. Projection به DTO
            var projected = queryable.ProjectTo<BookDto>(_mapper.ConfigurationProvider).AsNoTracking();

            // 3. ایجاد PaginatedList
            return await PaginatedList<BookDto>.CreateAsync(projected, request.PageNumber, request.PageSize, ct);
        }
    }
}
