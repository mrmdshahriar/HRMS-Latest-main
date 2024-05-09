using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class OverTimeSetup
    {
        public long Id { get; set; }
        public string OverTimeType { get; set; }
        public decimal? OverTimeRate { get; set; }

        public bool? Active { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

    }
}