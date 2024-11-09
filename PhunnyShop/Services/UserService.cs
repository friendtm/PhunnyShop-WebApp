using PhunnyShop.Data;
using PhunnyShop.Models;

namespace PhunnyShop.Services
{
    public class UserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        // Method to get user data by username
        public ApplicationUser GetUserDataByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email); // Fetch user by their username
        }

        public async Task CompleteRepairAsync(int repairId)
        {
            var equipmentRepair = await _context.EquipmentRepairs.FindAsync(repairId);

            if (equipmentRepair != null && equipmentRepair.Status == "Concluído")
            {
                var historyEntry = new RepairHistory
                {
                    RepairId = equipmentRepair.Id,
                    UserId = equipmentRepair.UserId,
                    Name = equipmentRepair.Name,
                    Model = equipmentRepair.Model,
                    RepairStart = equipmentRepair.RepairStart,
                    RepairFinish = DateTime.Now,
                    Status = equipmentRepair.Status
                };

                _context.RepairsHistory.Add(historyEntry);
                _context.EquipmentRepairs.Remove(equipmentRepair);  // Optional if you want to delete it
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevertRepairAsync(int historyId)
        {
            var historyEntry = await _context.RepairsHistory.FindAsync(historyId);

            if (historyEntry != null)
            {
                var equipmentRepair = new EquipmentRepair
                {
                    Id = historyEntry.RepairId,
                    UserId = historyEntry.UserId,
                    Name = historyEntry.Name,
                    Model = historyEntry.Model,
                    RepairStart = historyEntry.RepairStart,
                    Status = "Em Andamento"  // Reset status as needed
                };

                _context.EquipmentRepairs.Add(equipmentRepair);
                _context.RepairsHistory.Remove(historyEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
