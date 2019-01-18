using Microsoft.EntityFrameworkCore;
using TaxCalculatorApp.API.models;

namespace TaxCalculatorApp.API.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<TaxCalculatedValue> TaxCalculatedValues { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);
            builder.Entity<PostalCode>()
                .HasOne(t => t.TaxCalculationType)
                .WithMany(c => c.PostalCodes)
                .HasForeignKey(f => f.TaxCalculationTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<TaxCalculationType>().HasData(
                new TaxCalculationType { Id = 1, Type = "Progressive" },
                new TaxCalculationType { Id = 2, Type = "Flat Value" },
                new TaxCalculationType { Id = 3, Type = "Flat Rate" }
            );
            builder.Entity<PostalCode>().HasData(
                new PostalCode { Id = 1, Code = "7441", TaxCalculationTypeId = 1 },
                new PostalCode { Id = 2, Code = "A100", TaxCalculationTypeId = 2 },
                new PostalCode { Id = 3, Code = "7000", TaxCalculationTypeId = 3 },
                new PostalCode { Id = 4, Code = "1000", TaxCalculationTypeId = 1}
            );
        }
    }
}