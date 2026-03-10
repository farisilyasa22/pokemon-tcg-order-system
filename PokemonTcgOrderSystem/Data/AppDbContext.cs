using Microsoft.EntityFrameworkCore;
using PokemonTcgOrderSystem.Models;

namespace PokemonTcgOrderSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
