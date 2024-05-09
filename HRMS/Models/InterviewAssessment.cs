using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class InterviewAssessment
    {
        [Key]
        public int PK_InterviewId { get; set; }
        public long AppliedId { get; set; }
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
    }
}