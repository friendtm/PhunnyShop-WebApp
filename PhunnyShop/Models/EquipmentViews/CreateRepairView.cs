using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PhunnyShop.Models.EquipmentViews
{
    public class CreateRepairView
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }
        public List<EquipmentRepair>? EquipmentRepairs { get; set; }

        public IEnumerable<SelectListItem>? Users { get; set; }
    }
}

