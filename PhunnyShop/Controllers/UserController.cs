using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace PhunnyShop.Controllers
{
    [Authorize]
	public class UserController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
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
