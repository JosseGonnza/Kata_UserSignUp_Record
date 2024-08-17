using Kata_UserSignUp_Record.Models;
using Kata_UserSignUp_Record.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Kata_UserSignUp_Record.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var emailConverter = new EmailConverter();
            var passwordConverter = new PasswordConverter();

            modelBuilder.Entity<User>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Id).ValueGeneratedOnAdd();
                    e.Property(x => x.Email)
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasConversion(emailConverter);
                    e.Property(x => x.Password)
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasConversion(passwordConverter);
                    e.ToTable($"{nameof(User)}s");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
