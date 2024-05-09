using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class NotificationRequest
    {
    }
        public class Policies
        {
            [Key]
            public int? PolicyID { get; set; }
            public string PolicyName { get; set; }
            public DateTime? effectiveDatePolicy { get; set; }

            public string Policy { get; set; }
         }

        public class Procedures
        {
            [Key]
            public int? ProcID { get; set; }
            public string ProcedureName { get; set; }
            public DateTime? effectiveDateProcedure { get; set; }
            public string Procedure { get; set; }

        }
  
    public class SpecialConsidertion
    {
        [Key]
        public long SCID { get; set; }

        public long EmployeeId { get; set; }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Subject { get; set; }
        public string Request { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

    }

    public class NonObjectionCertificate
    {
        [Key]
        public long OCID { get; set; }
        public string Application { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Subject { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}