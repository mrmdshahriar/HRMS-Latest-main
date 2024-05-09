using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class SalaryCalculationHeader
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime? DateofJoining { get; set; }
        public string SalaryMonth { get; set; }
        public DateTime? SalaryDate { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalMonthlySalary { get; set; }
        public decimal? Allowances { get; set; }
        public int? OThours { get; set; }
        public decimal? OTAmount { get; set; }
        public int? NormalOTHours { get; set; }
        public decimal? NormalOTSalary { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? Loan { get; set; }
        public decimal? EOBI { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? ProvidentFund { get; set; }
        public decimal? Advance { get; set; }
        public decimal? TotalSalaryWithAllowances { get; set; }
        public int? TotalPayableDays { get; set; }
        public int? WeekendOT { get; set; }
        public decimal? WeekendOTSalary { get; set; }
        public int? PublicHolidayOTHours { get; set; }
        public decimal? PublicHolidayOTSalary { get; set; }
        public decimal? Arrears { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? GrossPayableSalary { get; set; }
    }
}


