namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JobRequisition
    {
        [Key]
        public long ReqId { get; set; }

        [StringLength(100)]
        public string AddvertiseNo { get; set; }

        public long? JobId { get; set; }

        //public int? JobType { get; set; }

        //[StringLength(50)]
        //public string JobTitle { get; set; }

        //public long? DesignationId { get; set; }

        //public long? DepartmentId { get; set; }

        //public long? ShiftId { get; set; }

        //public decimal? MinExpereince { get; set; }

        //public decimal? MaxExpereince { get; set; }

        //[StringLength(150)]
        //public string MInQualification { get; set; }

        //[StringLength(150)]
        //public string Location { get; set; }

        //[StringLength(50)]
        //public string Gender { get; set; }

        //public decimal? Age { get; set; }

        //[StringLength(500)]
        //public string Skills { get; set; }

        public DateTime? LastDate { get; set; }

        //public decimal? ExpectedSalary { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string Attachment { get; set; }

        public bool? Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
