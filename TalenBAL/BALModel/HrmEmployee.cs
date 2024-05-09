namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HrmEmployee()
        {
            Departments = new HashSet<Department>();
            HrmEmployees1 = new HashSet<HrmEmployee>();
            HrmInternalRequisitions = new HashSet<HrmInternalRequisition>();
            HrmInternalRequisitions1 = new HashSet<HrmInternalRequisition>();
            HrmInterviewDetails = new HashSet<HrmInterviewDetail>();
            HrmInterviews = new HashSet<HrmInterview>();
            HrmJobPosts = new HashSet<HrmJobPost>();
        }

        public int Id { get; set; }

        public int? TitleId { get; set; }

        public string FirstName { get; set; }

        public string Middlename { get; set; }

        public string LastName { get; set; }

        public string EmployeeCode { get; set; }

        public string Gender { get; set; }

        public string FatherHusbandName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string IdentityCardNo { get; set; }

        public DateTime? IdentityExpiryDate { get; set; }

        public int? ReligionId { get; set; }

        public string MaritalStatus { get; set; }

        public int? Dependants { get; set; }

        public int? NationalCountryId { get; set; }

        public int? EthnicityId { get; set; }

        public string BloodGroup { get; set; }

        public int? HrmLanguageId { get; set; }

        public int? DisabilitiesId { get; set; }

        public string EmployeeType { get; set; }

        public int? EmployeeGroupId { get; set; }

        public int? HrmLocationOficeId { get; set; }

        public int? DesignationId { get; set; }

        public int? ReportToId { get; set; }

        public int? DepartmentId { get; set; }

        public int? SubDepartmentId { get; set; }

        public int? HrmGradeId { get; set; }

        public string MachineId { get; set; }

        public DateTime? dteJoiningDate { get; set; }

        public int? ProbationPeriod { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public DateTime? ContractExpiryDate { get; set; }

        public double? BasicSalary { get; set; }

        public string ContacNo { get; set; }

        public string Email { get; set; }

        public int? LivingCountryId { get; set; }

        public int? StateId { get; set; }

        public int? CityId { get; set; }

        public string ZipCode { get; set; }

        public string CurrentAddress { get; set; }

        public int? CurrentCountryId { get; set; }

        public int? CurrentStateId { get; set; }

        public int? CurrentCityId { get; set; }

        public string CurrentZipCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool? IswebloginAllowed { get; set; }

        [StringLength(128)]
        public string SoftRoleinformation { get; set; }

        [StringLength(128)]
        public string ApplicationUserId { get; set; }

        public string ProfilePicture { get; set; }

        public int? CostCenterId { get; set; }

        public bool? Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public int? Country_Id { get; set; }

        [StringLength(50)]
        public string BiometricCode { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Passport { get; set; }

        [StringLength(50)]
        public string DrivingLicence { get; set; }

        public int? BirthCountryId { get; set; }

        [StringLength(50)]
        public string HrmLanguage { get; set; }

        [StringLength(50)]
        public string ProbationType { get; set; }

        [StringLength(50)]
        public string OfficialNo { get; set; }

        [StringLength(50)]
        public string OfficialEmail { get; set; }

        public int? RegionId { get; set; }

        [StringLength(50)]
        public string PermnantAddress { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }

        [StringLength(50)]
        public string BranchName { get; set; }

        [StringLength(50)]
        public string AccountTitle { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string AccountType { get; set; }
        public long? ShiftId { get; set; }
        public virtual ShiftMaster ShiftMaster { get; set; }
        public DateTime? PassportExpiryDate { get; set; }

        public virtual AspNetRole AspNetRole { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        public virtual Country Country2 { get; set; }

        public virtual Country Country3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual Designation Designation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmEmployee> HrmEmployees1 { get; set; }

        public virtual HrmEmployee HrmEmployee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInternalRequisition> HrmInternalRequisitions1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterviewDetail> HrmInterviewDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmInterview> HrmInterviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HrmJobPost> HrmJobPosts { get; set; }
    }
}
