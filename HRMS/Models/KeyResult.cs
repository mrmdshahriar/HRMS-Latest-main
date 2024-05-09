using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class KeyResult
    {
        public long Id { get; set; }
        public string DefineKeyResult { get; set; }
        public Nullable<long> KeyObjectiveId { get; set; }
        public Nullable<int> AssignedTo { get; set; }
        public Nullable<System.DateTime> AssignDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public Nullable<int> AssignedPercentage { get; set; }
        public Nullable<int> CompletedPercentage { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<long> PendingDays { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual HrmEmployee HrmEmployee { get; set; }
        public virtual KeyObjective KeyObjective { get; set; }
    }
}