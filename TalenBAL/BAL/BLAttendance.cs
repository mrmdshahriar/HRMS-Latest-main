using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalenBAL.ViewModel;
using TalenBAL.BALModel;

using ExcelDataReader;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Globalization;

namespace TalenBAL.BAL
{
    public class BLAttendance
    {
        BALHRMS db = new BALHRMS();

        #region MONTHLY ATTENDANCE
        public dynamic GetEmployeesLeaveRequests()
        {
            dynamic _lstEmployeesLeaveRequests = null;
            try
            {
                _lstEmployeesLeaveRequests = db.LeaveRequests
                    .Join(
                       db.LeaveTypes.Where(x => !(x.IsDeleted ?? false)).Select(x => new { x.Id, x.Name, x.IsDeleted }),
                       LR => LR.LeaveTypeId,
                       LT => LT.Id,
                       (LR, LT) => new
                       {
                           LR.Employee,
                           LR.DateFrom,
                           LR.DateTo,
                           LR.Reason,
                           LR.Status,
                           LT.Name
                       }).Where(x => (x.Status ?? string.Empty).Equals("Approved")).ToList();
            }
            catch (Exception ex)
            {

            }
            return _lstEmployeesLeaveRequests;
        }
        public dynamic GetPublicHolidays()
        {
            dynamic _lstPublicHolidays = null;
            try
            {
                _lstPublicHolidays = db.PublicHolidays.Where(x => !(x.IsDeleted ?? false))
                    .Select(x => new
                    {
                        x.Name,
                        x.DateFrom,
                        x.DateTo
                    }).ToList();
            }
            catch (Exception ex)
            {

            }
            return _lstPublicHolidays;
        }


