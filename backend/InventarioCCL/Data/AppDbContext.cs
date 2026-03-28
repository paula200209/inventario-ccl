using Microsoft.EntityFrameworkCore;
using InventarioCCL.Models;

namespace InventarioCCL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("usuarios");
        modelBuilder.Entity<Producto>().ToTable("productos");
        modelBuilder.Entity<Movimiento>().ToTable("movimientos");

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("numeric(10,2)");
    }
}
