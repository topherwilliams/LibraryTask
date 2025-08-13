using LibraryTask.Config;

namespace LibraryTask.Services
{
    public class BaseService<T>
    {
        protected readonly DatabaseContext Db;
        protected readonly ILogger<T> Logger;

        protected BaseService(DatabaseContext db, ILogger<T> logger)
        {
            Db = db;
            Logger = logger;
        }
    }
}
