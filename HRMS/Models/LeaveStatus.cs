using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    //[Table("LeaveStatuses")]
    public class LeaveStatus
    {
        [Key]
        public int Pk_LeaveStatus { get; set; }
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
        public bool IsDeleted { get; set; }
        public string Status { get; set; }

    }
}