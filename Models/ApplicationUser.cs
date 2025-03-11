using Microsoft.AspNetCore.Identity;

namespace JwtAuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } 
    }
}
