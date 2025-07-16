using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Models;

namespace UrlShortenerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<URLItem> Urls { get; set; } = null!; // Representa a tabela de URLs no banco

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garante que o ShortCode seja único e não nulo
            modelBuilder.Entity<URLItem>()
                .HasIndex(u => u.ShortCode)
                .IsUnique();

            modelBuilder.Entity<URLItem>()
                .Property(u => u.ShortCode)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}