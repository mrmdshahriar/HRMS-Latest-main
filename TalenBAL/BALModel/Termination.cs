namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Termination
    {
        [Key]
        public long TerminatinId { get; set; }

        public long? EmployeeId { get; set; }

        public long? DesignationId { get; set; }

        public long? DepartmentId { get; set; }

        public long? ToDepartmentId { get; set; }

        public int? Type { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        public DateTime? Date { get; set; }

        public long? TerminationBy { get; set; }

        public string Reason { get; set; }

        public bool? Active { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
