/*
 * Ao chamar o método 'Index', ele vai procurar a view respetiva ao controlador: Past Views > Category
 * Dentro da pasta 'Category' encontramos a view 'Index.cshtml'.
 * 
 * Caso ele não encontre a pasta 'Category', ele irá procurar o index dentro da pasta 'Shared'.
 */

using Microsoft.AspNetCore.Mvc;
using PhunnyShop.Data;
using PhunnyShop.Models;
using Microsoft.AspNetCore.Authorization;
using PhunnyShop.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhunnyShop.Models.EquipmentViews;
using System.Diagnostics;
using PhunnyShop.Services;

namespace PhunnyShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly ApplicationDBContext _db;  // Local Variable. Permite a utilização da Variavel em outros métodos.
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDBContext db, UserManager<ApplicationUser> userManager, UserService userService)  // Criamos o objecto 'db' da Classe ApplicationDBContext.
        {
            _db = db;  // Associamos o objecto 'db' à Local Variable '_db'.
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
			var repairs = _db.EquipmentRepairs
						  .Include(r => r.User) // This ensures the User is included
						  .ToList();

			return View(repairs);
		}

        public IActionResult Create()
        {
            var viewModel = new UserEquipmentView
            {
                Users = _userManager.Users
                .Select(user => new SelectListItem
                {
                    // Text = $"{user.FirstName} {user.LastName}",  Combine first and last name, or modify to your needs
                    Text = $"{user.Email}",
                    Value = user.Id // The UserId
                })
                .ToList()
            };

            return View(viewModel);
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
        public async Task<IActionResult> Create(UserEquipmentView viewModel)
        {
            if (ModelState.IsValid)
            {
                var equipmentRepair = new EquipmentRepair
                {
                    Name = viewModel.Name,
                    Model = viewModel.Model,
                    UserId = viewModel.UserId  // Selected UserId
                };

                // Add the new repair record to the database
                _db.Add(equipmentRepair);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // If validation fails, repopulate the Users list
            viewModel.Users = _userManager.Users
                .Select(user => new SelectListItem
                {
                    // Text = $"{user.FirstName} {user.LastName}",
                    Text = $"{user.Email}",
                    Value = user.Id
                })
                .ToList();

            return View(viewModel);
        }

		public async Task<IActionResult> ChangeStatus(int id)
		{
			var repair = await _db.EquipmentRepairs.FindAsync(id);
			if (repair == null)
			{
				return NotFound();
			}

			var model = new EditRepairStatusView
			{
				Id = repair.Id,
				CurrentStatus = repair.Status,
				StatusOptions = GetStatusOptions()  // Reusable method to get status options
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeStatus(EditRepairStatusView model)
		{
            // Log the model state to check if it's valid
            Debug.WriteLine($"Model state is valid: {ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                Debug.WriteLine($"Model Validated, CurrentStatus: {model.CurrentStatus}");

                // Fetch the repair from the database
                var repair = await _db.EquipmentRepairs.FindAsync(model.Id);
                if (repair == null)
                {
                    return NotFound();
                }

                // Update the repair status
                repair.Status = model.CurrentStatus;

                // Check if the status is "Complete" and call the CompleteRepairAsync method
                if (repair.Status == "Concluído")
                {
                    await _userService.CompleteRepairAsync(repair.Id);
                }
                else
                {
                    _db.Update(repair);
                    await _db.SaveChangesAsync();
                }

                try
                {
                    // Save changes to the database
                    _db.Update(repair);
                    await _db.SaveChangesAsync();

                    Debug.WriteLine("Status changed successfully");

                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error saving changes: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating the status.");
                }
            }
            else
            {
                // If model is not valid, log the validation errors
                Debug.WriteLine("Model is not valid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            // Return the view with the model in case of errors
            model.StatusOptions = GetStatusOptions();

            return View(model);
        }

        // Helper method to populate User list in Create view
        private List<SelectListItem> GetUserSelectListItems()
		{
			return _userManager.Users
							   .Select(user => new SelectListItem
							   {
								   Text = user.Email,  // Display the email or combine first and last name
								   Value = user.Id      // UserId as the value
							   })
							   .ToList();
		}

		// Helper method to get status options for ChangeStatus view
		private List<SelectListItem> GetStatusOptions()
		{
			return new List<SelectListItem>
			{
				new SelectListItem { Text = "Em Espera", Value = "Em Espera" },
				new SelectListItem { Text = "Em Andamento", Value = "Em Andamento" },
				new SelectListItem { Text = "Concluído", Value = "Concluído" }
			};
		}
	}
}
