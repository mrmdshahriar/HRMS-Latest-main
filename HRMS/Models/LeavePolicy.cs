using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class LeavePolicy
    {
        public long Id { get; set; }
        public long? LeaveTypeId { get; set; }
        public int? LeaveDays { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEncashable { get; set; }
    }
}