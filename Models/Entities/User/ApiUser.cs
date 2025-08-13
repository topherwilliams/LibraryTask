using Microsoft.AspNetCore.Identity;

namespace LibraryTask.Models.Entities.User
{
    public class ApiUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
