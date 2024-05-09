using HRMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static iTextSharp.text.pdf.AcroFields;

namespace HRMS.Controllers
{
    public class ShiftMasterController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();

        #region ShiftMaster
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllShifts()
        {
            _hrms.Configuration.ProxyCreationEnabled = false;
            var items = _hrms.ShiftMasters.ToList();
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var data = new List<object>();
            foreach (var item in items)
            {
                var obj = new
                {
                    data = item,
                    StartTime =  new DateTime(item.StartTime.Ticks).ToString("hh:mm tt"),
                    EndTime =    new DateTime(item.EndTime.Ticks).ToString("hh:mm tt"),
                    GressTime =  new DateTime(item.GressTime.Ticks).ToString("hh:mm tt"),
                    EarlyLeave = new DateTime(item.EarlyLeave.Ticks).ToString("hh:mm tt"),
                    HalfDay =    new DateTime(item.HalfDay.Ticks).ToString("hh:mm tt"),
                };
                data.Add(obj);
            }
            var result = JsonConvert.SerializeObject(data, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Shifts(int? id)
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;

            var LastEmployee = _hrms.ShiftMasters.ToList().LastOrDefault();
            if (LastEmployee != null && !string.IsNullOrEmpty(LastEmployee.Code))
            {
                stringCode = LastEmployee.Code.Substring(0, 3);
                int intCode = Convert.ToInt16(LastEmployee.Code.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D3"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "Shf" + 1.ToString("D3");
            }
            ViewBag.Code = stringCode;
            return View();
        }
        [HttpGet]
        public ActionResult GetShiftById(int id)
        {
            try
            {
                var stringCode = string.Empty;
                _hrms.Configuration.ProxyCreationEnabled = false;
                //var data = _hrms.ShiftMasters
                //       .Where(x => x.ShiftId == id).FirstOrDefault();
                //if (data is null)
                //{
                //    return Json(data, JsonRequestBehavior.AllowGet);
                //}

                //object obj = new { data,

                //    StartTime = new DateTime(data.StartTime.Ticks).ToString("HH:mm"),
                //    EndTime = new DateTime(data.EndTime.Ticks).ToString("HH:mm"),
                //    GressTime = new DateTime(data.GressTime.Ticks).ToString("HH:mm"),
                //    EarlyLeave = new DateTime(data.EarlyLeave.Ticks).ToString("HH:mm"),
                //    HalfDay = new DateTime(data.HalfDay.Ticks).ToString("HH:mm"),


                //};
                //JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                //var result = JsonConvert.SerializeObject(obj, jss);
                //return Json(result, JsonRequestBehavior.AllowGet);

                //JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                //var result = JsonConvert.SerializeObject(record, jss);
                var data = _hrms.ShiftMasters.Where(x => x.ShiftId == id).FirstOrDefault();

                object obj = new
                {
                    data,

                    StartTime = new DateTime(data.StartTime.Ticks).ToString("HH:mm"),
                    EndTime = new DateTime(data.EndTime.Ticks).ToString("HH:mm"),
                    GressTime = new DateTime(data.GressTime.Ticks).ToString("HH:mm"),
                    EarlyLeave = new DateTime(data.EarlyLeave.Ticks).ToString("HH:mm"),
                    HalfDay = new DateTime(data.HalfDay.Ticks).ToString("HH:mm"),
                    ShiftName = data.ShiftName,
                    Code = data.Code,
                    IsFriday = data.IsFriday,
                    IsSaturday = data.IsSaturday,
                    IsSunday = data.IsSunday,
                    Breakhour = data.Breakhour,
                };
                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                var result = JsonConvert.SerializeObject(obj, jss);
                return Json(result, JsonRequestBehavior.AllowGet);
                //return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult InsertUpdateShift(ShiftMaster employee)
        {
            string message = "";
           
            if (employee.ShiftId > 0)
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
              
                _hrms.Entry(employee).State = EntityState.Modified;
                message = "Updated Successfully.";
            }
            else
            {
                var ShifName = _hrms.ShiftMasters.Where(x => x.ShiftName == employee.ShiftName).FirstOrDefault();
                if(ShifName == null)
                {
                    _hrms.ShiftMasters.Add(employee);
                    message = "Add Successfully.";
                }
                else
                {
                    message = "Shift name already exist.";
                }
               
            }
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            //return Json(new { success = true, Id = employee.ShiftId, message = message, JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Shift Name is Already Exists.", JsonRequestBehavior.AllowGet });
           // return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        [HttpGet]
        public ActionResult DeleteShift(int Id)
        {
            ShiftMaster emp = _hrms.ShiftMasters.Where(x => x.ShiftId == Id).FirstOrDefault<ShiftMaster>();
            _hrms.ShiftMasters.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion
    }
}