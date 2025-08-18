using Library.Application.Books.Dtos;
using Library.Application.Commands;
using Library.Application.Common.Models;
using Library.Application.Queries.GetBookDetail;
using Library.Application.Queries.GetBooksWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> Get(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBookDetailQuery(id), ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<BookDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetBooksWithPaginationQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateBookCommand command, CancellationToken ct)
        {
            var id = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command, CancellationToken ct)
        {
            if (id != command.Id) return BadRequest();

            await _mediator.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteBookCommand(id), ct);
            return NoContent();
        }
    }

}