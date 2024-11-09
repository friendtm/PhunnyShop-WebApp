using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhunnyShop.Models
{
    public class EquipmentRepair
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign Key
        public ApplicationUser User { get; set; }  // Navigator
        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime RepairStart { get; set; } = DateTime.Now;  // Assim que o registo acontecer.
        public DateTime? RepairFinish { get; set; }  // Opcional.
        public string Status { get; set; } = "Em Espera";  // Default é 'Em Espera..'
        public bool IsCompleted { get; set; } = false;  // New field to mark completion
        public string? Description {  get; set; }
    }
}
