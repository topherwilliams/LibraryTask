using LibraryTask.Config;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTask.Controllers
{
    [ApiController]
    public abstract class BaseController<T> : Controller
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _dbContext;

        public BaseController(ILogger<T> logger, DatabaseContext dbContext)
        {
            this._logger = logger;
            this._dbContext = dbContext;
        }

    }
}
