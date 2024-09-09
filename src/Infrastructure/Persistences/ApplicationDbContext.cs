using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.User;

namespace Infrastructure.Persistence
{
    // ApplicationDbContext inherits from IdentityDbContext to integrate ASP.NET Core Identity
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSet for each entity in your system
        public DbSet<Employee> Employees { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        // Override OnModelCreating to configure relationships and additional settings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add your custom configurations for entities here

            // Employee and ApplicationUser one-to-one relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ApplicationUser)
                .WithOne()
                .HasForeignKey<Employee>(e => e.ApplicationUserId);

            // Seed initial roles using IdentityRole<int> (since you are using int for PK)
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "HR", NormalizedName = "HR" },
                new IdentityRole<int> { Id = 3, Name = "Employee", NormalizedName = "EMPLOYEE" },
                new IdentityRole<int> { Id = 4, Name = "JuniorEmployee", NormalizedName = "JUNIOREMPLOYEE" },
                new IdentityRole<int> { Id = 5, Name = "SeniorEmployee", NormalizedName = "SENIOREMPLOYEE" }
            );
        }
    }
}
