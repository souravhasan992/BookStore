using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookById([FromBody] BookModel bookModel,[FromRoute] int id)
        {
            await bookRepository.UpdateBookByIdAsync(id,bookModel);
            
            return Ok("Book Updated");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookByPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        {
            await bookRepository.UpdateBookByPatchAsync(id, bookModel);

            return Ok("Book Updated By Pathch");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int id)
        {
            await bookRepository.DeleteBookByIdAsync(id);

            return Ok("Book Deleted Successfully");
        }
    }
}
