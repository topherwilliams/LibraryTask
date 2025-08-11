using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using Microsoft.AspNetCore.Mvc;


namespace LibraryTask.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : Controller
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _dbContext;

        public BaseController(ILogger<T> logger, DatabaseContext dbContext)
        {
            this._logger = logger;
            this._dbContext = dbContext;
        }

        //[HttpGet]   
        //public async Task<List<Book>> GetAllBooksAsync()
        //{
        //    var books = new List<Book>();
        //    return books;
        //}

        //[HttpGet("{id}")]
        //public async Task<Book> GetBookAsync()
        //{
        //    var book = new Book();
        //    return book;
        //}

        //[HttpPost]
        //public async Task<Book> AddBookAsync()
        //{
        //    var book = new Book();
        //    return book;
        //}

        //[HttpPut("{id}")]
        //public async Task<Book> UpdateBookAsync()
        //{
        //    var book = new Book();
        //    return book;
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBookAsync()
        //{
        //    // find book

        //    // if no book then log warning and return NotFound

        //    try
        //    {
        //        // delete logic
        //        _logger.LogInformation($"DeleteBookAsync - Book Deleted - {id} - User");
        //    } catch (Exception ex) {
        //        _logger.LogCritical($"DeleteBookAsync - Unable to delete - {00id} - User");
        //    }
            
        //    return Accepted( new { message = "Book deleted"});
        //}

    }
}
