using HRMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TalenBAL.BAL;
using TalenBAL.ViewModel;
//using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Reflection;

namespace HRMS.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        Attendance ObjAttendance1 = new Attendance();
        BAL ObjBAL = new BAL();
        //JsonResult result = new JsonResult();
        JsonResult result = new JsonResult();
        DataSetCache Cache = new DataSetCache();
        IList<Attendance> IList = new List<Attendance>();
        BLAttendance _objBLAttendance = new BLAttendance();
        public void FetchAttendanceReport(AttendanceCalculationModel dataa)
        {
            #region CREATE DIRECTORY IF NOT EXISTS
            string folder = Server.MapPath("~/AttendanceReports");
            string path = "/AttendanceReports/EmployeeAttendance.pdf";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folder);
            folder = Server.MapPath("~/" + path);
            if (!dir.Exists)
                dir.Create();
            #endregion

            #region DELETE FILE IF EXISTS
            var exfiles = dir.GetFiles();
            foreach (var item in exfiles)
            {
                try
                {
                    item.Delete();
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            #endregion

            #region APPLYING LOGIC
            ObjBAL.GetEmployeeMonthlyAttendanceRecord(folder, dataa);
            #endregion
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttendaceList()
        {
            return View();
        }

        public ActionResult GetAttendaceList()
        {
            DateTime DateFrom = Convert.ToDateTime("1/2/2023");
            DateTime DateTo = Convert.ToDateTime("4/2/2023");
            DateTime cuurent = DateTime.Now;
            string Month = DateTime.Now.ToString("MMMM");
            string CurrentYear = DateTime.Now.Year.ToString();

            var rec = _objBLAttendance.GetAttendanceListRecord(10,DateFrom, DateTo, Month, CurrentYear);
            return Json(rec, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult AttendaceForm()
        {
            ViewBag.EmployeeDDl = ObjBAL.GetAllEmployee();
            ViewBag.LeaveTypeDDL = ObjBAL.GetLeaveType();
            ViewBag.HoliDays = ObjBAL.GetHoliDay();
            return View();
        }
        [HttpPost]
        public JsonResult GetLeaveByEmployee(Attendance model, FormCollection form, long EmpId, DateTime Date)
        {
            JsonResult result = new JsonResult();
            var IsRecordExist = ObjBAL.GetAttendanceBetwenDate(EmpId, Date);
            result = this.Json(JsonConvert.SerializeObject(IsRecordExist), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult ManualAttendace()
        {
            ViewBag.DepartmentDLL = ObjBAL.GetAllDepartment();
            ViewBag.EmployeeDLL = ObjBAL.GetAllEmployee();
            ViewBag.EmployeeLeaveRequests = JsonConvert.SerializeObject(_objBLAttendance.GetEmployeesLeaveRequests());
            ViewBag.GetPublicHolidays = JsonConvert.SerializeObject(_objBLAttendance.GetPublicHolidays());
            ViewBag.GetOffDays = JsonConvert.SerializeObject(_objBLAttendance.GetOffDays());
            return View();
        }

        public ActionResult GetEmployeeHolidays(int? id)
        {

            var result = ObjBAL.GetEmployeeHolidays(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEarlyOrLateCalculation(int? id)
        {
            var result = ObjBAL.GetEmployeeTimeIn(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AttendanceCalculation()
        {
            ViewBag.DepartmentDLL = ObjBAL.GetAllDepartment();
            ViewBag.EmployeeDLL = JsonConvert.SerializeObject(ObjBAL.GetAllEmployee());
            return View();
        }
        [HttpPost]
        public ActionResult DepartmentChange(long DepartmentId)
        {
            ViewBag.EmployeeDLL = ObjBAL.GetEmployeebyDepartment(DepartmentId);
            var Rresponse = "True";
            result = this.Json(JsonConvert.SerializeObject(Rresponse), JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult AddAttendance(EmployeeModel dataa)
        {
            Response _objRes = _objBLAttendance.AddAttendance(dataa);
            return Json(_objRes.StatusCode == 200 ? true : false);
        }

        [HttpPost]
        public JsonResult CheckEmployeeTimeFacility(long EmpId, TimeSpan StartTime, TimeSpan EndTime)
        {
            JsonResult result = new JsonResult();
            Response _objRes = _objBLAttendance.CheckEmployeeTimeFacility(EmpId, StartTime, EndTime);
            result = this.Json(JsonConvert.SerializeObject(_objRes.Result), JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult GetLeaveByEmployeeAndHoliday(long EmpId, DateTime dt)
        {
            JsonResult result = new JsonResult();
            Response _objRes = _objBLAttendance.GetLeaveByEmployeeAndHoliday(EmpId, dt);
            result = this.Json(JsonConvert.SerializeObject(_objRes.Result), JsonRequestBehavior.AllowGet);
            return result;
        }
        public ActionResult UploadAttendance()
        {
            TempData["CheckData"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult UploadAttendance(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    TempData["CheckData"] = "success";
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Server.MapPath("~/VLAFiles");
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);
                    try
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(filePath);
                        foreach (FileInfo item in di.GetFiles())
                        {
                            item.Delete();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    string fileAbsolutePath = Path.Combine(filePath, fileName);
                    file.SaveAs(fileAbsolutePath);
                    _objBLAttendance.ReadAsDataTableAndInsertIntoTable(fileAbsolutePath);
                }
            }
            catch (Exception ex)
            {
                TempData["CheckData"] = "error";
            }
            return View();
        }
    }
}