using LibraryTask.Models.Entities.Book;
using Microsoft.EntityFrameworkCore;

namespace LibraryTask.Config
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BookDb");
            //base.OnConfiguring(optionsBuilder);
        }

    }
}
