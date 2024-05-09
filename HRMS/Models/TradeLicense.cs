    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

namespace HRMS.Models
{

    public class TradeLicense
    {
  
        public long Id { get; set; }
        public string Name { get; set; }

        public string MOLCode { get; set; }

        public DateTime? TradeLicenseExpiry { get; set; }

        public DateTime? MOLExpiry { get; set; }

        public int? VisaQuotaAvailability { get; set; }

        public bool Active { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public long LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; } = DateTime.Now;

        public bool? IsDeleted { get; set; }
    }
}
