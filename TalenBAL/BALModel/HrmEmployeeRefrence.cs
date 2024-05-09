namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployeeRefrence
    {
        [Key]
        public long ReferenceId { get; set; }

        public long? EmployeeId { get; set; }

        public int? ReferenceType { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        public long? Designation { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        [StringLength(100)]
        public string ContactNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(50)]
        public string PeriodOfKowing { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
