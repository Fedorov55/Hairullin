using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hairullin.Entities;
using Microsoft.EntityFrameworkCore;


namespace Hairullin
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public ApplicationDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
               .HasKey(u => u.Id);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host = 10.0.2.2; Database = Hairullin; User = root; Password = 1234;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
