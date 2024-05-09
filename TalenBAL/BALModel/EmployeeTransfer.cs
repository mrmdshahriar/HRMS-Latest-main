namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeTransfer
    {
        [Key]
        public long TransferId { get; set; }

        public long? EmployeeId { get; set; }

        public long? DesignationId { get; set; }

        public long? NewDesignationId { get; set; }

        public long? FromDepartmentId { get; set; }

        public long? ToDepartmentId { get; set; }

        [StringLength(500)]
        public string TrasnferReason { get; set; }

        public DateTime? TrasnferDate { get; set; }

        public long? TrasnferedBy { get; set; }

        public bool? Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
