using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class SpecialAllowance
    {
        public long Id { get; set; }

        public long? EmployeeId { get; set; }

        public long? AllowanceType { get; set; }

        public decimal? Amount { get; set; }

        public string PayRollMonth { get; set; }

        public string Effect { get; set; }

        public bool? Active { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? Deleted { get; set; }
    }
}