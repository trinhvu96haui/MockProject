using ClaimRequest.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Core.Models
{
    public class Claim
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ClaimId { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "Total Working Hour is not valid")]
        public decimal TotalWorkingHour { get; set; }
        public string AuditTrail { get; set; }
        [ForeignKey("Staff")]
        public string StaffId { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<ClaimInfor> ClaimInfors { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
