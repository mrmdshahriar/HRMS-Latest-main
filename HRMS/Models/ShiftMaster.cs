using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    [Table("ShiftMasters")]
    public class ShiftMaster
    {
        [Key]
        public long ShiftId { get; set; }
        public string Code { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan GressTime { get; set; }
        public TimeSpan EarlyLeave { get; set; }
        public TimeSpan HalfDay { get; set; }
        public int? Breakhour { get; set; }
        public bool? IsFriday { get; set; }
        public bool? IsSaturday { get; set; }       
        public bool? IsSunday { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long CreatedBy { get; set; } 
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public long UpdatedBy { get; set; }
    }
}