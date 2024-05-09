using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
   public class BLPayRoll
    {
        BALHRMS db = new BALHRMS();
     
        SalaryRequest ObjSalaryRequest = new SalaryRequest();
        LoanRequest ObjLoanRequest = new LoanRequest();
        ExpnesesRequest ObjExpnesesRequest = new ExpnesesRequest();


        #region Salary
        public dynamic GetSalaryMonthByEmployee(long EmpId,string Month)
        {
            var record = db.SalaryRequests.Where(x => x.EmployeeId == EmpId && x.SalaryMonth == Month).FirstOrDefault();
            return record;
        }

        public dynamic AddSalaryRequest(long EmployeeId, string SalaryMonth, string SalaryMode, decimal Amount)
        {
            bool result = false;
            try
            {

           
            ObjSalaryRequest.EmployeeId = EmployeeId;
            ObjSalaryRequest.SalaryMonth = SalaryMonth;
            ObjSalaryRequest.SalaryMode = SalaryMode;
            ObjSalaryRequest.Amount = Amount;
            ObjSalaryRequest.Status = "Pending";
            ObjSalaryRequest.CreatedBy = EmployeeId;
            ObjSalaryRequest.CreatedOn = DateTime.Now;
            db.SalaryRequests.Add(ObjSalaryRequest);
            db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Loan
        public dynamic GetLoanByEmployee(long EmpId, DateTime Date)
        {

          
            var record = db.LoanRequests.Where(x => x.EmployeeId == EmpId && x.RequestDate == Date).FirstOrDefault();
            return record;
        }
        public dynamic AddLoanRequest(long EmployeeId, decimal Amount, int Duration, DateTime RequestDate,string Purpose)
        {
            bool result = false;
            try
            {


                ObjLoanRequest.EmployeeId = EmployeeId;
                ObjLoanRequest.Amount = Amount;
                ObjLoanRequest.Duration = Duration;
                ObjLoanRequest.RequestDate = RequestDate;
                ObjLoanRequest.Purpose = Purpose;
                ObjLoanRequest.Status = "Pending";
                ObjLoanRequest.CreatedBy = EmployeeId;
                ObjLoanRequest.CreatedOn = DateTime.Now;
                db.LoanRequests.Add(ObjLoanRequest);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        #endregion

        #region Expenses
        public dynamic GetExpensesByEmployee(long EmpId, DateTime Date)
        {


            var record = db.ExpnesesRequests.Where(x => x.EmployeeId == EmpId && x.RequestDate == Date).FirstOrDefault();
            return record;
        }
        public dynamic AddExpensesRequest(long EmployeeId, decimal Amount,  DateTime RequestDate, string Purpose,string Attachment)
        {
            bool result = false;
            try
            {
                ObjExpnesesRequest.EmployeeId = EmployeeId;
                ObjExpnesesRequest.Amount = Amount;             
                ObjExpnesesRequest.RequestDate = RequestDate;
                ObjExpnesesRequest.Purpose = Purpose;
                ObjExpnesesRequest.Attachment = Attachment;
                ObjExpnesesRequest.Status = "Pending";
                ObjExpnesesRequest.CreatedBy = EmployeeId;
                ObjExpnesesRequest.CreatedOn = DateTime.Now;
                db.ExpnesesRequests.Add(ObjExpnesesRequest);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region Task
        public dynamic GetEmployeeTask(long EmpId)
        {
            var record = db.EmployeeTasks.Where(x => x.EmployeeId == EmpId).ToList();
            return record;
        }


        public dynamic GetSalaryByEmployee(long EmpId)
        {
            var record = db.SalaryRequests.Where(x => x.EmployeeId == EmpId).ToList();
            return record;
        }

        public dynamic GetExpensesByEmployee(long EmpId)
        {
            var record = db.ExpnesesRequests.Where(x => x.EmployeeId == EmpId).ToList();
            return record;
        }

        public dynamic GetLoanByEmployee(long EmpId)
        {
            var record = db.LoanRequests.Where(x => x.EmployeeId == EmpId).ToList();
            return record;
        }
        #endregion
    }
}
