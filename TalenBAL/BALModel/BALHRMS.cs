using HRMS.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TalenBAL.BALModel
{
    public partial class BALHRMS : DbContext
    {
        //public BALHRMS()
        //   //: base("name=BALHRMS")
        //     : base("data source=DESKTOP-17KLQ1B\\SQLEXPRESS;initial catalog=HRMS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework providerName=System.Data.SqlClient")
        //{
        //}
  
        //static BALHRMS()
        //{
        //    Database.SetInitializer<BALHRMS>(null);
        //}


        //public BALHRMS()
        //: base("data source=.;initial catalog=Talenthrm;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework providerName = System.Data.SqlClient")

        //: base("data source=sql.bsite.net\\MSSQL2016;initial catalog=mrmdshah_Talenthrm;User ID=mrmdshah_Talenthrm;Password=123456789;")


        //: base("workstation id=Talentdb.mssql.somee.com;packet size=4096;user id=ghazihur_SQLLogin_1;pwd=oq4f22l1hb;data source=Talentdb.mssql.somee.com;persist security info=False;initial catalog=Talentdb")
        //: base("Data Source=167.235.228.209;Initial Catalog=Talenthrm;Persist Security Info=True;User ID=sa;Password=Talenthrm123")

        public BALHRMS() : base("name=Talenthrm")
        {
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public virtual DbSet<EmployeeTransfer> EmployeeTransfers { get; set; }
        public virtual DbSet<HrmEmployeeEducation> HrmEmployeeEducations { get; set; }
        public virtual DbSet<HrmEmployeeHealth> HrmEmployeeHealths { get; set; }
        public virtual DbSet<HrmEmployeementHistory> HrmEmployeementHistories { get; set; }
        public virtual DbSet<HrmEmployeeRefrence> HrmEmployeeRefrences { get; set; }
        public virtual DbSet<HrmEmployeeRelation> HrmEmployeeRelations { get; set; }
        public virtual DbSet<HrmEmployee> HrmEmployees { get; set; }
        public virtual DbSet<HrmInternalRequisition> HrmInternalRequisitions { get; set; }
        public virtual DbSet<HrmInterviewDetail> HrmInterviewDetails { get; set; }
        public virtual DbSet<HrmInterview> HrmInterviews { get; set; }
        public virtual DbSet<HrmJobPost> HrmJobPosts { get; set; }
        public virtual DbSet<HrmJobType> HrmJobTypes { get; set; }
        public virtual DbSet<HrmSkill> HrmSkills { get; set; }
        public virtual DbSet<JobRequisition> JobRequisitions { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ShiftMaster> ShiftMasters { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Termination> Terminations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Allowance> Allowances { get; set; }
        public virtual DbSet<AllowancesDeduction> AllowancesDeductions { get; set; }
        public virtual DbSet<AllowanceType> AllowanceTypes { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }
        public virtual DbSet<ApplyCandidate> ApplyCandidates { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<SalaryRequest> SalaryRequests { get; set; }

        public virtual DbSet<LoanRequest> LoanRequests { get; set; }

        public virtual DbSet<ExpnesesRequest> ExpnesesRequests { get; set; }

        public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }
       



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
                .HasForeignKey(e => e.RequestedBy);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmInternalRequisitions1)
                .WithOptional(e => e.HrmEmployee1)
                .HasForeignKey(e => e.RepalceToId);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmInterviews)
                .WithOptional(e => e.HrmEmployee)
                .HasForeignKey(e => e.HrmEmployee_Id);

            modelBuilder.Entity<HrmEmployee>()
                .HasMany(e => e.HrmJobPosts)
                .WithOptional(e => e.HrmEmployee)
                .HasForeignKey(e => e.Req);

            modelBuilder.Entity<HrmJobType>()
                .HasMany(e => e.HrmJobPosts)
                .WithOptional(e => e.HrmJobType1)
                .HasForeignKey(e => e.HrmJobType_Id);

            modelBuilder.Entity<AllowancesDeduction>()
                .Property(e => e.Percentage)
                .HasPrecision(18, 0);

            modelBuilder.Entity<AllowancesDeduction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);
        }
    }
}
