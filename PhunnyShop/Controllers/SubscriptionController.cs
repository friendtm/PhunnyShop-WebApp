using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;

namespace PhunnyShop.Controllers
{
	public class SubscriptionController : Controller
	{
		private readonly ApplicationDBContext _db;

		public SubscriptionController(ApplicationDBContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			List<Subscription> objSubscriptionList = _db.Subscriptions.ToList();

			return View(objSubscriptionList);
		}
	}
}
