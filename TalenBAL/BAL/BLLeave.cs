using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
   public class BLLeave
    {
        BALHRMS db = new BALHRMS();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        public dynamic GetAllLeaveType()
        {
            var record = db.LeaveTypes.Where(x => x.IsActive == true).ToList();
            return record;
        }
        public dynamic GetLeavesByEmployee(long EmployeeId)
        {
            var record = from st in db.LeaveRequests
                         join d in db.LeaveTypes on st.LeaveTypeId equals d.Id into table1
                         from d in table1.ToList()
                         join i in db.HrmEmployees on st.Employee equals i.Id into table2
                         from i in table2.ToList()
                         where i.Id == EmployeeId
                         select new
                         {
                             Id = st.Pk_LeaveRequest,
                             EmployeeId = st.Employee,
                             EmployeeCode = i.EmployeeCode,
                             EmployeeName = i.FirstName + " " + i.LastName,
                             LeaveTypeId = st.LeaveTypeId,
                             LeaveTypeName = d.Name,
                             DateFrom = st.DateFrom.ToString(),
                             DateTo = st.DateTo.ToString(),                             
                             LeaveDays = st.LeaveDays,
                             Reason = st.Reason,                             
                             Status = st.Status
                             
                         };

            return record;
        }

        public dynamic AddLeaveRequest(string Auth, long EmployeeId, long LeaveTypeId, DateTime DateFrom, DateTime DateTo,
                                                   int LeaveDays, string Reason)
        {
          
            ObjLeaveRequest.Employee = EmployeeId;
            ObjLeaveRequest.LeaveTypeId = LeaveTypeId;
            ObjLeaveRequest.DateFrom = DateFrom;
            ObjLeaveRequest.DateTo = DateTo;
            ObjLeaveRequest.LeaveDays = LeaveDays;
            ObjLeaveRequest.Reason = Reason;
            ObjLeaveRequest.IsActive = true;
            ObjLeaveRequest.Status = "Pending";
            ObjLeaveRequest.CreatedBy = EmployeeId.ToString();
            ObjLeaveRequest.CreatedOn = DateTime.Now;
            try
            {        
                db.LeaveRequests.Add(ObjLeaveRequest);
                db.SaveChanges();
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            return ObjLeaveRequest;
        }

        public dynamic GetLeaveStatus()
        {
            var record = from st in db.LeaveRequests
                         join d in db.LeaveTypes on st.LeaveTypeId equals d.Id into table1
                         from d in table1.ToList()
                         join i in db.HrmEmployees on st.Employee equals i.Id into table2
                         from i in table2.ToList()

                         select new
                         {
                             Id = st.Pk_LeaveRequest,
                             Name = d.Name,
                             DateFrom = st.DateFrom.ToString(),
                             DateTo = st.DateTo.ToString(),
                             Active = st.IsActive,
                             LeaveDays = st.LeaveDays,
                             Reason = st.Reason,
                             EmployeeName = i.FirstName,
                             Status = st.Status

                         };
            return record;
        }
    }
}
