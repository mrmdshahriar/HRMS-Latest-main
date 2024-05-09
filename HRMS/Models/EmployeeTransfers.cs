using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class EmployeeTransfers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long TransferId { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationId { get; set; }
        public long NewDesignationId { get; set; }
        public long FromDepartmentId { get; set; }
        public long ToDepartmentId { get; set; }
        public string TrasnferReason { get; set; }
        public DateTime TrasnferDate { get; set; } = DateTime.Now;
        public long TrasnferedBy { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}