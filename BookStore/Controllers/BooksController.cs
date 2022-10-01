using BookStore.Data.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BooksController( IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books=await bookRepository.GetBookModelsAsync();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var book = await bookRepository.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromForm] BookModel bookModel)
        {
            var book = await bookRepository.AddBookAsync(bookModel);
            if (book == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetBookById), new {id=book.Id,Controller="books"},book);
        }
    }
}
