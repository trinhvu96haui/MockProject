using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Core.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ProjectId { get; set; }
        [StringLength(20,ErrorMessage ="Project code do not 20 character")]
        [Required(ErrorMessage ="Project code do not leave blank")]
        public string ProjectCode { get; set; }
        [Required(ErrorMessage = "Project name do not leave blank")]
        public string ProjectName { get; set; }
       // public DateTime Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [NotMapped]
        public string PM { get; set; }
        [NotMapped]
        public string QA { get; set; }
        [NotMapped]
        public string TechnicalLead { get; set; }
        [NotMapped]
        public string BA { get; set; }
        [NotMapped]
        public string Developers { get; set; }
        [NotMapped]
        public string Testers { get; set; }
        [NotMapped]
        public string TechnicalConsultancy { get; set; }
        public virtual IList<Claim> Claims { get; set; }
        public virtual ICollection<StaffProjectMap> StaffProjectMaps { get; set; }


    }
}
