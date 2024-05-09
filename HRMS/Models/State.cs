using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class State
    {
        public State()
        {
            //HrmEmployees = new HashSet<HrmEmployee>();
            //HrmEmployees1 = new HashSet<HrmEmployee>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int? CountryId { get; set; }

        public bool? Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual Country Country { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HrmEmployee> HrmEmployees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }
    }
}