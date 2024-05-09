using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class HrmEmployeementHistories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long EmpExpId { get; set; }
        public long EmployeeId { get; set; }
        public int JobType { get; set; }
        public string Organization { get; set; }
        public string OrganizationAddress { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string ReasonForLeaaving { get; set; }
        public decimal LastSalary { get; set; }
        public string Currency { get; set; }
        public string JobDescription { get; set; }
        public string Attachment { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}