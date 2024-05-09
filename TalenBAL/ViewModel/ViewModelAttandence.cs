using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.ViewModel
{
    public class ViewModelAttandence
    {
        public bool IsHalfDay { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarly { get; set; }
        public bool Isleave { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsPresent { get; set; }
        public int employeeId { get; set; }
        public bool IsHoliday { get; set; }

    }
}
