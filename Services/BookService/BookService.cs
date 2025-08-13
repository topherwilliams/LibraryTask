using LibraryTask.Config;
using LibraryTask.Models.DTOs;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Utils;
using LibraryTask.Utils.Constants;
using Microsoft.EntityFrameworkCore;

namespace LibraryTask.Services.BookService
{
    public class BookService : BaseService<BookService>, IBookService
    {
        public BookService(DatabaseContext db, ILogger<BookService> logger): base(db, logger) {}

        public async Task<ServiceResult<Book>> AddNewBook(Book newBook)
        {
            if (!BookUtils.PublishedYearIsValid(newBook.PublishedYear))
            {
                Logger.LogInformation($"AddNewBook: Unable to add book - {ErrorMessages.InvalidYear}");
                return ServiceResult<Book>.Fail(ErrorMessages.InvalidYear);
            }

            if (await BookUtils.IsbnExists(newBook.ISBN, Db)) {
                Logger.LogInformation($"AddNewBook: Unable to add book - {ErrorMessages.IsbnAlreadyExists}");
                return ServiceResult<Book>.Fail(ErrorMessages.IsbnAlreadyExists);
            }

            try
            {
                Db.Add(newBook);
                await Db.SaveChangesAsync();
            }
            catch (Exception ex) {
                Logger.LogError($"AddNewBook: Unable to add book - {ex}");
                return ServiceResult<Book>.Fail(ErrorMessages.DatabaseAddError);
            }

            var book = await GetBook(newBook.Id);

            return ServiceResult<Book>.Ok(book);
        }

        public async Task<List<Book>> GetAllBooks(int page, int take) {
            var books = await Db.Set<Book>()
                .OrderBy(book => book.Title)
                .ThenBy(book => book.PublishedYear)
                .Skip(take * (page - 1))
                .Take(take)
                .ToListAsync();
            return books;
        }

        public async Task<Book> GetBook(int id)
        {
            var book = await Db.Set<Book>()
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
                Db.Remove(book);
                await Db.SaveChangesAsync();
                return ServiceResult<object>.Ok(null);
            }
            catch (Exception ex)
            {
                Logger.LogError($"DeleteBook: Unable to delete book {id} - {ex}");
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
                Logger.LogInformation($"UpdateBook: Unable to add book - {ErrorMessages.InvalidYear}.");
                return ServiceResult<Book>.Fail(ErrorMessages.InvalidYear);
            }

            if (await BookUtils.IsbnExists(updatedBook.ISBN, Db))
            {
                Logger.LogInformation($"UpdateBook: Unable to add book - {ErrorMessages.IsbnAlreadyExists}.");
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
                await Db.SaveChangesAsync();
            }
            catch (Exception ex) {
                Logger.LogError($"UpdateBook: Unable to update book {id} - {ex}");
                return ServiceResult<Book>.Fail(ErrorMessages.DatabaseUpdateError);
            }

            return ServiceResult<Book>.Ok(book);
        }
    }
}
