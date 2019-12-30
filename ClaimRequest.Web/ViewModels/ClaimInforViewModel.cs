using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mock.Models.ViewModel
{
    public class ClaimInforViewModel
    {
        public string ClaimInfoId { get; set; }
        [Required(ErrorMessage = "Please specify value for this ClaimInformation Date.")]
        public DateTime? ClaimInfoDate { get; set; }
        [Required(ErrorMessage = "Please specify value for this From time.")]
        public DateTime ClaimInfoFrom { get; set; }
        [Required(ErrorMessage = "Please specify value for To time.")]
        public DateTime ClaimInfoTo { get; set; }
        [Required(ErrorMessage = "Please specify value for Total Hour.")]
        public decimal TotalHour { get; set; }
        public string Remark { get; set; }
    }
}