using System.ComponentModel.DataAnnotations;

namespace LibraryTask.Models.Entities.Book
{
    public class Book : IBook
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author ID is required")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Published year is required")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }
    }
}
