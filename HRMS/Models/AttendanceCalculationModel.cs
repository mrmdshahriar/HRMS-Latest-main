using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class AttendanceCalculationModel
    {
        public string ReportType { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}