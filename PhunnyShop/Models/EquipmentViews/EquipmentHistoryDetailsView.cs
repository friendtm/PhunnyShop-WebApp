namespace PhunnyShop.Models.EquipmentViews
{
    public class EquipmentHistoryDetailsView
    {
        public int RepairId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentModel { get; set; }
        public DateTime RepairStart { get; set; }
        public DateTime? RepairFinish { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        // User information
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserContact { get; set; }
    }
}
