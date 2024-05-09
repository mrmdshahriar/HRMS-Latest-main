using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string LoginId { get; set; }

        public string Password { get; set; }

        public long? UserTypeId { get; set; }
        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}