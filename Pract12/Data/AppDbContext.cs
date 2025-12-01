using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract12.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=HOME-PC;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>() // отношение один-к-одному
            .HasOne(s => s.UserProfile)
            .WithOne(ps => ps.User)
            .HasForeignKey<UserProfile>(ps => ps.UserId);

            modelBuilder.Entity<Role>() // отношение один-ко-многим
            .HasMany(g => g.Users)
            .WithOne(s => s.Role)
            .HasForeignKey(s => s.RoleId);
        }
    }
}
