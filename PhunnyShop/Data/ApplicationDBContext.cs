/*
 * Ao criar um Modelo novo, temos que o adicionar na DB Context.
 * Verificar ConnectionString em 'appsetings.json'
 * Verificar a Connection em Program.cs
 * 
 * 
 * #### CRIAR/ATUALIZAR BASE DE DADOS ####
 * 1º - Tools > Manage NuGet Packages > Console
 * 2º - Na Consola: update-database
 * ##########################################################################
 * 
 * 
 * #### CRIAR MIGRAÇÃO ####
 * 1º - Tools > Manage NuGet Packages > Console
 * 2º - Na Consola: add-migration NomeParaMigração (ex: FreshAddTables )
 * ##########################################################################
 * 
 * 
 * #### MIGRAÇÕES APLICADAS ####
 * Tables > EFMigrationsHistory > Select Top 1000
 * ##########################################################################
 * 
 * 
 * #### OnModelCreating() ####
 * Dizemos que queremos criar aqueles 3 registos depois de criada a Tabela.
 * ##########################################################################
 * 
 * 
 */

using Microsoft.EntityFrameworkCore;
using PhunnyShop.Models;  // Import nos Models para os utilizar nos 'DbSet'.

namespace PhunnyShop.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }  // Category é o nosso ficheiro 'Category.cs'. 'Categories' será o nome da tabela.

        public DbSet<Subscription> Subscriptions { get; set; }  // Criar a Tabela Subscriptions.

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Comedy", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Horror", DisplayOrder = 3 }
                );
		}
        */
    }
}
