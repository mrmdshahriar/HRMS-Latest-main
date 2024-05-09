using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class LoanType
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public bool? Active { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}