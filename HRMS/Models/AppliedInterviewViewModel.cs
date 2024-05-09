using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class AppliedInterviewViewModel
    {
        //interview model
        //public long AppliedId { get; set; }
        public string Education { get; set; }
        public string ComputerLiteracy { get; set; }
        public string Intelligence { get; set; }
        public string ExperienceInterviewed { get; set; }
        public string ExperienceCompanyBusiness { get; set; }
        public string JobKnowledgeSkill { get; set; }
        public string Personality { get; set; }
        public string CommunicationSkills { get; set; }
        public string DevelopmentMotivation { get; set; }
        public string PersonalAptitude { get; set; }
        public string Comments { get; set; }

        //applied model

        [Key]
        public long AppliedId
        {
            get;
            set;
        }
        public string CandidateName
        {
            get;
            set;
        }
        public string FatherName
        {
            get;
            set;
        }
        public string CNIC
        {
            get;
            set;
        }

        public string ContactNumber
        {
            get;
            set;
        }
        public string Email
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
        public long ShiftId
        {
            get;
            set;
        }
        public int? DepartmentId
        {
            get;
            set;
        }
        public long? DesignationId
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
        public string AddvertiseNo
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
        public string Skills
        {
            get;
            set;
        }
        public decimal? ExpectedSalary
        {
            get;
            set;
        }
        public string AppliedFrom
        {
            get;
            set;
        }
        public decimal? Age
        {
            get;
            set;
        }
        public int? Currency
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public string Photo
        {
            get;
            set;
        }

        public string Attachment
        {
            get;
            set;
        }
        public DateTime? ApplyDate
        {
            get;
            set;
        }

        public DateTime? AvailableDate
        {
            get;
            set;
        }

        public bool? Active
        {
            get;
            set;
        }

        public DateTime? LastModifiedOn
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
    }
}