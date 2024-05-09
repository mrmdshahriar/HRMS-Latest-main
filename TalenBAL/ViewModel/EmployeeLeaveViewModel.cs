using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.ViewModel
{
    public class EmployeeLeaveViewModel
    {
        public bool IsHoliday { get; set; }
        public int Holiday { get; set; }
        public bool IsLeave { get; set; }
        public int LeaveType { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarly { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsPresent { get; set; }
        public bool? IsFriday { get; set; }
        public bool? IsSaturday { get; set; }
        public bool? IsSunday { get; set; }
    }
}
