using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class HrmEmployeeHealths
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long EmpHealthId { get; set; }
        public long EmployeeId { get; set; }
        public string DiseasName { get; set; }
        //public string Result { get; set; }
        //public string FromSuffering { get; set; }
        public string Treatment { get; set; }
        public string Attachment { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}