using LibraryTask.Models.Entities.Book;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace LibraryTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {

        [HttpGet]   
        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = new List<Book>();
            return books;
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookAsync()
        {
            var book = new Book();
            return book;
        }

        [HttpPost]
        public async Task<Book> AddBookAsync()
        {
            var book = new Book();
            return book;
        }

        [HttpPut("{id}")]
        public async Task<Book> UpdateBookAsync()
        {
            var book = new Book();
            return book;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync()
        {
            
            return Accepted( new { message = "Book deleted"});
        }

    }
}
