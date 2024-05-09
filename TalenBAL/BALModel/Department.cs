namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Departments1 = new HashSet<Department>();
            HrmEmployees = new HashSet<HrmEmployee>();
            HrmEmployees1 = new HashSet<HrmEmployee>();
            HrmInternalRequisitions = new HashSet<HrmInternalRequisition>();
            HrmInternalRequisitions1 = new HashSet<HrmInternalRequisition>();
            HrmJobPosts = new HashSet<HrmJobPost>();
            HrmJobPosts1 = new HashSet<HrmJobPost>();
            HrmJobPosts2 = new HashSet<HrmJobPost>();
        }

        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public int? ParentDepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string Name { get; set; }

        public int? HrmDepartmentHeadId { get; set; }

        public int? ReferenceId { get; set; }

        public bool? Active { get; set; }

        public bool? IsSubDepartment { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments1 { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual HrmEmployee HrmEmployee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts2 { get; set; }
    }
}
