using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Core.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
