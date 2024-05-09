namespace HRMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organization
    {
        public int Id { get; set; }

        public int? GroupOfCompaniesID { get; set; }

        public string Name { get; set; }

        public string ShortCode { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string BaseCurrency { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
