using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tienda.Models
{
    public partial class TiendaCTX : DbContext
    {
        public TiendaCTX()
        {
        }

        public TiendaCTX(DbContextOptions<TiendaCTX> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Compras> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompra { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.56.3;Database=Tienda;Trusted_Connection=False; User ID=sa;Password=@Avrill1990;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A104598A40C");

                entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__D594664210FFEE31");
            });

            modelBuilder.Entity<Compras>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__0A5CDB5C34469791");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compras__IdProve__5070F446");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCompra)
                    .HasName("PK__DetalleC__E046CCBB6FA39771");

                entity.Property(e => e.Total).HasComputedColumnSql("([Precio]*[Cantidad])");

                entity.HasOne(d => d.Compra)
                    .WithMany(p => p.DetalleCompra)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DetalleCo__IdCom__45F365D3");
               
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DetalleV__AAA5CEC2B1C74364");

                entity.Property(e => e.Total).HasComputedColumnSql("([Precio]*[Cantidad])");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleVe__IdPro__48CFD27E");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__IdVen__47DBAE45");
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__098892105A875A82");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Productos__IdCat__440B1D61");
            });

            modelBuilder.Entity<Proveedores>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__E8B631AF3B3C3590");
            });

            modelBuilder.Entity<Ventas>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Ventas__BC1240BD1BCB2200");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ventas__IdClient__4F7CD00D");
            });
        }
    }
}
