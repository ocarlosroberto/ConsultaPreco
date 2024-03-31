using ConsultaPreco.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultaPreco.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.CodigoBarras);
        }
    }
}
