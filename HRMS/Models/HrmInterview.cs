namespace HRMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmInterview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmInterview()
        {
            HrmInterviewDetails = new HashSet<HrmInterviewDetail>();
        }

        public int Id { get; set; }

        public int? HrmApplyFormId { get; set; }

        public int? HrmJobPostId { get; set; }

        public string Result { get; set; }

        public int TotalScore { get; set; }

        public int TotalEarned { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public int? HrmEmployee_Id { get; set; }

        public virtual HrmEmployee HrmEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterviewDetail> HrmInterviewDetails { get; set; }

        public virtual HrmJobPost HrmJobPost { get; set; }
    }
}
