namespace LibraryTask.Models.Entities.Book
{
    public interface IBook
    {
        int Id { get; set; }
        string Title { get; set; }
        int AuthorId { get; set; }
        int PublishedYear { get; set; }
        string ISBN { get; set; }
    }
}
