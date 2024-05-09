using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class EmployeeView
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public long DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}