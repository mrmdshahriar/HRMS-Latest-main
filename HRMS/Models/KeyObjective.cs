using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class KeyObjective
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KeyObjective()
        {
            this.KeyResults = new HashSet<KeyResult>();
        }

        public long Id { get; set; }
        public string DefineObjective { get; set; }
        public Nullable<long> DepId { get; set; }
        public Nullable<long> AssignedTo { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public Nullable<int> AssignedPercentage { get; set; }
        public Nullable<int> CompletedPercentage { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<long> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public Nullable<bool> Deleted { get; set; }

        //////public virtual KeyObjective KeyObjectives1 { get; set; }
        //////public virtual KeyObjective KeyObjective1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KeyResult> KeyResults { get; set; }


    }
}