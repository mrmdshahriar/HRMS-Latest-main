using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HRMS.Models
{
    public partial class HRMS : DbContext
    {
        public HRMS()
            : base("name=Talenthrm")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<HrmEmployee> HrmEmployees { get; set; }
        public virtual DbSet<HrmInternalRequisition> HrmInternalRequisitions { get; set; }
        public virtual DbSet<HrmInterviewDetail> HrmInterviewDetails { get; set; }
        public virtual DbSet<HrmInterview> HrmInterviews { get; set; }
        public virtual DbSet<HrmJobPost> HrmJobPosts { get; set; }
        public virtual DbSet<HrmJobType> HrmJobTypes { get; set; }
        public virtual DbSet<HrmEmployeeEducations> HrmEmployeeEducations { get; set; }
        public virtual DbSet<HrmEmployeementHistories> HrmEmployeementHistories { get; set; }
        public virtual DbSet<HrmSkill> HrmSkills { get; set; }
        public virtual DbSet<HrmEmployeeHealths> HrmEmployeeHealths { get; set; }
        public virtual DbSet<HrmEmployeeRelations> HrmEmployeeRelations{ get; set; }
        public virtual DbSet<HrmEmployeeRefrences> HrmEmployeeRefrences { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Terminations> Terminations { get; set; }
        public virtual DbSet<EmployeeTransfers> EmployeeTransfers { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<JobRequisitions> JobRequisitions { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<AppliedCandidat> Applieds { get; set; }
        public virtual DbSet<OfferLetter> OfferLetters { get; set; }
        public virtual DbSet<InterviewAssessment> InterviewAssessments { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<ShiftMaster> ShiftMasters { get; set; }
        public virtual DbSet<AllowanceType> AllowanceTypes { get; set; }
        public virtual DbSet<Allowance> Allowances { get; set; }

        public virtual DbSet<AllowancesDeduction> AllowancesDeductions { get; set; }
        public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }
        public virtual DbSet<ApplyCandidate> ApplyCandidates { get; set; }
        public virtual DbSet<TradeLicense> TradeLicenses { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<CostingTab> CostingTabs { get; set; }

        public virtual DbSet<EmployeeVisaDetail> EmployeeVisaDetails { get; set; }
        public virtual DbSet<LoanSanction> LoanSanctions { get; set; }
        public virtual DbSet<Arrear> Arrears { get; set; }
        public virtual DbSet<OverTimeSetup> OverTimeSetups { get; set; }
        public virtual DbSet<EmployeeCost> EmployeeCosts { get; set; }
        public virtual DbSet<BonusSetup> BonusSetups { get; set; }

        public virtual DbSet<SalarySetup> SalarySetups { get; set; }
        public virtual DbSet<LeavePolicy> LeavePolicies { get; set; }
        public virtual DbSet<PayRollCutOff> PayRollCutOffs { get; set; }
        public virtual DbSet<SpecialAllowance> SpecialAllowances { get; set; }
        public virtual DbSet<AnnualLeaveSalaryCalculation> AnnualLeaveSalaryCalculations { get; set; }

        public virtual DbSet<KeyObjective> KeyObjectives { get; set; }
        public virtual DbSet<KeyObjResultViewModel> KeyObjResultViewModels { get; set; }
        public virtual DbSet<KeyResult> KeyResults { get; set; }
        public virtual DbSet<ManagerKeyResult> ManagerKeyResults { get; set; }
        public virtual DbSet<AdvanceSalary> AdvaceSalarys { get; set; }
        public virtual DbSet<SalaryCalculationHeader> SalaryCalculationHeaders { get; set; }
        public virtual DbSet<SpecialConsidertion> SpecialConsidertions { get; set; }

        public virtual DbSet<NonObjectionCertificate> NonObjectionCertificates { get; set; }

        public virtual DbSet<Policies> Policies { get; set; }

        public virtual DbSet<Procedures> Procedures { get; set; }
        public virtual DbSet<tbl_EmployeeAttendanceCalculations> tbl_EmployeeAttendanceCalculations { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.HrmEmployees)
                .WithOptional(e => e.AspNetRole)
                .HasForeignKey(e => e.SoftRoleinformation);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.HrmEmployees)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.ApplicationUserId);

            modelBuilder.Entity<City>()
                .HasMany(e => e.HrmEmployees)
                .WithOptional(e => e.City)
                .HasForeignKey(e => e.CityId);

            modelBuilder.Entity<City>()
                .HasMany(e => e.HrmEmployees1)
                .WithOptional(e => e.City1)
                .HasForeignKey(e => e.CurrentCityId);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.HrmEmployees)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.Country_Id);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.HrmEmployees1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.CurrentCountryId);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.HrmEmployees2)
                .WithOptional(e => e.Country2)
                .HasForeignKey(e => e.LivingCountryId);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.HrmEmployees3)
                .WithOptional(e => e.Country3)
                .HasForeignKey(e => e.NationalCountryId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Departments1)
                .WithOptional(e => e.Department1)
                .HasForeignKey(e => e.ParentDepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmEmployees)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmEmployees1)
                .WithOptional(e => e.Department1)
                .HasForeignKey(e => e.SubDepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmInternalRequisitions)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmInternalRequisitions1)
                .WithOptional(e => e.Department1)
                .HasForeignKey(e => e.SubDepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmJobPosts)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.Department_Id);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmJobPosts1)
                .WithOptional(e => e.Department1)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.HrmJobPosts2)
                .WithOptional(e => e.Department2)
                .HasForeignKey(e => e.SubDepartmentId);

            modelBuilder.Entity<Designation>()
                .HasMany(e => e.HrmInternalRequisitions)
                .WithOptional(e => e.Designation)
                .HasForeignKey(e => e.DesignationId);

            modelBuilder.Entity<Designation>()
                .HasMany(e => e.HrmInternalRequisitions1)
                .WithOptional(e => e.Designation1)
                .HasForeignKey(e => e.ReplacePositionId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.Departments)
                .WithOptional(e => e.HrmEmployee)
                .HasForeignKey(e => e.HrmDepartmentHeadId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmEmployees1)
                .WithOptional(e => e.HrmEmployee1)
                .HasForeignKey(e => e.ReportToId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmInternalRequisitions)
                .WithOptional(e => e.HrmEmployee)
                .HasForeignKey(e => e.HrmEmployeeId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmInternalRequisitions1)
                .WithOptional(e => e.HrmEmployee1)
                .HasForeignKey(e => e.RepalceToId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmInterviews)
                .WithOptional(e => e.HrmEmployee)
                .HasForeignKey(e => e.HrmEmployee_Id);

            modelBuilder.Entity<HrmInterview>()
                .HasMany(e => e.HrmInterviewDetails)
                .WithRequired(e => e.HrmInterview)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HrmJobType>()
                .HasMany(e => e.HrmJobPosts)
                .WithOptional(e => e.HrmJobType1)
                .HasForeignKey(e => e.HrmJobType_Id);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Countries)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);
        }
    }

    
}
