using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class AllowancesDeduction
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public long? FK_AllowanceId { get; set; }

        public decimal? Percentage { get; set; }
        public decimal? Amount { get; set; }
        public int GLCode { get; set; }
        public string Effect { get; set; }
        public bool? IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DeductionMonth { get; set; }

    }
}