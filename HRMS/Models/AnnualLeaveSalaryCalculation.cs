using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class AnnualLeaveSalaryCalculation
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public bool? SelectEmp { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public decimal? Tenure { get; set; }
        public decimal? GrossMonthlySalary { get; set; }
        public int? NoOfPendingAnnualLeaves { get; set; }
        public decimal? LeaveSalary { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime ? CashOutMonth { get; set; }
        public string  Status { get; set; }

    }
}