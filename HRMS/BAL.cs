using HRMS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using TalenBAL.ViewModel;


namespace HRMS
{
    public class BAL
    {
        Models.HRMS db = new Models.HRMS();
        IList<EmployeeView> IListEmpView = new List<EmployeeView>();
        public bool UserLogin(string UserId,string Password)
        {
            bool Islogin = false;
            var record = db.Users.Where(x => x.LoginId == UserId && x.Password == Password).FirstOrDefault();
            if(record !=null)
            {
                Islogin = true;
            }
            return Islogin;
        }

        public dynamic GetEmployeeHolidays(int? id)
        {
           var obj = db.HrmEmployees.Where(x => x.Id == id).FirstOrDefault();

            var ShiftId = obj?.ShiftId;

            return  db.ShiftMasters.Where(x => x.ShiftId == ShiftId).Select(x => new
            {
                x.IsFriday,
                x.IsSaturday,
                x.IsSunday
            }).ToList();
        }


        public IList<HrmEmployee> GetAllEmployee()
        {
           
                try
                {
                    IList<HrmEmployee> IList = new List<HrmEmployee>();
                    var emp = db.HrmEmployees.Where(x => x.FirstName != "").ToList();
                    foreach (var item in emp)
                    {
                        HrmEmployee ObjModel = new HrmEmployee();
                        ObjModel.Id = item.Id;
                        ObjModel.TitleId = item.TitleId;
                        ObjModel.FirstName = item.FirstName +" "+ item.LastName;
                        ObjModel.Middlename = item.Middlename;
                        ObjModel.LastName = item.LastName;
                        ObjModel.EmployeeCode = item.EmployeeCode;
                        ObjModel.Gender = item.Gender;
                        ObjModel.FatherHusbandName = item.FatherHusbandName;
                        ObjModel.DateOfBirth = item.DateOfBirth;
                        ObjModel.IdentityCardNo = item.IdentityCardNo;
                        ObjModel.IdentityExpiryDate = item.IdentityExpiryDate;
                        ObjModel.ReligionId = item.ReligionId;
                        ObjModel.MaritalStatus = item.MaritalStatus;
                        ObjModel.Dependants = item.Dependants;
                        ObjModel.NationalCountryId = item.NationalCountryId;
                        ObjModel.EthnicityId = item.EthnicityId;
                        ObjModel.BloodGroup = item.BloodGroup;
                        ObjModel.HrmLanguageId = item.HrmLanguageId;
                        ObjModel.DisabilitiesId = item.DisabilitiesId;
                        ObjModel.EmployeeType = item.EmployeeType;
                        ObjModel.EmployeeGroupId = item.EmployeeGroupId;
                        ObjModel.HrmLocationOficeId = item.HrmLocationOficeId;
                        ObjModel.DesignationId = item.DesignationId;
                        ObjModel.ReportToId = item.ReportToId;
                        ObjModel.DepartmentId = item.DepartmentId;
                        ObjModel.SubDepartmentId = item.SubDepartmentId;
                        ObjModel.HrmGradeId = item.HrmGradeId;
                        ObjModel.MachineId = item.MachineId;
                        ObjModel.dteJoiningDate = item.dteJoiningDate;
                        ObjModel.ProbationPeriod = item.ProbationPeriod;
                        ObjModel.ConfirmationDate = item.ConfirmationDate;
                        ObjModel.ContractExpiryDate = item.ContractExpiryDate;
                        ObjModel.BasicSalary = item.BasicSalary;
                        ObjModel.ContacNo = item.ContacNo;
                        ObjModel.Email = item.Email;
                        ObjModel.LivingCountryId = item.LivingCountryId;
                        ObjModel.StateId = item.StateId;
                        ObjModel.CityId = item.CityId;
                        ObjModel.ZipCode = item.ZipCode;
                        ObjModel.CurrentAddress = item.CurrentAddress;
                        ObjModel.CurrentCountryId = item.CurrentCountryId;
                        ObjModel.CurrentStateId = item.CurrentStateId;
                        ObjModel.CurrentCityId = item.CurrentCityId;
                        ObjModel.CurrentZipCode = item.CurrentZipCode;
                        ObjModel.UserName = item.UserName;
                        ObjModel.Password = item.Password;
                        ObjModel.IswebloginAllowed = item.IswebloginAllowed;
                        ObjModel.SoftRoleinformation = item.SoftRoleinformation;
                        ObjModel.ApplicationUserId = item.ApplicationUserId;
                        ObjModel.ProfilePicture = item.ProfilePicture;
                        ObjModel.CostCenterId = item.CostCenterId;
                        ObjModel.Active = item.Active;
                        ObjModel.CreatedBy = item.CreatedBy;
                        ObjModel.CreatedOn = item.CreatedOn;
                        ObjModel.LastModifiedBy = item.LastModifiedBy;
                        ObjModel.LastModifiedOn = item.LastModifiedOn;
                        ObjModel.IsDeleted = item.IsDeleted;
                        ObjModel.Country_Id = item.Country_Id;
                        ObjModel.BiometricCode = item.BiometricCode;
                        ObjModel.Title = item.Title;
                        ObjModel.Passport = item.Passport;
                        ObjModel.DrivingLicence = item.DrivingLicence;
                        ObjModel.BirthCountryId = item.BirthCountryId;
                        ObjModel.HrmLanguage = item.HrmLanguage;
                        ObjModel.ProbationType = item.ProbationType;
                        ObjModel.OfficialNo = item.OfficialNo;
                        ObjModel.OfficialEmail = item.OfficialEmail;
                        ObjModel.RegionId = item.RegionId;
                        ObjModel.PermnantAddress = item.PermnantAddress;
                        ObjModel.BankName = item.BankName;
                        ObjModel.BranchCode = item.BranchCode;
                        ObjModel.BranchName = item.BranchName;
                        ObjModel.AccountTitle = item.AccountTitle;
                        ObjModel.AccountNumber = item.AccountNumber;
                        ObjModel.AccountType = item.AccountType;
                        ObjModel.PassportExpiryDate = item.PassportExpiryDate;
                        IList.Add(ObjModel);
                    }
                    // var result = _hrms.HrmEmployees.Select(x => x).ToList();
                    var result = IList;
                    //var result = JsonConvert.SerializeObject(data);
                    return IList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
         
        }
        public IList<LeaveType> GetLeaveType()
        {
            List<LeaveType> ObjList = new List<LeaveType>();
            IList<LeaveType> ObjIList = new List<LeaveType>();
            ObjList = db.LeaveTypes.ToList();
            try
            {
                foreach (var item in ObjList)
                {
              
                    ObjIList.Add(new LeaveType
                    {
                        Id = Convert.ToInt32(item.Id),
                        Name = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjIList;
        }

        public IList<PublicHoliday> GetHoliDay()
        {
            List<PublicHoliday> ObjList = new List<PublicHoliday>();
            IList<PublicHoliday> ObjIList = new List<PublicHoliday>();
            ObjList = db.PublicHolidays.ToList();
            try
            {
                foreach (var item in ObjList)
                {
                    
                    ObjIList.Add(new PublicHoliday
                    {
                        Pk_PublicId = Convert.ToInt32(item.Pk_PublicId),
                        Name = item.Name
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjIList;
        }

        public dynamic GetAttendanceBetwenDate(long EmpId, DateTime Date)
        {
        
            var GetDate = db.LeaveRequests.Where(x => x.Employee == EmpId && x.DateFrom >= Date && x.DateTo <= Date && x.Status == "Approved").ToList();

            return GetDate;
        }

        public dynamic GetAllEmployeeGroup()
        {
            var record = db.EmployeeGroups.Where(x => x.IsActive == true).ToList();
            return record;
        }

        public dynamic GetAllDepartment()
        {
            var record = db.Departments.Where(x => x.Active == true).ToList();
            return record;
        }

        public dynamic GetEmployeebyDepartment(long DepartmentId)
        {
            var record = db.HrmEmployees.Where(x => x.DepartmentId == DepartmentId && x.Active == true).ToList();
            return record;
        }

        public dynamic GetEmployeebyDepartmentName()
        {
            var record = (from s in db.HrmEmployees
                          join c in db.Departments on s.DepartmentId equals c.Id
                          where s.Active == true
                          select new
                          {
                              Id = s.Id,
                              EmployeeName = s.FirstName + " " + s.LastName,
                              DepartmentId = s.DepartmentId,
                              DepartmentName = c.Name,                             
                          }).ToList();

            foreach (var item in record)
            {
                EmployeeView Obj = new EmployeeView();
                Obj.Id = item.Id;
                Obj.FullName = item.EmployeeName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;

                IListEmpView.Add(Obj);
            }
            return IListEmpView;
        }

        public Response GetEmployeeMonthlyAttendanceRecord(string folder, AttendanceCalculationModel dataa)
        {
            Response _objRes = new Response();
            try
            {
                #region PDF SETUP, FETCH & CREATE PDF
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    #region FONT FOR PDF
                    Font _fontNormalCell = FontFactory.GetFont("Helvetica", 8, Font.NORMAL, BaseColor.BLACK);
                    Font _fontHeaderCell = FontFactory.GetFont("Helvetica", 8, Font.BOLD, BaseColor.BLACK);
                    #endregion

                    #region GET MONTHLY EMPLOYEE ATTENDANCE RECORD
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMS"].ConnectionString);
                    SqlDataAdapter adp = new SqlDataAdapter("SP_AttendanceCalculations", con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar).Value = dataa.ReportType;
                    adp.SelectCommand.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = dataa.EmployeeId ?? string.Empty;
                    adp.SelectCommand.Parameters.Add("@DeptId", SqlDbType.VarChar).Value = dataa.DepartmentId ?? string.Empty;
                    adp.SelectCommand.Parameters.Add("@Salary_Month", SqlDbType.Int).Value = dataa.Month;
                    adp.SelectCommand.Parameters.Add("@Salary_Year", SqlDbType.Int).Value = dataa.Year;
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    con.Close();
                    #endregion

                    #region CONVERT DATATABLE RECORDS TO LIST
                    List<AttendanceCalculationReportModel> _lstAttendanceReportModel = new List<AttendanceCalculationReportModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _lstAttendanceReportModel.Add(new AttendanceCalculationReportModel()
                        {
                            Name = dt.Rows[i]["Name"].ToString(),
                            Department = dt.Rows[i]["Department"].ToString(),
                            PresentDays = dt.Rows[i]["PresentDays"].ToString(),
                            LeaveDays = dt.Rows[i]["LeaveDays"].ToString(),
                            AbsentDays = dt.Rows[i]["AbsentDays"].ToString(),
                            TotalHoursWorked = dt.Rows[i]["TotalHoursWorked"].ToString(),
                            NormalOTHours = dt.Rows[i]["NormalOTHours"].ToString(),
                            WeekendOTHours = dt.Rows[i]["WeekendOTHours"].ToString(),
                            PublicHolidaysOTHours = dt.Rows[i]["PublicHolidaysOTHours"].ToString(),
                        });
                    }
                    #endregion

                    #region CREATE MONTHLY ATTENDANCE PDF
                    PdfPTable table = new PdfPTable(9);
                    table.WidthPercentage = 100;
                    table.HorizontalAlignment = 0;
                    table.SpacingBefore = 20f;
                    table.SpacingAfter = 30f;

                    PdfPCell _cell = new PdfPCell();
                    _cell = new PdfPCell(new Phrase("Employee's Monthly Attendance"));
                    _cell.Colspan = 9;
                    _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(_cell);

                    table.AddCell(CreateNormalCell("Name", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Department", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Present Days", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Leave Days", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Absent Days", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Total Hours Worked", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Normal OT", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Weekend OT", BaseColor.WHITE, _fontHeaderCell));
                    table.AddCell(CreateNormalCell("Public Holidays OT", BaseColor.WHITE, _fontHeaderCell));

                    for (int i = 0; i < _lstAttendanceReportModel.Count; i++)
                    {
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].Name, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].Department, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].PresentDays, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].LeaveDays, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].AbsentDays, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].TotalHoursWorked, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].NormalOTHours, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].WeekendOTHours, BaseColor.WHITE, _fontNormalCell));
                        table.AddCell(CreateNormalCell(_lstAttendanceReportModel[i].PublicHolidaysOTHours, BaseColor.WHITE, _fontNormalCell));
                    }
                    #endregion

                    pdfDoc.Add(table);
                    pdfWriter.CloseStream = false;
                    pdfDoc.Close();
                    byte[] content = memoryStream.ToArray();
                    // Write out PDF from memory stream.
                    using (FileStream fs = System.IO.File.Create(folder))
                    {
                        fs.Write(content, 0, (int)content.Length);
                    }
                }
                #endregion

                _objRes.StatusCode = 200;
            }
            catch (Exception ex)
            {
                _objRes.StatusCode = 500;
            }
            return _objRes;
        }

        public static PdfPCell CreateNormalCell(string txt, BaseColor backGroundColor, Font fontStyle)
        {
            PdfPCell _pdfPCell = new PdfPCell(new Phrase(txt, fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.BackgroundColor = backGroundColor;
            _pdfPCell.Border = Rectangle.ALIGN_CENTER;
            return _pdfPCell;
        }
    }
}