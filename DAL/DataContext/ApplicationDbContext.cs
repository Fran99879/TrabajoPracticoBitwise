using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TrabajoPracticoBit.Models;

namespace TrabajoPracticoBit.DAL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 

        }     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>().Property(x => x.Nombre).HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(x => x.Descripcion).HasMaxLength(1000);
            modelBuilder.Entity<Producto>().Property(x => x.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Producto>().Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<Producto>().Property(e => e.Activo).HasColumnName("esActivo");

            modelBuilder.Entity<Categoria>().Property(x => x.Nombre).HasMaxLength(100);

            modelBuilder.Entity<Marca>().Property(x => x.Nombre).HasMaxLength(50);
            modelBuilder.Entity<Marca>().Property(x => x.Descripcion).HasMaxLength(1000);

        }


    

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
