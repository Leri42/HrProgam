using HrProgramWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HrProgramWeb.Data
{
    public class ApplicationDbContext :IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options ) : base( options )
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Employees)
                .WithOne(u => u.ApplicationUser)
                .IsRequired()
                .HasForeignKey(p => p.ApplicationUserId);

            base.OnModelCreating(builder);
        }
    }

}
