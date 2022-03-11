using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.DAL.Database
{
    public sealed class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(employee => employee.Id);
            modelBuilder.Entity<TaskModel>()
                .HasNoKey();
            modelBuilder.Entity<Report>()
                .HasNoKey();
            modelBuilder.Entity<Comment>()
                .HasNoKey();
        }
    }
}