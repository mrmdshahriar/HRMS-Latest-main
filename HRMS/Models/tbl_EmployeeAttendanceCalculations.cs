using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class tbl_EmployeeAttendanceCalculations
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int? PresentDays { get; set; }
        public int? LeaveDays { get; set; }
        public int? AbsentDays { get; set; }
        public string TotalHoursWorked { get; set; }
        public int? NormalOTHours { get; set; }
        public int? WeekendOTHours { get; set; }
        public int? PublicHolidaysOTHours { get; set; }
         public DateTime? AttendanceMonth { get; set; }

    }
}