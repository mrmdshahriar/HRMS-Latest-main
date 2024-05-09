using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class EmployeeVisaDetail
    {
        public long Id { get; set; }
        public string VisaTitle { get; set; }
        public long? VisaNumber { get; set; }
        public DateTime? VisaIssuanceDate { get; set; }
        public DateTime? VisaExpiryDate { get; set; }
        public long? LabourCardNumber { get; set; }
        public string LabourCardCode { get; set; }
        public DateTime? LabourCardExpiry { get; set; }
        public long? InsuranceCardNumber { get; set; }
        public bool Active { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
}