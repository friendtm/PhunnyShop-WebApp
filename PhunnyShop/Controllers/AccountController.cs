    /*
 * Responsável pelos Users.
 * Aqui é onde está a Lógica de Registo, Login e Logout.
 * Campos adicionais para o User devem ser criados em Models/ApplicationUser.cs
 * Não esquecer de dar update no método Register.
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Models;
using System.Threading.Tasks;

namespace PhunnyShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return RedirectToAction("Index", "User"); // Redirect to a new action on success
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
                return RedirectToAction("Index", "User"); // Redirect to a new action on success
            }

            TempData["ErrorMessage"] = "Login inválido. Verifica as credenciais.";
            return View("LoginRegister", model);
        }

        // POST: Handles logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoginRegister", "Account");
        }
    }
}

