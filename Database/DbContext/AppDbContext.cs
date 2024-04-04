using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database.DatabaseContext
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Asset> Coins { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.User)
                .WithMany(p => p.Portfolios)
                .OnDelete(DeleteBehavior.Cascade) // If a user is deleted, delete associated portfolios
                .IsRequired(); 

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Asset)
                .WithMany(t =>t.Transactions)
                .OnDelete(DeleteBehavior.Cascade)  // If an asset is deleted, delete associated transactions
                .IsRequired();

            modelBuilder.Entity<Asset>()
                .HasOne(c => c.Portfolio)
                .WithMany(c => c.Assets)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Portfolio)
                .WithOne(a => a.Address)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<ApiKey>()
                .HasOne(a => a.Portfolio)
                .WithOne(a => a.ApiKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }
}
