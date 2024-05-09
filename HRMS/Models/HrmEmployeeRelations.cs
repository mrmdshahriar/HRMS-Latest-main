using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class HrmEmployeeRelations
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long RelationId { get; set; }
        public long EmployeeId { get; set; }
        public int RelationType { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}