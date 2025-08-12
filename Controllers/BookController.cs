using LibraryTask.Config;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Services.BookServices;
using LibraryTask.Utils.Constants;
using Microsoft.AspNetCore.Mvc;


namespace LibraryTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        //TODO: 'Production ready' - Would want logging (even basic), authorization

        private readonly ILogger _logger;
        private readonly DatabaseContext _dbContext;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, DatabaseContext dbContext, IBookService bookService) 
        {
            this._logger = logger;
            this._dbContext = dbContext;
            this._bookService = bookService;
        }

        [HttpGet]   
        public async Task<List<Book>> GetAllBooksAsync([FromQuery] int page = 1, [FromQuery] int take = 10)
        {
            var books = await _bookService.GetAllBooks(page, take);
            return books;
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookAsync(int id)
        {
            return await _bookService.GetBook(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody]Book newBook)
        {
            var res = await _bookService.AddNewBook(newBook);
            if (!res.Success)
            {
                return res.ErrorMessage switch
                {
                    ErrorMessages.IsbnAlreadyExists => Conflict(res.ErrorMessage),
                    ErrorMessages.BookNotFound => NotFound(res.ErrorMessage),
                    _ => BadRequest(res.ErrorMessage),
                };
            }

            return Ok(res.Result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody]Book updatedBook)
        {
            var res = await _bookService.UpdateBook(updatedBook, id);

            if (!res.Success)
            {
                return res.ErrorMessage switch
                {
                    ErrorMessages.IsbnAlreadyExists => Conflict(res.ErrorMessage),
                    ErrorMessages.BookNotFound => NotFound(res.ErrorMessage),
                    _ => BadRequest(res.ErrorMessage),
                };
            }

            return Ok(res.Result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var res = await _bookService.DeleteBook(id);
            if (!res.Success)
            {
                return res.ErrorMessage switch
                {
                    ErrorMessages.BookNotFound => NotFound(res.ErrorMessage),
                    _ => BadRequest(res.ErrorMessage)
                };
            }
            return Ok( new { message = "Book deleted"});
        }
    }
}
