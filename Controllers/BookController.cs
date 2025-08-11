using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Services.BookServices;
using Microsoft.AspNetCore.Mvc;


namespace LibraryTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        //TODO: 'Production ready' - Would want logging (even basic), authorization
        // In memory storage

        private readonly ILogger _logger;
        private readonly DatabaseContext _dbContext;

        public BookController(ILogger<BookController> logger, DatabaseContext dbContext) 
        {
            this._logger = logger;
            this._dbContext = dbContext;
        }

        [HttpGet]   
        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await BookService.GetAllBooks(_dbContext, _logger);
            return books;
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookAsync(int id)
        {
            return await BookService.GetBook(id, _dbContext, _logger);
        }

        [HttpPost]
        public async Task<Book> AddBookAsync([FromBody]Book newBook)
        {
            return await BookService.AddNewBook(newBook, _dbContext, _logger);
        }

        [HttpPut("{id}")]
        public async Task<Book> UpdateBookAsync(int id, [FromBody]Book updatedBook)
        {
            var book = new Book();
            return book;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var res = await BookService.DeleteBook(id, _dbContext, _logger);
            if (!res)
            {
                return NotFound();
            }
            return Ok( new { message = "Book deleted"});
        }

    }
}
