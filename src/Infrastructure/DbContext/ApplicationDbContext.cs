using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.User;

namespace Infrastructure.DbConetxt
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSet for each entity
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee and related entities relationships
            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.KPIs)
                .WithOne(k => k.Employee)
                .HasForeignKey(k => k.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.Requests)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.Attendances)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.Documents)
                .WithOne(d => d.Employee)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.Feedbacks)
                .WithOne(f => f.Employee)
                .HasForeignKey(f => f.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.CreatedBy)
                .WithMany()
                .HasForeignKey(m => m.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.UpdatedBy)
                .WithMany()
                .HasForeignKey(m => m.UpdatedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure RolePermission relationship
            modelBuilder.Entity<RolePermission>()
                 .HasOne(rp => rp.Role)
                 .WithMany()
                 .HasForeignKey(rp => rp.RoleId)
                 .OnDelete(DeleteBehavior.Restrict);


            // Configure relationships for the Announcement entity
            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.PostedBy)
                .WithMany()
                .HasForeignKey(a => a.PostedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentModel>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Departments)
            .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<DepartmentModel>()
                .HasOne(d => d.Director)
                .WithOne()
                .HasForeignKey<EmployeeModel>(e => e.DepartmentId);

            // Configure other relationships if needed

            // Seed initial roles
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
