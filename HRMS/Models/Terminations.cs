using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Terminations
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long TerminatinId { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public long ToDepartmentId { get; set; }
        public int Type { get; set; }
        public DateTime LastWorkingDate { get; set; } = new DateTime();
        public DateTime Date { get; set; } = new DateTime();
        public long TerminationBy { get; set; }
        public string Reason { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }=DateTime.Now;
        public bool IsDeleted { get; set; }      
    }
}