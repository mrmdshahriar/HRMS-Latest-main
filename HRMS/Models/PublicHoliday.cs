using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    [Table("PublicHolidays")]
    public class PublicHoliday
    {
        [Key]
        public int Pk_PublicId { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int LeaveDays { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}