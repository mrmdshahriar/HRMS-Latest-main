using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    [Table("AdvanceSalarys")]
    public class AdvanceSalary
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public decimal? Amount { get; set; }
        public long? PayoutMonth { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}