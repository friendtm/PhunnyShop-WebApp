/*
* Responsável pelos Users.
* Aqui é onde está a Lógica de Registo, Login e Logout.
* Campos adicionais para o User devem ser criados em Models/ApplicationUser.cs
* Não esquecer de dar update no método Register.
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhunnyShop.Data;
using PhunnyShop.Models;
using PhunnyShop.Models.Account;
using PhunnyShop.Models.LoginRegister;
using PhunnyShop.Services;
using System.Threading.Tasks;

namespace PhunnyShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserService _userService;
		private readonly ApplicationDBContext _db;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService, ApplicationDBContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
			_db = db;
		}

        // This action will be accessible only to logged-in users
        /*
        [Authorize]
        public IActionResult Index()
        {
            // You can access the current user info using User.Identity.Name or User.FindFirst()
            var currentUser = User.Identity.Name;

            return View();
        } */

        // This action will render a personal page based on the username (e.g., /xxxx, /yyyy)
        [Authorize]
        [HttpGet]
        [Route("Account/Index/{email}")]
        public async Task<IActionResult> Index(string email)
        {
			/* Versão 1
            if (email != User.Identity.Name)
            {
                return Forbid(); // If the user tries to access someone else's profile, deny access
            }

            // Fetch user data using ApplicationUser
            var userData = _userService.GetUserDataByEmail(email);

            if (userData == null)
            {
                return NotFound(); // If no user found, return 404 Not Found
            }

            // Pass ApplicationUser data to the view
            return View(userData);
            */

			// Versão 2

			// Check if the email provided matches the currently logged-in user's email
			if (email != User.Identity.Name)
			{
				return Forbid(); // Deny access if the email does not match the logged-in user's email
			}

			// Fetch user data using the provided email
			var userData = _userService.GetUserDataByEmail(email);
            var user = await _userManager.GetUserAsync(User);

            if (userData == null)
			{
				return NotFound(); // Return a 404 error if no user is found
			}

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "Admin");  // Redirects admin users
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

        // GET: Displays the combined Login and Register view
        [HttpGet]
        public IActionResult LoginRegister()
        {
            var model = new LoginRegisterView
            {
                RegisterModel = new RegisterViewModel(),
                LoginModel = new LoginViewModel()
            };

            return View(model);
        }

        // POST: Handles registration
        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterView model)
        {
            if (!ModelState.IsValid)
            {
                // Return the combined view if the model state is invalid
                return View("LoginRegister", model);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.RegisterModel.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already in use.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.RegisterModel.FirstName,
                LastName = model.RegisterModel.LastName,
                Contact = model.RegisterModel.Contact,
                Email = model.RegisterModel.Email,
                UserName = model.RegisterModel.Email // Ensure UserName is set
            };

            var result = await _userManager.CreateAsync(user, model.RegisterModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Account", new { email = User.Identity.Name}); // Redirect to a new action on success
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Return the view with errors if registration fails
            return View("LoginRegister", model);
        }

        // POST: Handles login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRegisterView model)
        {
            if (!ModelState.IsValid)
            {
                // Return the combined view if model state is invalid
                return View("LoginRegister", model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.LoginModel.Email,
                model.LoginModel.Password,
                model.LoginModel.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account", new { email = User.Identity.Name }); // Redirect to a new action on success
            }

            TempData["ErrorMessage"] = "Login inválido. Verifica as credenciais.";
            return View("LoginRegister", model);
        }

        // POST: Handles logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