        public dynamic GetOffDays()
        {
            dynamic _lstoffdays = null;
            try
            {
                _lstoffdays = db.ShiftMasters.Select(x => new
                {
                    x.IsFriday,
                    x.IsSaturday,
                    x.IsSunday,
                    x.ShiftId,
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return _lstoffdays;
        }
        #endregion

        #region SAVE ATTENDANCE (MONTHLY & DAILY ATTENDANCE)
        public Response AddAttendance(EmployeeModel dataa)
        {
            Response _objRes = new Response();
            List<Attendance> attendanceList = new List<Attendance>();
            try
            {
                if (dataa != null)
                {
                    if (dataa.EmployeeAttendances.Count > 0)
                    {
                        dataa.EmployeeAttendances.ForEach(x =>
                        {
                            
                            attendanceList.Add(new Attendance
                            {
                                
                                 EmployeeId = x.EmployeeId,
                                IsPresent = x.IsPresent,
                                 Date = x.AttendanceDate,
                                AttendanceDate = x.AttendanceDate,
                                //Date = dateTime,//Convert.ToDateTime(x.AttendanceDate), //== DateTime.MinValue ? (DateTime?)null : x.Date,
                                // just convert date from strng to date
                                Month = x.Month,
                                TimeIn = x.TimeIn,
                                TimeOut = x.TimeOut,
                                IsAbsent = x.IsAbsent,
                                IsLeave = x.IsLeave,
                                LeaveType = x.LeaveType,
                                Holiday = x.Holiday,
                                IsHalfDay = x.IsHalfDay,
                                IsLate = x.IsLate,
                                IsEarly = x.IsEarly,
                                Department = x.Department,
                                Year = x.Year
                            });  

                        });
                        if (attendanceList.Count > 0)
                        {
                            db.Attendances.AddRange(attendanceList);
                            db.SaveChanges();

                            _objRes.StatusCode = 200;
                            _objRes.StatusMessage = "Monthly attendance has been saved successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _objRes.StatusCode = 500;
                _objRes.StatusMessage = "Something went wrong, please contact technical team!";
            }
            return _objRes;
        }
        #endregion

        #region CHECK EARLY/LATE/HALF-LEAVE (MONTHLY & DAILY ATTENDANCE)
        public Response CheckEmployeeTimeFacility(long EmpId, TimeSpan StartTime, TimeSpan EndTime)
        {
            Response _objRes = new Response();
            EmployeeLeaveViewModel _employeeLeaveViewModel = new EmployeeLeaveViewModel();
            try
            {

                long? sId = db.HrmEmployees.Where(y => y.Id == EmpId).Select(y => y.ShiftId).FirstOrDefault();
                var objEmployeeShiftTimings = db.ShiftMasters.Where(x => x.ShiftId == sId).FirstOrDefault();
                if (objEmployeeShiftTimings != null)
                {
                    _employeeLeaveViewModel.IsPresent = true;
                    _employeeLeaveViewModel.IsLate = StartTime > objEmployeeShiftTimings.GressTime ? true : false;
                    _employeeLeaveViewModel.IsEarly = EndTime < objEmployeeShiftTimings.EarlyLeave ? true : false;
                    _employeeLeaveViewModel.IsHalfDay = StartTime > objEmployeeShiftTimings.HalfDay || EndTime < objEmployeeShiftTimings.HalfDay ? true : false;
                    _objRes.StatusCode = 200;
                    _objRes.Result = _employeeLeaveViewModel;
                }
            }
            catch (Exception ex)
            {
                _objRes.StatusCode = 500;
                _objRes.StatusMessage = "Something went wrong, please contact technical team!";
            }
            return _objRes;
        }
        #endregion

        #region CHECK TODAY'S ATTENDANCE
        public Response GetLeaveByEmployeeAndHoliday(long EmpId, DateTime dt)
        {
            EmployeeLeaveViewModel _employeeLeaveViewModel = new EmployeeLeaveViewModel();
            Response _objRes = new Response();
            try
            {
                long? _sId = db.HrmEmployees.Where(y => y.Id == EmpId).Select(y => y.ShiftId).FirstOrDefault();
                var _objEmployeeShiftTimings = db.ShiftMasters.Where(x => x.ShiftId == _sId).FirstOrDefault();
                var _objHolidays = db.PublicHolidays.Where(y => y.DateFrom >= dt && y.DateTo <= dt && y.IsActive == true).FirstOrDefault();

                _employeeLeaveViewModel.IsHoliday = _objHolidays == null ? false : true;
                _employeeLeaveViewModel.Holiday = _objHolidays == null ? 0 : _objHolidays.Pk_PublicId;
                var _objLeave = db.LeaveRequests.Where(x => DbFunctions.TruncateTime(x.DateTo) >= DbFunctions.TruncateTime(dt) && DbFunctions.TruncateTime(x.DateFrom) <= DbFunctions.TruncateTime(dt)
                                                && x.Status == "Approved" && x.Employee == EmpId).FirstOrDefault();

                _employeeLeaveViewModel.LeaveType = _objLeave == null ? 0 : Convert.ToInt32(_objLeave.LeaveTypeId);
                _employeeLeaveViewModel.IsLeave = _objLeave == null ? false : true;
                if (_objEmployeeShiftTimings != null)
                {
                    _employeeLeaveViewModel.IsFriday = dt.DayOfWeek == DayOfWeek.Friday && _objEmployeeShiftTimings.IsFriday == true ? true : false;
                    _employeeLeaveViewModel.IsSaturday = dt.DayOfWeek == DayOfWeek.Saturday && _objEmployeeShiftTimings.IsSaturday == true ? true : false;
                    _employeeLeaveViewModel.IsSunday = dt.DayOfWeek == DayOfWeek.Sunday && _objEmployeeShiftTimings.IsSunday == true ? true : false;
                }
                _objRes.StatusCode = 200;
                _objRes.Result = _employeeLeaveViewModel;
            }
            catch (Exception ex)
            {
                _objRes.StatusCode = 500;
                _objRes.StatusMessage = "Something went wrong, please contact technical team!";
            }
            return _objRes;
        }
        #endregion

        #region EXCEL SHEET ATTENDANCE
        public void ReadAsDataTableAndInsertIntoTable(string path)
        {

            List<Attendance> hrmAttendances = new List<Attendance>();
            DataTable Exceldt = ConvertToDataTable(path);
            DateTime? _TimeIn = null;
            DateTime? _TimeOut = null;
            for (int i = 0; i < Exceldt.Rows.Count; i++)
            {
                string _checkrow = Convert.ToString(Exceldt.Rows[i][0]);
                if (_checkrow.Trim().ToLower() == "employeecode")
                    continue;
                if (Exceldt.Rows[i][0] == DBNull.Value && Exceldt.Rows[i][1] == DBNull.Value && Exceldt.Rows[i][2] == DBNull.Value && Exceldt.Rows[i][3] == DBNull.Value && Exceldt.Rows[i][6] == DBNull.Value)
                    continue;
                else
                {
                    try
                    {
                        string _employeeCode = Convert.ToString(Exceldt.Rows[i][0]).Trim().ToLower();
                        string _department = Convert.ToString(Exceldt.Rows[i][1]).Trim().ToLower();
                        string _employeeName = Convert.ToString(Exceldt.Rows[i][2]).Trim().ToLower();
                        string _date = Convert.ToString(Exceldt.Rows[i][3]).Trim().ToLower();
                        string _timeIn = Convert.ToString(Exceldt.Rows[i][4]);
                        string _timeOut = Convert.ToString(Exceldt.Rows[i][5]);
                        string _shiftCode = Convert.ToString(Exceldt.Rows[i][6]).Trim().ToLower();
                        _date = Convert.ToDateTime(_date).ToShortDateString();
                        if (!string.IsNullOrEmpty(_timeIn) && !string.IsNullOrEmpty(_timeOut))
                        {

                            _timeIn = Convert.ToDateTime(_timeIn).ToShortTimeString();
                            _timeOut = Convert.ToDateTime(_timeOut).ToShortTimeString();
                            _TimeIn = Convert.ToDateTime(_date + " " + _timeIn);
                            _TimeOut = DateTime.Parse(_date + " " + _timeOut);
                        }
                        ViewModelAttandence _attandence = MarksCheck(_employeeCode, Convert.ToDateTime(_date), Convert.ToDateTime(_TimeIn), Convert.ToDateTime(_TimeOut), _shiftCode);
                        hrmAttendances.Add(new Attendance
                        {
                            EmployeeId = _attandence.employeeId,
                            IsAbsent = _attandence.IsAbsent,
                            IsLeave = _attandence.Isleave,
                            IsPresent = _attandence.IsPresent,
                            IsEarly = _attandence.IsEarly,
                            IsLate = _attandence.IsLate,
                            IsHalfDay = _attandence.IsHalfDay,
                            IsHoliday = _attandence.IsHoliday,
                            TimeIn = Convert.ToDateTime(_TimeIn),
                            TimeOut = Convert.ToDateTime(_TimeOut),
                            Date = _date,
                        });
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
            if (hrmAttendances.Count > 0)
            {
                DataTable dt = ToDataTable(hrmAttendances);
                //string connString = "data source=DESKTOP-5LE59CT;initial catalog=HRMS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework providerName = System.Data.SqlClient";//ConnectionStrings["BALHRMS"].ToString();
                //  string connString = "data source=DESKTOP-17KLQ1B\\SQLEXPRESS;initial catalog=HRMS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework providerName = System.Data.SqlClient";//ConnectionStrings["BALHRMS"].ToString();
                string connString = "data source=Talentdb.mssql.somee.com;initial catalog=Talentdb;user id=ghazihur_SQLLogin_1;pwd=oq4f22l1hb;MultipleActiveResultSets=True;App=EntityFramework providerName = System.Data.SqlClient";//ConnectionStrings["BALHRMS"].ToString();

                
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    // make sure to enable triggers
                    // more on triggers in next post
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(
                        connection,
                        SqlBulkCopyOptions.TableLock |
                        SqlBulkCopyOptions.FireTriggers |
                        SqlBulkCopyOptions.UseInternalTransaction,
                        null
                        );

                    // set the destination table name
                    bulkCopy.DestinationTableName = "dbo.Attendances";
                    connection.Open();
                    // write the data in the "dataTable"
                    bulkCopy.WriteToServer(dt);
                    connection.Close();
                }
                // reset
                dt.Clear();
            }


        }
        public static DataTable ConvertToDataTable(string FileName)
        {
            DataTable res = null;

            FileInfo fileInfo = new FileInfo(FileName);
            string file = fileInfo.Name;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader;
            if (Path.GetExtension(file) == ".xls")
                excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
            else if (Path.GetExtension(file) == ".xlsx")
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
            else
                excelReader = ExcelReaderFactory.CreateCsvReader(fs);

            DataSet dsUnUpdated = new DataSet();
            dsUnUpdated = excelReader.AsDataSet();
            excelReader.Close();
            fs.Dispose();

            if (dsUnUpdated != null && dsUnUpdated.Tables.Count > 0)

            {
                res = dsUnUpdated.Tables[0];
            }
            else
            {
            }


            return res;

        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public ViewModelAttandence MarksCheck(string employee, DateTime date, DateTime TimeIn, DateTime Timeout, string code)
        {
            var getEmployee = db.HrmEmployees.FirstOrDefault(x => x.EmployeeCode == employee);
            ViewModelAttandence _attandence = new ViewModelAttandence();
            if (getEmployee != null)
            {
                _attandence.employeeId = getEmployee.Id;
                if (TimeIn == null && Timeout == null)
                {
                    var _checkLeave = db.LeaveRequests.Any(x => x.Employee == getEmployee.Id && x.IsActive == true && (date >= x.DateFrom && date <= x.DateTo));
                    if (_checkLeave)
                        _attandence.Isleave = true;
                    else
                        _attandence.IsAbsent = false;
                }
                else
                {

                    var _getData = db.ShiftMasters.FirstOrDefault(x => x.Code == code);
                    if (_getData != null)
                    {
                        _attandence.IsPresent = true;
                        getEmployee.ShiftId = _getData.ShiftId;
                        DateTime _StartTime = Convert.ToDateTime(date + _getData.StartTime);
                        DateTime _HalfDay = Convert.ToDateTime(date + _getData.HalfDay);
                        DateTime _EndTime = Convert.ToDateTime(date + _getData.StartTime);
                        DateTime _EarlyTime = Convert.ToDateTime(date + _getData.EarlyLeave);

                        if (TimeIn.TimeOfDay > _StartTime.TimeOfDay)
                            _attandence.IsLate = true;
                        if (Timeout.TimeOfDay >= _HalfDay.TimeOfDay && Timeout.TimeOfDay <= _EndTime.TimeOfDay)
                            _attandence.IsHalfDay = true;
                        if (Timeout.TimeOfDay <= _EarlyTime.TimeOfDay)
                            _attandence.IsEarly = true;
                        if (_getData != null)
                        {
                            bool IsFriday = date.DayOfWeek == DayOfWeek.Friday && _getData.IsFriday == true ? true : false;
                            bool IsSaturday = date.DayOfWeek == DayOfWeek.Saturday && _getData.IsSaturday == true ? true : false;
                            bool IsSunday = date.DayOfWeek == DayOfWeek.Sunday && _getData.IsSunday == true ? true : false;
                            if (IsFriday || IsSaturday || IsSunday)
                            {
                                _attandence.IsHoliday = true;
                            }
                        }

                    }
                }
            }
            db.SaveChanges();
            return _attandence;
        }



        public dynamic GetAttendanceListRecord(long EmployeeId,DateTime DateFrom, DateTime DateTo, string Month, string Year)
        {
            try
            {

                var ListRecord =
                         (from emp in db.HrmEmployees

                          where emp.Id == EmployeeId //&& Year == Year
                          select new
                          {
                              Employee = emp.Id,
                              EmployeeCode = emp.EmployeeCode,
                              EmployeeName = emp.FirstName + " " + emp.LastName,
                              MonthYear = Month + "," + Year,
                              TotalDays = db.Attendances.Where(x => x.Month == Month && x.Year == Year).Count(),
                              PresentDays = db.Attendances.Where(x => x.IsPresent == true && x.Month == Month).Count(),
                              AbsentDays = db.Attendances.Where(x => x.IsAbsent == true && x.Month == Month).Count(),
                              LeaveDays = db.Attendances.Where(x => x.IsLeave == true && x.Month == Month).Count(),
                              EarlyDays = db.Attendances.Where(x => x.IsEarly == true && x.Month == Month).Count(),
                              LateDays = db.Attendances.Where(x => x.IsLate == true && x.Month == Month).Count(),
                              HalfDays = db.Attendances.Where(x => x.IsHalfDay == true && x.Month == Month).Count()

                          }).ToList();
             
                return ListRecord;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion
    }
}
