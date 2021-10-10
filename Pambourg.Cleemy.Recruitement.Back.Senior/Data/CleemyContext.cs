using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Data
{
    public class CleemyContext : DbContext
    {
        public CleemyContext(DbContextOptions<CleemyContext> options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ExpenseType>(entity =>
            {
                entity.ToTable(nameof(ExpenseType));
                entity.HasKey(et => et.ID);

                entity.Property(et => et.Label)
                .IsRequired();
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable(nameof(Currency));
                entity.HasKey(e => e.ID);

                entity.Property(c => c.Code)
                .IsRequired();

                entity.Property(c => c.Label)
                 .IsRequired();
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable(nameof(Expense));
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Comment)
                 .IsRequired();

                entity.HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));
                entity.HasKey(u => u.ID);

                entity.Property(u => u.FirstName)
                 .IsRequired();

                entity.Property(u => u.LastName)
                 .IsRequired();

                entity.HasMany(u => u.Expenses)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
