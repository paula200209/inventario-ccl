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
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuarios");
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Username).HasColumnName("username");
            e.Property(p => p.Password).HasColumnName("password");
            e.Property(p => p.Activo).HasColumnName("activo");
        });

        modelBuilder.Entity<Producto>(e =>
        {
            e.ToTable("productos");
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Nombre).HasColumnName("nombre");
            e.Property(p => p.Precio).HasColumnName("precio").HasColumnType("numeric(10,2)");
            e.Property(p => p.Cantidad).HasColumnName("cantidad");
        });

        modelBuilder.Entity<Movimiento>(e =>
        {
            e.ToTable("movimientos");
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.ProductoId).HasColumnName("producto_id");
            e.Property(p => p.Tipo).HasColumnName("tipo");
            e.Property(p => p.Cantidad).HasColumnName("cantidad");
            e.Property(p => p.Fecha).HasColumnName("fecha");
            e.Property(p => p.Observacion).HasColumnName("observacion");
        });
    }
}