using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class EmployeeCost
    {
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public long? CostingTabsId { get; set; }
        public decimal? TotalCost { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
       
    }
}