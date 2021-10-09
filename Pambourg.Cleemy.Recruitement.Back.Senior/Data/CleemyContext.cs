using Microsoft.EntityFrameworkCore;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Data
{
    public class CleemyContext : DbContext
    {
        public CleemyContext(DbContextOptions<CleemyContext> options) : base(options)
        {

        }

        public DbSet<Expenditure> Expenditures { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expenditure>().ToTable(nameof(Expenditure));
            modelBuilder.Entity<User>().ToTable(nameof(User));
        }
    }
}
