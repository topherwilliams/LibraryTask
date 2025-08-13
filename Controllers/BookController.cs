using LibraryTask.Config;
using LibraryTask.Models.DTOs;
using LibraryTask.Models.Entities.Book;
using LibraryTask.Services.BookService;
using LibraryTask.Utils.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryTask.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : BaseController<BookController>
    {
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, DatabaseContext dbContext, IBookService bookService) :  base(logger, dbContext)
        {
            this._bookService = bookService;
        }

        [HttpGet]   
        public async Task<List<Book>> GetAllBooksAsync([FromQuery] int page = 1, [FromQuery] int take = 10)
        {
            return await _bookService.GetAllBooks(page, take);
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
                return ResolveServiceResultErrorToIActionResult(res.ErrorMessage);
            }

            return Ok(res.Result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var res = await _bookService.DeleteBook(id);
            if (!res.Success)
            {
                return ResolveServiceResultErrorToIActionResult(res.ErrorMessage);
            }
            return Ok( new { message = "Book deleted"});
        }

        protected IActionResult ResolveServiceResultErrorToIActionResult(string errorMessage)
        {
            return errorMessage switch
            {
                ErrorMessages.IsbnAlreadyExists => Conflict(errorMessage),
                ErrorMessages.BookNotFound => NotFound(errorMessage),
                _ => BadRequest(errorMessage),
            };
        }
    }
}
