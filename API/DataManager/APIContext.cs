using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace API.DataManager
{
    public class APIContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        string ConnString = "Server=127.0.0.1; User ID=root; Password=1234567890; Database=cybersecurity";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnString, ServerVersion.AutoDetect(ConnString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(z => z.RoleId);
                entity.HasMany(a => a.Users)
                    .WithOne(a => a.UserRole)
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();
            });


        }
    }
}
