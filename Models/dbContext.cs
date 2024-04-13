using Microsoft.EntityFrameworkCore;
using Nuova_cartella.Models;
public class dbContext : DbContext { 
    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
        {
        }
    public DbSet<Registrazione>Registrazioni{ get; set; }
    public DbSet<Purchase> Purchase { get; set; } = default!;
}
