using Microsoft.AspNetCore.Identity;

namespace Task_Manager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
