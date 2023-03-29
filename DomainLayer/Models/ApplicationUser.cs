using Microsoft.AspNetCore.Identity;

namespace BookLibrary.Domain.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
