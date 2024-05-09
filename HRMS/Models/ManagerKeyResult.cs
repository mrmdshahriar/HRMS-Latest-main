using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class ManagerKeyResult
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public long? GrossSalary { get; set; }
        public bool? FitForPromotion { get; set; }
        public bool? FitInCurrentPossition { get; set; }
        public bool? NotFitForPromotionButLikelyToBecome { get; set; }
        public bool? UnfitForPromotionHasReachedHisCeiling { get; set; }
        public bool? EligibleForBonus { get; set; }
        public int? EligibleForIncrementPercentage { get; set; }
        public bool? Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}