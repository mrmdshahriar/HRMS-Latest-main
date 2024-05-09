namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployeeEducation
    {
        [Key]
        public long EducationId { get; set; }

        public long? EmployeeId { get; set; }

        public int? QualificationType { get; set; }

        [StringLength(50)]
        public string Degree { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(5)]
        public string Grade { get; set; }

        [StringLength(5)]
        public string Division { get; set; }

        [StringLength(5)]
        public string GPA { get; set; }

        [StringLength(5)]
        public string TotalGPA { get; set; }

        public decimal? Percentage { get; set; }

        [StringLength(10)]
        public string PassingYear { get; set; }

        [StringLength(100)]
        public string Institutes { get; set; }

        [StringLength(100)]
        public string Attachment { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
