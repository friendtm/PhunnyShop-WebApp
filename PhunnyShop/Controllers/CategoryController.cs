/*
 * Ao chamar o método 'Index', ele vai procurar a view respetiva ao controlador: Past Views > Category
 * Dentro da pasta 'Category' encontramos a view 'Index.cshtml'.
 * 
 * Caso ele não encontre a pasta 'Category', ele irá procurar o index dentro da pasta 'Shared'.
 */

using Microsoft.AspNetCore.Mvc;

namespace PhunnyShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
