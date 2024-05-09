using HRMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class NotificationRequestsController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        // GET: NotificationRequests
        public ActionResult Index()
        {
            return View();
        }

        //Policy strt
        public ActionResult CreatePolicy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertUpdatePolicy(Policies policyModel)
        {
            if (policyModel.PolicyID > 0)
            {

                _hrms.Entry(policyModel).State = EntityState.Modified;
            }
            else
            {
                _hrms.Policies.Add(policyModel);
            }
            var rowAffected = 0;
            try
            {
                rowAffected = _hrms.SaveChanges();
            }
            catch (Exception)
            {
                //terminations.LastWorkingDate = DateTime.Now;
               // policyModel.Date = DateTime.Now;
                rowAffected = _hrms.SaveChanges();
            }
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }



        //policy end
        public ActionResult CreateProcedure()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertUpdateProcedure(Procedures procedureModel)
        {
            if (procedureModel.ProcID > 0)
            {

                _hrms.Entry(procedureModel).State = EntityState.Modified;
            }
            else
            {
                _hrms.Procedures.Add(procedureModel);
            }
            var rowAffected = 0;
            try
            {
                rowAffected = _hrms.SaveChanges();
            }
            catch (Exception)
            {
                //terminations.LastWorkingDate = DateTime.Now;
                // policyModel.Date = DateTime.Now;
                rowAffected = _hrms.SaveChanges();
            }
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }

        //Objection Certificate Start

        public ActionResult ObjectionCertificate()
        {
            return View();
        }
        public ActionResult GetAllOC()
        {

            _hrms.Configuration.ProxyCreationEnabled = false;
            var data = _hrms.NonObjectionCertificates.ToList();
            List<object> listOfObject = new List<object>();
            foreach (var item in data)
            {
                var newObject = new
                {
                    OCList = item,
                    Date = item.Date.ToString("dd/MM/yyyy"),
                    Name = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FirstName,
                    Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
                    Department = _hrms.Departments.FirstOrDefault(x => x.Id == item.DepartmentId)?.Name,
                    Application = _hrms.NonObjectionCertificates.FirstOrDefault(x => x.OCID == item.OCID)?.Application,
                    Subject = _hrms.NonObjectionCertificates.FirstOrDefault(x => x.OCID == item.OCID)?.Subject,

                };
                listOfObject.Add(newObject);
            }
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(listOfObject, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DeleteOC(int Id)
        {
            NonObjectionCertificate emp = _hrms.NonObjectionCertificates.Where(x => x.OCID == Id).FirstOrDefault<NonObjectionCertificate>();
            _hrms.NonObjectionCertificates.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }

        public ActionResult LoadOCById(int id)
        {
            var terminationEmployee = _hrms.NonObjectionCertificates.FirstOrDefault(x => x.OCID == id);
            return Json(new
            {
                Termination = terminationEmployee,
                Date = terminationEmployee?.Date.ToString("yyyy-MM-dd"),
            }
            , JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOC(int? Id)
        {
            var Employeess = _hrms.HrmEmployees.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            //var terminationEmployee = _hrms.Terminations.ToList();//.FirstOrDefault(x => x.TerminatinId == Id.Value);
            var model = new Common
            {
                EmployeeList = Employeess,
                DesignationList = Designations,
                DepartmentList = Departments,
               // Termination = terminationEmployee.FirstOrDefault()
            };
            return View(model);
        }



        [HttpPost]
        public ActionResult InsertUpdateOC(NonObjectionCertificate OC)
        {
            if (OC.OCID > 0)
            {

                _hrms.Entry(OC).State = EntityState.Modified;
            }
            else
            {
                _hrms.NonObjectionCertificates.Add(OC);
            }
            var rowAffected = 0;
            try
            {
                rowAffected = _hrms.SaveChanges();
            }
            catch (Exception)
            {
                //terminations.LastWorkingDate = DateTime.Now;
                OC.Date = DateTime.Now;
                rowAffected = _hrms.SaveChanges();
            }
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }


        //Objection Certificate End

        //Special Consideration Start
        public ActionResult SpecialConsiderationIndex()
        {
            return View();
        }

        public ActionResult GetAllSC()
        {

            _hrms.Configuration.ProxyCreationEnabled = false;
            var data = _hrms.SpecialConsidertions.ToList();
            List<object> listOfObject = new List<object>();
            foreach (var item in data)
            {
                var newObject = new
                {
                    SCList = item,
                    Date = item.Date.ToString("dd/MM/yyyy"),
                    Name = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FirstName,
                    Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
                    Department = _hrms.Departments.FirstOrDefault(x => x.Id == item.DepartmentId)?.Name,
                    Request = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Request,
                    Subject = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Subject,


                    //Date = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Date.ToString("dd/MM/yyyy"),
                    //Name = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Name,
                    //Designation = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Department,
                    //Department = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Designation,
                    //Request = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Request,
                    //Subject = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == item.SCID)?.Subject,
                };
                listOfObject.Add(newObject);
            }
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(listOfObject, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteSC(int Id)
        {
            SpecialConsidertion emp = _hrms.SpecialConsidertions.Where(x => x.SCID == Id).FirstOrDefault<SpecialConsidertion>();
            _hrms.SpecialConsidertions.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }

        public ActionResult LoadSCById(int id)
        {
            var SC = _hrms.SpecialConsidertions.FirstOrDefault(x => x.SCID == id);
            return Json(new
            {
                SC = SC,
                Date = SC?.Date.ToString("yyyy-MM-dd"),
                //LastWorkingDate = terminationEmployee?.Date.ToString("yyyy-MM-dd"),
            }
            , JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateSC()
        {
            var Employeess = _hrms.HrmEmployees.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var model = new Common
            {
                EmployeeList = Employeess,
                DesignationList = Designations,
                DepartmentList = Departments,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertUpdateSC(SpecialConsidertion SP)
        {
            if (SP.SCID > 0)
            {

                _hrms.Entry(SP).State = EntityState.Modified;
            }
            else
            {
                _hrms.SpecialConsidertions.Add(SP);
            }
            var rowAffected = 0;
            try
            {
                rowAffected = _hrms.SaveChanges();
            }
            catch (Exception)
            {
                SP.Date = DateTime.Now;
                rowAffected = _hrms.SaveChanges();
            }
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        //Special Consideration end
    }
}