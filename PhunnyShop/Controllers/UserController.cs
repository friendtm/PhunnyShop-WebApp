using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;

namespace PhunnyShop.Controllers
{
	public class UserController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Subscriptions()
        {
            return View();
        }
    }
}
