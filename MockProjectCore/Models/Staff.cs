using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClaimRequest.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Staff : IdentityUser
    {       
        public string StaffName { get; set; }
        [Required(ErrorMessage = "Job Rank do not leave blank")]
        public int JobRank { get; set; }
        [Required(ErrorMessage = "Salary do not leave blank")]
        public decimal Salary { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }       
        public virtual Department Department { get; set; }
        public  ICollection<StaffProjectMap> StaffProjectMaps { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Staff> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}