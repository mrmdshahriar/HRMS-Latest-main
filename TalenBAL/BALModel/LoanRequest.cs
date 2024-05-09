namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoanRequest
    {
        
      
        public long Id { get; set; }
        public long? EmployeeId { get; set; }
        public decimal? Amount { get; set; }
        public int? Duration { get; set; }
        public DateTime? RequestDate { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }


    }
}
