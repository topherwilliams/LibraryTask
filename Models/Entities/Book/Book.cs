namespace LibraryTask.Models.Entities.Book
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublishedYear { get; set; }
        public string ISBN { get; set; }
    }
}
