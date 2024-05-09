using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.BALModel
{
    public class EmplouyeeView
    {
        public int Id { get; set; }

        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }      
        public DateTime DateofJoining { get; set; }

        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }      
  
        public string ContacNo { get; set; }

        public string Email { get; set; }

        public string CurrentAddress { get; set; }

        public string CurrentZipCode { get; set; }

        public DateTime? IdentityExpiryDate { get; set; }

        public DateTime? PassportExpiryDate { get; set; }

    }
}
