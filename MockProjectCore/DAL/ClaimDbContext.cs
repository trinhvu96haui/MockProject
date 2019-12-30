using MockProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProjectCore.DAL
{
    public class ClaimDbContext : DbContext
    {
        public ClaimDbContext():base("name=ClaimContext")
        {
            Database.SetInitializer<ClaimDbContext>(new DBInitializer());
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet <Project> Projects { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimInfor> ClaimInfors { get; set; }
        public DbSet<StaffProjectMap> StaffProjectMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>().HasMany(cr => cr.ClaimInfors)
                .WithRequired(ci => ci.Claim)
                .HasForeignKey(ci => ci.ClaimId);
            // stafff one- to- many claim
            modelBuilder.Entity<Staff>().HasMany(s => s.Claims)
                .WithRequired(cr => cr.staff)
                .HasForeignKey(cr => cr.StaffId);
            // department one - to - many Staff
            modelBuilder.Entity<Department>().HasMany(d => d.Staffs)
                .WithRequired(s => s.Department)
                .HasForeignKey(s => s.DepartmentId);
            //staff one - to - many staffprojectmap
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
