using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
