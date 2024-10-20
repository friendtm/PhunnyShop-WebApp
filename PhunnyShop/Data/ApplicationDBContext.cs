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

    }
}
