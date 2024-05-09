using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class HrmEmployeeEducations
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long EducationId { get; set; }
        public long EmployeeId { get; set; }
        public int QualificationType { get; set; }
        public string Degree { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public string Division { get; set; }
        public string GPA { get; set; }
        public string TotalGPA { get; set; }
        public decimal Percentage { get; set; }
        public string PassingYear { get; set; }
        public string Institutes { get; set; }
        public string Attachment { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}