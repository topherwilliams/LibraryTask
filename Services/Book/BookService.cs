
namespace LibraryTask.Services.Book
{
    public class BookService : IBookService
    {
        public static bool PublishedYearIsValid(int year)
        {
            var today = DateTime.Now;
            return year <= today.Year;
        }
    }
}
