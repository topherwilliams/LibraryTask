using LibraryTask.Services.BookServices;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Xunit;

namespace LibraryTask.Tests
{
    public class BookUnitTests
    {
        [Fact]
        public async void Test_CheckPublishedDate_HistoricDate_ReturnsTrue()
        {
            var publishedYear = 2020;
            var res = BookService.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ExtremeHistoricDate_ReturnsTrue()
        {
            var publishedYear = DateTime.MinValue.Year;
            var res = BookService.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ThisYear_ReturnsTrue()
        {
            var publishedYear = DateTime.Now.Year;
            var res = BookService.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.True(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_FutureDate_ReturnsTrue()
        {
            var publishedYear = DateTime.Now.Year + 1;
            var res = BookService.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.False(res);
        }

        [Fact]
        public async void Test_CheckPublishedDate_ExtremeFutureDate_ReturnsTrue()
        {
            var publishedYear = DateTime.MaxValue.Year;
            var res = BookService.PublishedYearIsValid(publishedYear);

            Assert.NotNull(res);
            Assert.False(res);
        }
    }
}
