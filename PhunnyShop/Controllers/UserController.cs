using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;
using PhunnyShop.Models.Account;
using Microsoft.AspNetCore.Authorization;
using PhunnyShop.Services;
using Microsoft.AspNetCore.Identity;

namespace PhunnyShop.Controllers
{
    [Authorize]
	public class UserController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserService _userService;
        private readonly ApplicationDBContext _db;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService, ApplicationDBContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _db = db;
        }

        [HttpGet]
        [Route("User/Index/{email}")]
        public IActionResult Index(string email)
        {
            if (email != User.Identity.Name)
            {
                return Forbid(); // Deny access if the email does not match the logged-in user's email
            }

            // Fetch user data using the provided email
            var userData = _userService.GetUserDataByEmail(email);

            if (userData == null)
            {
                return NotFound(); // Return a 404 error if no user is found
            }

            // Fetch the equipment repairs for the current user
            var repairs = _db.EquipmentRepairs
                                  .Where(r => r.UserId == userData.Id) // Filter by UserId
                                  .ToList();

            // Create a ViewModel to hold both user data and repairs
            var viewModel = new UserEquipmentView
            {
                User = userData,
                EquipmentRepairs = repairs
            };

            // Pass the retrieved user data to the view
            return View(viewModel);
        }

        public IActionResult History(string email)
        {
            // Criar lógica para retornar o Histórico do User.
            return View();
        }

        public IActionResult Subscriptions()
        {
            // Criar lógica para retornar as Subscrições disponiveis.
            return View();
        }

        public IActionResult Buy()
        {
            // Criar lógica para atribuir a Subscrição ao User

            return RedirectToAction("Subscriptions");
        }
    }
}
