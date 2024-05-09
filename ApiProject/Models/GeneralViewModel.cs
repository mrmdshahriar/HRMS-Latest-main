using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.BALModel
{
    public class GeneralViewModel
    {
        public long UserId { get; set; }

        public long? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public long? UserTypeId { get; set; }

        public string UserTypeName { get; set; }

        public DateTime CNICExpiry { get; set; }
        public DateTime PassportExpiry { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        public string LoginId { get; set; }

        public string Password { get; set; }

        //public bool? IsActive { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime? CreatedOn { get; set; }

        //public string LastModifiedBy { get; set; }

        //public DateTime? LastModifiedOn { get; set; }

        //public bool? IsDeleted { get; set; }
    }
}
