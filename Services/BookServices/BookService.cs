using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;

namespace LibraryTask.Services.BookServices
{
    public class BookService : IBookService
    {
        public static bool PublishedYearIsValid(int year)
        {
            var today = DateTime.Now;
            return year <= today.Year;
        }

        public static async Task<Book> AddNewBook(Book newBook, DatabaseContext db, ILogger logger)
        {
            if (!PublishedYearIsValid(newBook.PublishedYear))
            {
                logger.LogInformation("AddNewBook: Unable to add book - invalid year.");
                throw new ArgumentException("Invalid year.");
            }

            try
            {
                db.Add(newBook);
                await db.SaveChangesAsync();
            }
            catch (Exception ex) {
                logger.LogInformation($"AddNewBook: Unable to add book - {ex}");
                throw new Exception("Unable to add book.");
            }

            var book = await GetBook(newBook.Id, db, logger);

            return book;
        }

        public static async Task<List<Book>> GetAllBooks(DatabaseContext db, ILogger logger) {
            var books = db.Set<Book>().ToList();
            return books;
        }

        public static async Task<Book> GetBook(int id, DatabaseContext db, ILogger logger)
        {
            var book = db.Set<Book>()
                .FirstOrDefault(i => i.Id == id);
            return book;
        }

        public static async Task<bool> DeleteBook(int id, DatabaseContext db, ILogger logger)
        {
            var book = await GetBook(id, db, logger);
            if (book == null)
            {
                return false;
            }

            try
            {
                db.Remove(book);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"DeleteBook: Unable to delete book {id}");
                throw;
            }
        }

        public static async Task<Book> UpdateBook(Book updatedBook, int id,  DatabaseContext db, ILogger logger)
        {
            var book = await GetBook(id, db, logger);
            if (book == null)
            {
                return false;
            }


        }


    }
}
