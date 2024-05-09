using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Pk_LeaveRequest { get; set; }
        public long Employee { get; set; }
        public long LeaveTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int LeaveDays { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string Status { get; set; }
        public int? EligibilityDays { get; set; }
        public int? PendingDays { get; set; }
        public int? ExtraDays { get; set; }
        public DateTime? RejoiningDate { get; set; }
        public bool? Encashment { get; set; }
        public DateTime? CashOutDate { get; set; }
    }

}

