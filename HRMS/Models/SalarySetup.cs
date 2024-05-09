using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class SalarySetup
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public string Allowances { get; set; }
        public string AllowanceId { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool? OverTime { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? SalaryDate { get; set; }
        public int? AllonDeductId { get; set; }

    }
}