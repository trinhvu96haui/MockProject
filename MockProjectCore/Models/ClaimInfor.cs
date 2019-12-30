using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Core.Models
{
    public class ClaimInfor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ClaimInforId { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string Day { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal TotalNoOfHours { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("Claim")]
        public Guid ClaimId { get; set; }      
        public virtual Claim Claim { get; set; }
    }
}
