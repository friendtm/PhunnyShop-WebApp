using Microsoft.AspNetCore.Mvc.Rendering;
using PhunnyShop.Models.LoginRegister;

namespace PhunnyShop.Models.Account
{
    public class UserEquipmentView
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }
        public List<EquipmentRepair>? EquipmentRepairs { get; set; }

        public IEnumerable<SelectListItem>? Users { get; set; }
    }
}
