using LibraryTask.Config;
using LibraryTask.Models.DTOs;
using LibraryTask.Models.Entities.Book;

namespace LibraryTask.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResult<Book>> AddNewBook(Book newBook);
        Task<List<Book>> GetAllBooks(int page, int take);
        Task<Book> GetBook(int id);
        Task<ServiceResult<object>> DeleteBook(int id);
        Task<ServiceResult<Book>> UpdateBook(Book updatedBook, int id);
    }
}
