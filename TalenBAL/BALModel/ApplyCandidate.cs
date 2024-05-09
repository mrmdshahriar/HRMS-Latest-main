using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.BALModel
{
    public class ApplyCandidate
    {
        [Key]
        public long AppliedId { get; set; }
        public long? JobId { get; set; }
        public string CandidateName { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AppliedFrom { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? AvailableDate { get; set; }
        public string Photo { get; set; }
        public string Attachment { get; set; }

        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }


    }
}
