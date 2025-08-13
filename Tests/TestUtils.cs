using LibraryTask.Models.Entities.Book;

namespace LibraryTask.Tests
{
    public class TestUtils
    {
        public static List<Book> CreateRandomBookSet(int numberOfBooks)
        {
            var list = new List<Book>();
            for (int i = 0; i < numberOfBooks; i++)
            {
                list.Add(
                    new Book
                    {
                        Title = $"Book {i}",
                        AuthorId = 1,
                        ISBN = i.ToString(),
                        PublishedYear = 1900
                    }
                );
            }
            return list;
        }
    }
}
