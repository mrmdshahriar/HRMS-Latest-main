namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployeementHistory
    {
        [Key]
        public long EmpExpId { get; set; }

        public long? EmployeeId { get; set; }

        public int? JobType { get; set; }

        [StringLength(150)]
        public string Organization { get; set; }

        [StringLength(500)]
        public string OrganizationAddress { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(500)]
        public string ReasonForLeaaving { get; set; }

        public decimal? LastSalary { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        [StringLength(500)]
        public string JobDescription { get; set; }

        [StringLength(100)]
        public string Attachment { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
