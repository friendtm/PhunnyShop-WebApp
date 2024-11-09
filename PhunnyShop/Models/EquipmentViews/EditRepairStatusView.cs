using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PhunnyShop.Models.EquipmentViews
{
    public class EditRepairStatusView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a status")]
        public string CurrentStatus { get; set; }

		public List<SelectListItem>? StatusOptions { get; set; }
	}
}
