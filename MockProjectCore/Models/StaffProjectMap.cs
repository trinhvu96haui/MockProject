using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimRequest.Core.Models
{
    public class StaffProjectMap
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Staff")]
        public string StaffId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public  Staff Staff { get; set; }
        public  Project Project { get; set; }
        [Required(ErrorMessage ="Role in project do not blank")]
        public int RoleInProject { get; set; }
    }
}
