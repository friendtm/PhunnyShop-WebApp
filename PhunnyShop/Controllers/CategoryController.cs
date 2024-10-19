/*
 * Ao chamar o método 'Index', ele vai procurar a view respetiva ao controlador: Past Views > Category
 * Dentro da pasta 'Category' encontramos a view 'Index.cshtml'.
 * 
 * Caso ele não encontre a pasta 'Category', ele irá procurar o index dentro da pasta 'Shared'.
 */

using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;

namespace PhunnyShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;  // Local Variable. Permite a utilização da Variavel em outros métodos.

        public CategoryController(ApplicationDBContext db)  // Criamos o objecto 'db' da Classe ApplicationDBContext.
        {
            _db = db;  // Associamos o objecto 'db' à Local Variable '_db'.
        }

        public IActionResult Index()
        {
            // Criamos uma lista chamada 'objCategoryList' do tipo 'Category'. Category.cs na pasta 'Models'.
            // A Lista é populada com a função ToList() na tabela 'Categories'.
            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);  // Passamos o objecto para a view.
        }
    }
}
