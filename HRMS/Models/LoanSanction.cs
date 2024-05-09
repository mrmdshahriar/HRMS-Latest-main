using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class LoanSanction
    {
        public long Id { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? LoanAmount { get; set; }
        public DateTime? DateIssued { get; set; }
        public int? TentativeReturnMonth { get; set; }
        public decimal? EmiCalculation { get; set; }
        public DateTime? LoanDeductionStartDate{get;set;}
        //public DateTime? LoanDefermentDate { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
}