using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;        
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetBookModelsAsync()
        {
            var records = await _context.Books.Select(x=> new BookModel
            {
                Id=x.Id,
                Title=x.Title,
                Description=x.Description,
            }
            ).ToListAsync();
            return records;
        }
        
        public async Task<BookModel> GetBookByIdAsync(int BookId)
        {
            var records = await _context.Books.Where(x=>x.Id ==BookId).Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }
            ).FirstOrDefaultAsync();
            return records;
        }
        public async Task<Book> AddBookAsync(BookModel bookModel)
        {
            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
