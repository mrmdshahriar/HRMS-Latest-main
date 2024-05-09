using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProject.Models
{
    public class LeaveViewModel
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public long LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string DateFrom { get; set; }

        public string DateTo { get; set; }
        public int LeaveDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

     
    }
}