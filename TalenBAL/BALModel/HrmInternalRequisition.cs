namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmInternalRequisition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmInternalRequisition()
        {
            HrmJobPosts = new HashSet<HrmJobPost>();
        }

        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public string RequisitionNumber { get; set; }

        public int? DepartmentId { get; set; }

        public int? SubDepartmentId { get; set; }

        public int? DesignationId { get; set; }

        public int? HrmGradeId { get; set; }

        public int? ContractDuration { get; set; }

        public int? RequestedBy { get; set; }

        public string PositionStatus { get; set; }

        public string Description { get; set; }

        public double? Amount { get; set; }

        public int? ReplacePositionId { get; set; }

        public int? RepalceToId { get; set; }

        public int? RepalceToGradeId { get; set; }

        public int? NoOfPositions { get; set; }

        public int? Qualification { get; set; }

        public int? MinExperience { get; set; }

        public bool? ShiftRotation { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Designation Designation1 { get; set; }

        public virtual HrmEmployee HrmEmployee { get; set; }

        public virtual HrmEmployee HrmEmployee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts { get; set; }
    }
}
