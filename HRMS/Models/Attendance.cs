using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool IsPresent { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsLeave { get; set; }
        public string LeaveType { get; set; }
        public bool IsHoliday { get; set; }
        public string Holiday { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarly { get; set; }
        public long? Department { get; set; }
        public int? Employee { get; set; }
        public string Month { get; set; }
    }
}