namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployeeRelation
    {
        [Key]
        public long RelationId { get; set; }

        public long? EmployeeId { get; set; }

        public int? RelationType { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string ContactNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
