using LibraryTask.Config;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTask.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : Controller
    {
        protected readonly ILogger Logger;
        protected readonly DatabaseContext DbContext;

        protected BaseController(ILogger<T> logger, DatabaseContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

    }
}
