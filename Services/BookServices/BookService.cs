using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Utils;
using LibraryTask.Utils.Constants;
using Microsoft.EntityFrameworkCore;

namespace LibraryTask.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _db;
        private readonly ILogger<BookService> _logger;

        public BookService(DatabaseContext db, ILogger<BookService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ServiceResult<Book>> AddNewBook(Book newBook)
        {
            if (!BookUtils.PublishedYearIsValid(newBook.PublishedYear))
            {
                _logger.LogInformation("AddNewBook: Unable to add book - invalid year.");
                return ServiceResult<Book>.Fail(ErrorMessages.InvalidYear);
            }

            if (await BookUtils.IsbnExists(newBook.ISBN, _db)) {
                _logger.LogInformation("AddNewBook: Unable to add book - ISBN already in use.");
                return ServiceResult<Book>.Fail(ErrorMessages.IsbnAlreadyExists);
            }

            try
            {
                _db.Add(newBook);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                _logger.LogInformation($"AddNewBook: Unable to add book - {ex}");
                return ServiceResult<Book>.Fail(ErrorMessages.DatabaseAddError);
            }

            var book = await GetBook(newBook.Id);

            return ServiceResult<Book>.Ok(book);
        }

        public async Task<List<Book>> GetAllBooks(int page, int take) {
            var books = await _db.Set<Book>()
                .OrderBy(book => book.Title)
                .ThenBy(book => book.PublishedYear)
                .Skip(take * (page - 1))
                .Take(take)
                .ToListAsync();
            return books;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await _db.Set<Book>()
                .FirstOrDefaultAsync(i => i.Id == id);
            return book;
        }

        public async Task<ServiceResult<object>> DeleteBook(int id)
        {
            var book = await GetBook(id);
            if (book == null)
            {
                return ServiceResult<object>.Fail(ErrorMessages.BookNotFound);
            }

            try
            {
                _db.Remove(book);
                await _db.SaveChangesAsync();
                return ServiceResult<object>.Ok(null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteBook: Unable to delete book {id}");
                return ServiceResult<object>.Fail(ErrorMessages.DatabaseDeleteError);
            }
        }

        public async Task<ServiceResult<Book>> UpdateBook(Book updatedBook, int id)
        {
            if (updatedBook.Id != id)
            {
                return ServiceResult<Book>.Fail(ErrorMessages.ConflictInId);
            }

            if (!BookUtils.PublishedYearIsValid(updatedBook.PublishedYear))
            {
                return ServiceResult<Book>.Fail(ErrorMessages.InvalidYear);
            }

            if (await BookUtils.IsbnExists(updatedBook.ISBN, _db))
            {
                _logger.LogInformation("UpdateBook: Unable to add book - ISBN already in use.");
                return ServiceResult<Book>.Fail(ErrorMessages.IsbnAlreadyExists);
            }

            var book = await GetBook(id);
            if (book == null)
            {
                return ServiceResult<Book>.Fail(ErrorMessages.BookNotFound);
            }

            try
            {
                book.Title = updatedBook.Title;
                book.ISBN = updatedBook.ISBN;
                book.PublishedYear = updatedBook.PublishedYear;
                book.AuthorId = updatedBook.AuthorId;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) {
                _logger.LogError($"Unable to update book {id}");
                return ServiceResult<Book>.Fail(ErrorMessages.DatabaseUpdateError);
            }

            return ServiceResult<Book>.Ok(book);
        }
    }
}



public class ServiceResult<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public T Result { get; set; }

    public static ServiceResult<T> Ok(T? result) =>
        new ServiceResult<T> { Success = true, Result = result };

    public static ServiceResult<T> Fail(string error) =>
     new ServiceResult<T> { Success = false, ErrorMessage = error };

}