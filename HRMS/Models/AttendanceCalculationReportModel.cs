using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class AttendanceCalculationReportModel
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string PresentDays { get; set; }
        public string LeaveDays { get; set; }
        public string AbsentDays { get; set; }
        public string TotalHoursWorked { get; set; }
        public string NormalOTHours { get; set; }
        public string WeekendOTHours { get; set; }
        public string PublicHolidaysOTHours { get; set; }
    }
}