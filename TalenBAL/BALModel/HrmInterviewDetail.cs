namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HrmInterviewDetail
    {
        public int Id { get; set; }

        public int? HrmInterviewId { get; set; }

        public int? HrmEmployeeId { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public string Venue { get; set; }

        public string Description { get; set; }

        public int? Score { get; set; }

        public int? EarnedScore { get; set; }

        public string Remarks { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual HrmEmployee HrmEmployee { get; set; }

        public virtual HrmInterview HrmInterview { get; set; }
    }
}
