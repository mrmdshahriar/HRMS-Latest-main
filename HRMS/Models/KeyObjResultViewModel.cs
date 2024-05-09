using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class KeyObjResultViewModel
    {
        public long Id { get; set; }
        public string DefineKeyResult { get; set; }
        public long? AssignedTo { get; set; }
        public long? KeyObjectiveId { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int? AssignedPercentage { get; set; }
        public int? CompletedPercentage { get; set; }
        public string FirstName { get; set; }

        public string DefineObjective { get; set; }
        public string DepartmentName { get; set; }
    }
}