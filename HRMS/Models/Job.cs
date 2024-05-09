using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class Job
    {
        public Job()
        {
            //Department = new HashSet<Department>();
            //HrmJobType = new HashSet<HrmJobType>();
            //Designation = new HashSet<Designation>();
            //HrmSkill = new HashSet<HrmSkill>();
        }

        public int JobId
        {
            get;
            set;
        }
        public string JobTitle
        {
            get;
            set;
        }
        public int? JobType
        {
            get;
            set;
        }
        public long? ShiftId
        {
            get;
            set;
        }
        public int? DepartmentId
        {
            get;
            set;
        }
        public int? DesignationId
        {
            get;
            set;
        }
        public decimal? MinExpereince
        {
            get;
            set;
        }
        public Nullable<decimal> MaxExpereince
        {
            get;
            set;
        }

        public string MInQualification
        {
            get;
            set;
        }
        public int? NoOfposition
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public DateTime? LastModifiedOn
        {
            get;
            set;
        }
        public DateTime? ClosingDate
        {
            get;
            set;
        }
        public long Skills
        {
            get;
            set;
        }

        public string ExtraSkills
        {
            get;
            set;
        }
        public string SalaryRange
        {
            get;
            set;
        }
        public string jobDescription
        {
            get;
            set;
        }
        public int? Age
        {
            get;
            set;
        }

        public bool? Active
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime? CreatedOn
        {
            get;
            set;
        }

        public string LastModifiedBy
        {
            get;
            set;
        }

        public bool? IsDeleted
        {
            get;
            set;
        }
     
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HrmJobType> HrmJobType { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Department> Department { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Designation> Designation { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HrmSkill> HrmSkill { get; set; }
    }
}