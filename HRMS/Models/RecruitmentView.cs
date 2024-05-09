﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    class RecruitmentView
    {
        public long Id { get; set; }
        public string AddvertiseNo { get; set; }
        public long JobTypeId { get; set; }
        public string JobTypeName { get; set; }
        public string JobTitle { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public int MinExpereince { get; set; }
        public int MaxExpereince { get; set; }
        public string MInQualification { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Skills { get; set; }
        public DateTime LastDate { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string Currency { get; set; }
        public int Status { get; set; }
    }
}