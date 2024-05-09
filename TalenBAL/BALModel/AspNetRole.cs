namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetRole()
        {
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            HrmEmployees = new HashSet<HrmEmployee>();
        }

        public string Id { get; set; }

        public int? RoleCategoryId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }
    }
}
