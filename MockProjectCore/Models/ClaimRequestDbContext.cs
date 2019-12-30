using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ClaimRequest.Core.Models
{
    public class ClaimRequestDbContext : IdentityDbContext<Staff>
    {
        public ClaimRequestDbContext() : base("ClaimContext", throwIfV1Schema: false)
        {
        }

        static ClaimRequestDbContext()
        {
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ClaimRequestDbContext>(new ClaimRequestDbInitializer());
        }

        public static ClaimRequestDbContext Create()
        {
            return new ClaimRequestDbContext();
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimInfor> ClaimInfors { get; set; }
        public DbSet<StaffProjectMap> StaffProjectMaps { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
            modelBuilder.Entity<Staff>().HasMany(s => s.StaffProjectMaps)
                .WithRequired(spm => spm.Staff)
                .HasForeignKey(spm => spm.StaffId);
            //project one - to - many staffprojectmap
            modelBuilder.Entity<Project>().HasMany(p => p.StaffProjectMaps)
                .WithRequired(spm => spm.Project)
                .HasForeignKey(spm => spm.ProjectId);
        }
    }
}