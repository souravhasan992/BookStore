using BookStore.Data;
using BookStore.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(BookModel bookModel);
        Task<BookModel> GetBookByIdAsync(int BookId);        
        Task<List<BookModel>> GetBookModelsAsync();
    }
}