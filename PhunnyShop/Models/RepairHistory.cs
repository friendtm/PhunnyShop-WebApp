namespace PhunnyShop.Models
{
    public class RepairHistory
    {
        public int Id { get; set; }
        public int RepairId { get; set; }  // References the EquipmentRepair entry
        public string UserId { get; set; }  // User related to the repair
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime RepairStart { get; set; }
        public DateTime RepairFinish { get; set; }
        public string Status { get; set; }
    }
}
