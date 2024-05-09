using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class OfferLetter
    {

        [Key]
        public int PK_OfferLetterId { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationId { get; set; }
        public long DepartmentId { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }  

    }
}
