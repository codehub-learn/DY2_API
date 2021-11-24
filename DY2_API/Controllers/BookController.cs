using DY2_API.DTOs;
using DY2_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DY2_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IBookService BookService { get; set; }
        public BookController(IBookService bookService)
        {
            BookService = bookService;
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<BookDto?>> GetBookById([FromRoute] int id)
        {
            var dto = await BookService.GetBook(id);

            if (dto is null) return BadRequest("The requested book does not exist");
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>?>> GetBooks()
        {
            var dto = await BookService.GetAllBooks();

            if (dto is null) return BadRequest();
            return Ok(dto);
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult<List<BookDto?>>> SearchBooks (string? name, string? publisher, string? authorfirst, string? authorlast)
        {
            var result = await BookService.Search(name, publisher, authorfirst, authorlast);
            if (result is null || !result.Any()) return BadRequest("No books that match the specified criteria were found");
            return result;
        }


        [HttpPost]
        public async Task<ActionResult<BookDto?>> PostBook(BookDto? dto) 
        {
            if (dto is null) return BadRequest("Please provide a valid book description!");
            var result = await BookService.AddBook(dto);
            if (result is null) return BadRequest("Author does not exist. Please provide a valid author id");
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            var result = await BookService.Delete(id);
            if (result) return Ok("Book deleted!");
            return BadRequest();
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult<BookDto?>> UpdateBook(int id, BookDto dto)
        {
            var result = await BookService.Update(id, dto);
            if (result is null) return BadRequest("Invalid Operation, Book Not Found");
            return Ok(result);
        }
    }
}
