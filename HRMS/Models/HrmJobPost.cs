namespace HRMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmJobPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmJobPost()
        {
            HrmInterviews = new HashSet<HrmInterview>();
        }

        public int Id { get; set; }

        public int? HrmInternalRequisitionId { get; set; }

        public string JobPostCode { get; set; }

        public string JobTitle { get; set; }

        public string HrmJobType { get; set; }

        public decimal? DurationMonths { get; set; }

        public int? HrmShiftId { get; set; }

        public bool ShiftRotation { get; set; }

        public int? DesignationId { get; set; }

        public int? HrmEmployeeId { get; set; }

        public int? DepartmentId { get; set; }

        public int? SubDepartmentId { get; set; }

        public int? HrmLocationId { get; set; }

        public int TotalPosition { get; set; }

        public DateTime? ClosingDate { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public double ExperienceYears { get; set; }

        public double ExperienceMonths { get; set; }

        public double? MinSalaryRange { get; set; }

        public double? MaxSalaryRange { get; set; }

        public int? DegreeId { get; set; }

        public string JoDescription { get; set; }

        public bool EmailNotification { get; set; }

        public bool AutoResponse { get; set; }

        public string Status { get; set; }

        public bool Active { get; set; }

        public DateTime? EndedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public int? Department_Id { get; set; }

        public int? HrmJobType_Id { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual Department Department2 { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual HrmEmployee HrmEmployee { get; set; }

        public virtual HrmInternalRequisition HrmInternalRequisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterview> HrmInterviews { get; set; }

        public virtual HrmJobType HrmJobType1 { get; set; }
    }
}
