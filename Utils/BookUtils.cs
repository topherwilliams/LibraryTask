using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using Microsoft.EntityFrameworkCore;

namespace LibraryTask.Utils
{
    public class BookUtils
    {
        public static bool PublishedYearIsValid(int year)
        {
            var today = DateTime.Now;
            return year <= today.Year;
        }

        public static async Task<bool> IsbnExists(string isbn, DatabaseContext db)
        {
            return await db.Set<Book>()
                .AnyAsync(i => i.ISBN == isbn);
        }
    }
}