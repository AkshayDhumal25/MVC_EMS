using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_EMS.Models;

namespace MVC_EMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }

        public DbSet<Country> countries { get; set; }

        public DbSet<State> states { get; set; }

        public DbSet<City> cities { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<SelectListGroup>();
            modelBuilder.Ignore<SelectListItem>();


            modelBuilder.Entity<EmployeeMaster>()
                .HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeMaster>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeMaster>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }




    }

}

