    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace HRMS.Models
{

    public partial class HrmSkill
    {
  
        public int Id { get; set; }
        public long? EmployeeId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; } = DateTime.Now;

        public bool? IsDeleted { get; set; }
    }
}
