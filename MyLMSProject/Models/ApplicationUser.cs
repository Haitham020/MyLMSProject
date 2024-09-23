using Microsoft.AspNetCore.Identity;

namespace MyLMSProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool Active { get; set; } = false;
        public bool InActive { get; set; } = true;
    }
}
