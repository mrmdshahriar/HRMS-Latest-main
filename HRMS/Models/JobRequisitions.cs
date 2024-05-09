using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class JobRequisitions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ReqId { get; set; }
        public string AddvertiseNo { get; set; }

        public long? JobId { get; set; }
        public int JobType { get; set; }
        public string JobTitle { get; set; }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public long ShiftId { get; set; }
        public decimal MinExpereince { get; set; }
        public decimal MaxExpereince { get; set; }
        public string MInQualification { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public decimal Age { get; set; }
        public string Skills { get; set; }
        public DateTime LastDate { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
        public string Attachment { get; set; }
        //public HttpPostedFile ImageFile { get; set; }
        public bool? Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } 
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}