using AutoMapper;
using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetBookModelsAsync()
        {
            //var records = await _context.Books.Select(x=> new BookModel
            //{
            //    Id=x.Id,
            //    Title=x.Title,
            //    Description=x.Description,
            //}
            //).ToListAsync();
            //return records;
            var books=await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }
        
        public async Task<BookModel> GetBookByIdAsync(int BookId)
        {
            //var records = await _context.Books.Where(x=>x.Id ==BookId).Select(x => new BookModel
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}
            //).FirstOrDefaultAsync();
            //return records;

            var  book= await _context.Books.FindAsync(BookId);
            return _mapper.Map<BookModel>(book);
        }
        public async Task<Book> AddBookAsync(BookModel bookModel)
        {
            var book = new Book()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task UpdateBookByIdAsync(int bookId, BookModel bookModel)
        {
            //var book = await _context.Books.FindAsync(bookId);
            //if(book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;
            //    await _context.SaveChangesAsync();
            //}

            var book = new Book()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();            
        }
        
        public async Task UpdateBookByPatchAsync(int bookId, JsonPatchDocument bookModel)
        {         
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task DeleteBookByIdAsync(int bookId)
        {
            //var book= _context.Books.Where(x => x.Id == bookId).FirstOrDefault();
            //var book = _context.Books.Find(bookId);
            var book=new Book() { Id = bookId }; 
            if(book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
