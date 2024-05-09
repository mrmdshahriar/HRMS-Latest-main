namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployeeHealth
    {
        [Key]
        public long EmpHealthId { get; set; }

        public long? EmployeeId { get; set; }

        [StringLength(150)]
        public string DiseasName { get; set; }

        //[StringLength(150)]
        //public string Result { get; set; }

        //[StringLength(100)]
        //public string FromSuffering { get; set; }

        [StringLength(500)]
        public string Treatment { get; set; }

        [StringLength(100)]
        public string Attachment { get; set; }

        public bool? IsActive { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
