/*
 * Campos adicionais para o User.
 */

using Microsoft.AspNetCore.Identity;

namespace PhunnyShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public DateTime DateOfBirth { get; set; }
        public string Contact { get; set; }

    }
}
