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

        public IActionResult Create()
        {
            return View();
        }

		/*
         * Ao receber um POST executamos o método.
         * Nota que o utilizamos o mesmo método 'Create', com um objeto to tipo 'Category' chamado 'obj'.
         * Definimos as alterações que queremos fazer. Neste caso adicionar um registo que chega pelo método POST.
         * Usamos a função .Add() para adicionar o Objecto 'Category' às alterações.
         * Executamos as alterações com a função .SaveChanges().
         * Redirecionamos para uma página com o método RedirectToAction().
         * No Método de Redirect mencionamos o Método para onde queremos ir (Neste caso, 'Index') e o Controlador de seguida ('Category').
         * Como estamos no mesmo controlador não precisamos de o mencionar, mas para servir de exemplo, fica explicito.
         */

		[HttpPost]
        public IActionResult Create(Category obj)
		{
            _db.Categories.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
    }
}
