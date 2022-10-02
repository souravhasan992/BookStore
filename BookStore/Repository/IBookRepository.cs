using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(BookModel bookModel);
        Task DeleteBookByIdAsync(int bookId);
        Task<BookModel> GetBookByIdAsync(int BookId);        
        Task<List<BookModel>> GetBookModelsAsync();        
        Task UpdateBookByIdAsync(int bookId, BookModel bookModel);
        Task UpdateBookByPatchAsync(int id, JsonPatchDocument bookModel);
    }
}