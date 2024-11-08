using PhunnyShop.Data;
using PhunnyShop.Models;

namespace PhunnyShop.Services
{
    public class UserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        // Method to get user data by username
        public ApplicationUser GetUserDataByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email); // Fetch user by their username
        }
    }
}
