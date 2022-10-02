using AutoMapper;
using BookStore.Data;
using BookStore.Data.Models;

namespace BookStore.Helpers
{
    public class ApplicationMapper : Profile
    {
        protected ApplicationMapper()
        {
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
        }       
    }
}
