/*
 * Ao criar um Modelo novo, temos que o adicionar na DB Context.
 * Verificar ConnectionString em 'appsetings.json'
 * Verificar a Connection em Program.cs
 * 
 * #### OnModelCreating() ####
 * Dizemos que queremos criar aqueles 3 registos depois de criada a Tabela.
 * ##########################################################################
 * 
 */

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhunnyShop.Models;

namespace PhunnyShop.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }  // Category é o nosso ficheiro 'Category.cs'. 'Categories' será o nome da tabela.
        public DbSet<EquipmentRepair> EquipmentRepairs { get; set; }
        public DbSet<RepairHistory> RepairsHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Override
        }
    }
}
