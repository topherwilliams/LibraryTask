using LibraryTask.Config;
using LibraryTask.Services.BookService;
using LibraryTask.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        public async void Test_CheckIBSN_UniqueValue_ReturnsFalse()
        {
            var bookList = TestUtils.CreateRandomBookSet(1);
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestingDb")
                .Options;

            using var context = new DatabaseContext(options);
            context.Books.AddRange(bookList);
            context.SaveChanges();

            var res = await BookUtils.IsbnExists("1", context);

            Assert.NotNull(res);
            Assert.False(res);
        }

        [Fact]
        public async void Test_CheckIBSN_DuplicateValue_ReturnsTrue()
        {
            var bookList = TestUtils.CreateRandomBookSet(1);
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestingDb")
                .Options;

            using var context = new DatabaseContext(options);
            context.Books.AddRange(bookList);
            context.SaveChanges();

            var res = await BookUtils.IsbnExists("0", context);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_GetBooks_Pagination_TakeValue()
        {
            var bookList = TestUtils.CreateRandomBookSet(20);

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
    }
}


