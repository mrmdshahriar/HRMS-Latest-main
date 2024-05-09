namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PublicHoliday
    {
        [Key]
        [Column(Order = 0)]
        public int Pk_PublicId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime DateFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateTo { get; set; }

        public int? LeaveDays { get; set; }

        [StringLength(100)]
        public string Color { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
