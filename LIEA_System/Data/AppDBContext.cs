using LIEA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace LIEA_System.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
        }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relaciones Venta-DetalleVenta
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.DetallesVenta)
                .WithOne(dv => dv.Venta)
                .HasForeignKey(dv => dv.Id_Venta)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Compra-DetalleCompra
            modelBuilder.Entity<Compra>()
                .HasMany(c => c.DetallesCompra)
                .WithOne(dc => dc.Compra)
                .HasForeignKey(dc => dc.Id_Compra)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Cliente-Venta
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.Id_Cliente)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Proveedor-Compra
            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Compras)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.Id_Proveedor)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Proveedor-Producto
            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Productos)
                .WithOne(pr => pr.Proveedor)
                .HasForeignKey(pr => pr.Id_Proveedor)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Producto-DetalleVenta
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Ventas)
                .WithOne(dv => dv.Producto)
                .HasForeignKey(dv => dv.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Relaciones Producto-DetalleCompra
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Compras)
                .WithOne(dc => dc.Producto)
                .HasForeignKey(dc => dc.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada
        }

    }
}
