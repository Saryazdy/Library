using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks()
        {
            return await _mediator.Send(new GetBooksQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBook(CreateBookCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
