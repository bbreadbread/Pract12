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
        public DbSet<UserInterestGroup> UserInterestGroup { get; set; }
        public DbSet<InterestGroup> InterestGroup { get; set; }
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

            modelBuilder.Entity<UserInterestGroup>()
                        .HasKey(cs => new { cs.UserId, cs.InterestGroupId });

            // cвязь с таблицей Student
            modelBuilder.Entity<UserInterestGroup>()
            .HasOne(cs => cs.User)
            .WithMany(s => s.UserInterestGroup)
            .HasForeignKey(cs => cs.UserId);

            // связь с таблицей Cource
            modelBuilder.Entity<UserInterestGroup>()
            .HasOne(cs => cs.InterestGroup)
            .WithMany(c => c.UserInterestGroup)
            .HasForeignKey(cs => cs.InterestGroupId);
        }
    }
}
