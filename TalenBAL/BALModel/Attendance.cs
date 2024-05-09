namespace TalenBAL.BALModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class Attendance
    {
        [Key]
        public long AttendaceId { get; set; }
        public string AttendanceDate { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }
      
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool IsPresent { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsLeave { get; set; }
        public string LeaveType { get; set; }
        public bool IsHoliday { get; set; }
        public string Holiday { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarly { get; set; }
        public long? Department { get; set; }
        public long? EmployeeId { get; set; }
        public string Year { get; set; }
    }
}
