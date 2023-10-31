using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .Property(s => s.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Card>()
                .Property(c => c.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CreateDate)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
