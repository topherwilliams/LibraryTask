using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Services.BookServices;
using LibraryTask.Utils;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Sockets;
using Xunit;

namespace LibraryTask.Tests
{
    public class BookUnitTests
    {
        [Fact]
        public async void Test_CheckPublishedDate_HistoricDate_ReturnsTrue()
        {
            var publishedYear = 2020;
            var res = BookUtils.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ExtremeHistoricDate_ReturnsTrue()
        {
            var publishedYear = DateTime.MinValue.Year;
            var res = BookUtils.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ThisYear_ReturnsTrue()
        {
            var publishedYear = DateTime.Now.Year;
            var res = BookUtils.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_FutureDate_ReturnsTrue()
        {
            var publishedYear = DateTime.Now.Year + 1;
            var res = BookUtils.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.False(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ExtremeFutureDate_ReturnsTrue()
        {
            var publishedYear = DateTime.MaxValue.Year;
            var res = BookUtils.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.False(res);
        }

        [Fact]
        public async void Test_GetBooks_Pagination_TakeValue()
        {
            var bookList = CreateRandomBookSet(20);

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestingDb")
                .Options;

            using var context = new DatabaseContext(options);
            context.Books.AddRange(bookList);
            context.SaveChanges();

            var logger = new Mock<ILogger<BookService>>().Object;
            var service = new BookService(context, logger);

            var result = await service.GetAllBooks(1, 10);

            Assert.NotNull(result);
            Assert.Equal(10, result.Count);

        }

        public List<Book> CreateRandomBookSet(int numberOfBooks)
        {
            var list = new List<Book>();
            for (int i = 0; i <= numberOfBooks; i++)
            {
                list.Add(
                    new Book
                    {
                        Title = $"Book {i}",
                        AuthorId = 1,
                        ISBN = i.ToString(),
                        PublishedYear = 1999
                    }
                );
            }
            return list;
        }

    }


}


