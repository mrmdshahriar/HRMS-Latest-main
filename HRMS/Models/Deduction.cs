using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Deduction
    {
      public long Id { get; set; }
      public long? EmployeeId { get; set; }
      public decimal? Amount { get; set; }
      public string Reason { get; set; }
      public int? DeductionMode { get; set; }
      public bool? Active { get; set; }
      public int? CreatedBy { get; set; }
      public DateTime? CreatedOn { get; set; }
      public int? LastModifiedBy { get; set; }
      public DateTime? LastModifiedOn { get; set; }
      public bool? IsDeleted { get; set; }
      public DateTime? DeductionMonth { get; set; }

    }
}