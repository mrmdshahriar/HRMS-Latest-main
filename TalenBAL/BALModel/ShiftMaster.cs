namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShiftMaster
    {
        [Key]
        public long ShiftId { get; set; }

        public string ShiftName { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public TimeSpan? GressTime { get; set; }

        public TimeSpan? EarlyLeave { get; set; }

        public TimeSpan? HalfDay { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public long? UpdatedBy { get; set; }
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }
        public bool? IsFriday { get; set; }
        public bool? IsSaturday { get; set; }
        public bool? IsSunday { get; set; }
    }
}
