using WorkerManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkerManagement.Data
{

    // Manages database operations for Workers, Employees, Supervisors, and Managers.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Worker>()
                .HasKey(w => w.WorkerID);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeID);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Worker)
                .WithMany()
                .HasForeignKey(e => e.WorkerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supervisor>()
                .HasKey(s => s.SupervisorID);
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Worker)
                .WithMany()
                .HasForeignKey(s => s.WorkerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Manager>()
                .HasKey(m => m.ManagerID);
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.Worker)
                .WithMany()
                .HasForeignKey(m => m.WorkerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
