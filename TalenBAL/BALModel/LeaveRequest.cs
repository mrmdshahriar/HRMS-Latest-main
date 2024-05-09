namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeaveRequest
    {
        [Key]
        public int Pk_LeaveRequest { get; set; }

        public long? Employee { get; set; }

        public long? LeaveTypeId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? LeaveDays { get; set; }

        public string Reason { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
