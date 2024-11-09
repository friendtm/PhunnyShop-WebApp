namespace PhunnyShop.Models.EquipmentViews
{
    public class UserRepairHistoryView
    {
        public ApplicationUser User { get; set; }
        public List<RepairHistory> RepairHistoryEntries { get; set; }
    }
}
