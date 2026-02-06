using BlogSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasIndex(r => r.RoleName).IsUnique();

            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Role>().HasIndex(r => r.RoleName).IsUnique();

            modelBuilder.Entity<User>().Property(u => u.IsActive).HasDefaultValue((byte)1);
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
