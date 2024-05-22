using HRMS.Models;
using LinqToExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using TalenBAL.BAL;
using static iTextSharp.text.pdf.AcroFields;
using OfficeOpenXml;

namespace HRMS.Controllers
{


    //public class OutOfMemoryException : SystemException;


    public class EmployeeProfileController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        BLMaster ObjBLMaster = new BLMaster();
        BLEmployeeProfile ObjBLRecruitment = new BLEmployeeProfile();
        #region Employee_Profile


        //public ActionResult Index()
        //{
        //    return View();
        //}

       
        public ActionResult Index()
        {
            CheckTerminations();
            CheckTransfers();
            var Allrecord = _hrms.HrmEmployees.ToList();
            return View(Allrecord);
        }
        public void CheckTransfers()
        {
            var hrmEmp = _hrms.HrmEmployees.ToList();
            var Transfer = _hrms.EmployeeTransfers.ToList();

            foreach (var x in Transfer)
            {
                if (x.Active == true)
                {
                    foreach (var y in hrmEmp)
                    {
                        if (x.EmployeeId == y.Id)
                        {
                            y.DepartmentId = (int?)x.ToDepartmentId;
                            _hrms.Entry(y).State = EntityState.Modified;
                            _hrms.SaveChanges();
                        }
                    }
                }
            }



        }
        public void CheckTerminations()
        {
            //var result = _hrms.Terminations.ToList();
            // List<HrmEmployee> hrmEmp = new List<HrmEmployee>();
            //var hrmEmp = (List<HrmEmployee>)Session["hrmEmp"];
            var hrmEmp = _hrms.HrmEmployees.ToList();
            //var Termination = (List<Terminations>)Session["Termination"];
            var Termination = _hrms.Terminations.ToList();
            //var findEmp = hrmEmp.Where(x => x.Id == id).FirstOrDefault();

            foreach (var x in Termination)
            {
                if (x.Active == true && System.DateTime.Now.Date > x.LastWorkingDate.Date )
                {
                    foreach (var y in hrmEmp)
                    {
                        if (x.EmployeeId == y.Id)
                        {
                            y.Active = false;
                            _hrms.Entry(y).State = EntityState.Modified;
                            _hrms.SaveChanges();
                        }
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult EmployeesForm(string id)
        {

            //List<HrmEmployee> hrmEmp = new List<HrmEmployee>();
            //hrmEmp = _hrms.HrmEmployees.ToList();
            //Session["hrmEmp"] = hrmEmp;
            
            //List<Terminations> Termination = new List<Terminations>();
            //Termination = _hrms.Terminations.ToList();
            //Session["Termination"] = Termination;

            int EmpId = Convert.ToInt32(id);

            //CheckTerminations();

            try
            {

           
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;

            CascadingModel model = new CascadingModel();
            foreach (var country in _hrms.Countries)
            {
                model.Countries.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            }

            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastEmployee = _hrms.HrmEmployees.Where(x => x.FirstName !="").ToList().LastOrDefault();
            if (LastEmployee != null && !string.IsNullOrEmpty(LastEmployee.EmployeeCode))

            {
                stringCode = LastEmployee.EmployeeCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastEmployee.EmployeeCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D3"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "Emp" + 1.ToString("D3");
            }
            Common employeeDropDowns = new Common
            {
                //Contact Information
                RegionsList = _hrms.Regions.ToList(),
                CountriesList = _hrms.Countries.ToList<Country>(),
                StatesList = _hrms.States.ToList<State>(),
                CitiesList = _hrms.Cities.ToList<City>(),
                //Position
                EmployeeGroupList = _hrms.EmployeeGroups.ToList<EmployeeGroup>(),
                DesignationList = _hrms.Designations.ToList<Designation>(),
                DepartmentList = _hrms.Departments.ToList<Department>(),
                JobTypeList = _hrms.HrmJobTypes.ToList<HrmJobType>(),
                ShiftMasterList = _hrms.ShiftMasters.ToList<ShiftMaster>(),
                EmployeeList = _hrms.HrmEmployees.ToList<HrmEmployee>(),
                EmployeeCode = stringCode
            };


                return View(employeeDropDowns);

                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EmployeesFormView(int? id)
        {
            try
            {

                //CheckTerminations();
                var stringCode = string.Empty;
                _hrms.Configuration.ProxyCreationEnabled = false;

                CascadingModel model = new CascadingModel();
                foreach (var country in _hrms.Countries)
                {
                    model.Countries.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
                }

                //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
                var LastEmployee = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
                if (LastEmployee != null && !string.IsNullOrEmpty(LastEmployee.EmployeeCode))

                {
                    stringCode = LastEmployee.EmployeeCode.Substring(0, 3);
                    int intCode = Convert.ToInt16(LastEmployee.EmployeeCode.Substring(3));
                    intCode++;
                    var threeDidgit = intCode.ToString("D3"); // = "001"
                    stringCode += threeDidgit;
                }
                else
                {
                    stringCode = "Emp" + 1.ToString("D3");
                }
                Common employeeDropDowns = new Common
                {
                    //Contact Information
                    RegionsList = _hrms.Regions.ToList(),
                    CountriesList = _hrms.Countries.ToList<Country>(),
                    StatesList = _hrms.States.ToList<State>(),
                    CitiesList = _hrms.Cities.ToList<City>(),
                    //Position
                    EmployeeGroupList = _hrms.EmployeeGroups.ToList<EmployeeGroup>(),
                    DesignationList = _hrms.Designations.ToList<Designation>(),
                    DepartmentList = _hrms.Departments.ToList<Department>(),
                    JobTypeList = _hrms.HrmJobTypes.ToList<HrmJobType>(),
                    ShiftMasterList = _hrms.ShiftMasters.ToList<ShiftMaster>(),
                    EmployeeCode = stringCode
                };


                return View(employeeDropDowns);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetAllEmployees1()
        {
            //try
            //{

            //CheckTerminations();
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var dd = _hrms.HrmEmployees.Where(x => x.FirstName != "").FirstOrDefault();
           //var items = _hrms.HrmEmployees.Where(x => x.FirstName != null).ToList();
           // var items = _hrms.HrmEmployees.Select(s =>s).ToList();

          
            var items = _hrms.HrmEmployees.Include(x => x.Department).Include(x => x.Designation).ToList();

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var data = new List<object>();      

            JsonResult json = Json(jss, JsonRequestBehavior.AllowGet);


            json.MaxJsonLength = int.MaxValue;// 999999999;
            serializer.MaxJsonLength = int.MaxValue; // 999999999;




            //var serializer1 = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
            //return new ContentResult()
            //{
            //    Content = serializer.Serialize(json),
            //    ContentType = "application/json",
            //};
            foreach (var item in items)
            {
                var DesignationName = _hrms.Designations.Where(x => x.Id == item.DesignationId).FirstOrDefault();
                var DepartmentName = _hrms.Departments.Where(x => x.Id == item.DepartmentId).FirstOrDefault();
                var obj = new
                {
                    data = item,
                    dteJoiningDate = item.dteJoiningDate?.ToString("dd/MM/yyyy"),
                    DesignationName = DesignationName.Name,
                    DepartmentName = DepartmentName.Name
                };
                data.Add(obj);
              
            }
                var result = JsonConvert.SerializeObject(data, jss);


                return Json(result, JsonRequestBehavior.AllowGet);

            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
           
        }
        //[HttpGet]
        //public ActionResult All()
        //{

        //    var record = ObjBLRecruitment.GetEmployees();

        //    return Json(record, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            //CheckTerminations();
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
               var items = _hrms.HrmEmployees.Include(x => x.Department).Include(x => x.Designation).ToList();
                var data = new List<object>();

                foreach (var item in items)
                {
                    var DesignationName = _hrms.Designations.Where(x => x.Id == item.DesignationId).FirstOrDefault();
                    var DepartmentName = _hrms.Departments.Where(x => x.Id == item.DepartmentId).FirstOrDefault();
                    if (item.FirstName != null)
                    {
                        var obj = new
                        {
                            data = item,
                            dteJoiningDate = item.dteJoiningDate?.ToString("dd/MM/yyyy"),
                            DesignationName = DesignationName.Name,
                            DepartmentName = DepartmentName.Name
                        };
                        data.Add(obj);
                    }
                }

                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

                var result = JsonConvert.SerializeObject(data, jss);

               // var serializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };

                // Perform your serialization  86753090  2147483644  2147483644
               // serializer.Serialize(result);

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                throw ex;
            }

}


        [HttpGet]
        public ActionResult Employees(int? id)
        {

            //CheckTerminations();
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;

            CascadingModel model = new CascadingModel();
            foreach (var country in _hrms.Countries)
            {
                model.Countries.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            }

            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastEmployee = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastEmployee != null && !string.IsNullOrEmpty(LastEmployee.EmployeeCode))

            {
                stringCode = LastEmployee.EmployeeCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastEmployee.EmployeeCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D3"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "Emp" + 1.ToString("D3");
            }
            Common employeeDropDowns = new Common
            {
                //Contact Information
                RegionsList = _hrms.Regions.ToList(),
                CountriesList = _hrms.Countries.ToList<Country>(),
                StatesList = _hrms.States.ToList<State>(),
                CitiesList = _hrms.Cities.ToList<City>(),
                //Position
                EmployeeGroupList = _hrms.EmployeeGroups.ToList<EmployeeGroup>(),
                DesignationList = _hrms.Designations.ToList<Designation>(),
                DepartmentList = _hrms.Departments.ToList<Department>(),
                JobTypeList = _hrms.HrmJobTypes.ToList<HrmJobType>(),
                ShiftMasterList = _hrms.ShiftMasters.ToList<ShiftMaster>(),
                EmployeeCode = stringCode
            };


            return View(employeeDropDowns);
        }

        [HttpPost]
        public ActionResult Caccadeddlfill(int? countryId, int? stateId, int? cityId)
        {
            CascadingModel model = new CascadingModel();
            
            foreach (var country in _hrms.Countries)
            {
                model.Countries.Add(new SelectListItem { Text = country.Name, Value = country.Id.ToString() });
            }

            if (countryId.HasValue)
            {
                var states = (from state in _hrms.States
                              where state.CountryId == countryId.Value
                              select state).ToList();
                foreach (var state in states)
                {
                    model.States.Add(new SelectListItem { Text = state.Name, Value = state.Id.ToString() });
                }

                if (stateId.HasValue)
                {
                    var cities = (from city in _hrms.Cities
                                  where city.StateId == stateId.Value
                                  select city).ToList();
                    foreach (var city in cities)
                    {
                        model.Cities.Add(new SelectListItem { Text = city.Name, Value = city.Id.ToString() });
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult RegionChange(long Id)
        {
            
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
          
           
            var Record = _hrms.Countries.Where(x => x.RegionId == Id).ToList();
            return Json(Record, JsonRequestBehavior.AllowGet);
            //return View(employeeDropDowns);
        }

        [HttpGet]
        public ActionResult CountryChange(long Id)
        {

            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;


            var Record = _hrms.States.Where(x => x.CountryId == Id).ToList();
            return Json(Record, JsonRequestBehavior.AllowGet);
            //return View(employeeDropDowns);
        }

        [HttpGet]
        public ActionResult StateChange(long Id)
        {

            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;


            var Record = _hrms.Cities.Where(x => x.StateId == Id).ToList();
            return Json(Record, JsonRequestBehavior.AllowGet);
            //return View(employeeDropDowns);
        }

        [HttpGet]
        public ActionResult DepartmentChange(long Id)
        {

            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;


            var Record = _hrms.HrmEmployees.Where(x => x.DepartmentId == Id).ToList();
            return Json(Record, JsonRequestBehavior.AllowGet);
            //return View(employeeDropDowns);
        }

        [HttpGet]
        public ActionResult GetEmployeeById(int id)
        {
            try
            {
                var stringCode = string.Empty;
                _hrms.Configuration.ProxyCreationEnabled = false;
                var data = _hrms.HrmEmployees
                       .Where(x => x.Id == id).FirstOrDefault();
                object obj = new
                {
                    data,
                    dteJoiningDate = data.dteJoiningDate?.ToString("yyyy-MM-dd"),
                    VisaIssuanceDate = data.VisaIssuanceDate?.ToString("yyyy-MM-dd"),
                    DateOfBirth = data.DateOfBirth?.ToString("yyyy-MM-dd"),
                    VisaExpiryDate = data.VisaExpiryDate?.ToString("yyyy-MM-dd"),
                    ConfirmationDate = data.ConfirmationDate?.ToString("yyyy-MM-dd"),
                    LabourCardExpiry = data.LabourCardExpiry?.ToString("yyyy-MM-dd"),
                    ContractExpiryDate = data.ContractExpiryDate?.ToString("yyyy-MM-dd"),
                    //ContractExpiryDate = data.ContractExpiryDate?.ToString("dd/MM/yyyy"),
                    IdentityExpiryDate = data.IdentityExpiryDate?.ToString("yyyy-MM-dd"),
                    //IdentityExpiryDate = data.IdentityExpiryDate?.ToString("dd/MM/yyyy"),
                    PassportExpiryDate = data.PassportExpiryDate?.ToString("yyyy-MM-dd")
                    //PassportExpiryDate = data.PassportExpiryDate?.ToString("dd/MM/yyyy")
                };
                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                var result = JsonConvert.SerializeObject(obj, jss);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult InsertUpdateEmployee(HrmEmployee employee, HttpPostedFileBase file1)
        {
            string message = "";
            if (employee.Id > 0)
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var obj = _hrms.HrmEmployees.Where(x => x.Id == employee.Id).FirstOrDefault();
                if (obj != null && !string.IsNullOrEmpty(obj.ProfilePicture) && !obj.ProfilePicture.Equals(employee.ProfilePicture))
                {
                    string path = Path.Combine(Server.MapPath("~/Files/"), obj.ProfilePicture);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not
                        file.Delete();
                }
                if (obj != null)
                    _hrms.Entry(obj).State = EntityState.Detached;
                _hrms.Entry(employee).State = EntityState.Modified;
                message = "Updated Successfully.";
            }
            else
            {
                _hrms.HrmEmployees.Add(employee);
                message = "Add Successfully.";
            }
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, Id = employee.Id, message = message, JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int Id)
        {
            HrmEmployee emp = _hrms.HrmEmployees.Where(x => x.Id == Id).FirstOrDefault<HrmEmployee>();
            _hrms.HrmEmployees.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion
        #region Employee_Information
        [HttpPost]
        public ActionResult DeleteFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            file = Request.Files["file"];
            string FileWithPath = string.Empty;
            string fileName = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var files = fileName.Split('.');
                var FileName = files[0];
                var FileExtenstion = files[1];
                var guid = string.Join("", Guid.NewGuid().ToString().Split('-'));
                fileName = guid + "." + FileExtenstion;
                if (!Directory.Exists(Server.MapPath("~/Files/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Files/"));
                }
                FileWithPath = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(FileWithPath);
                return Json(new { success = true, savedFilePath = fileName, JsonRequestBehavior.AllowGet });

            }
            return Json(new { success = false, savedFilePath = fileName, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ContentResult DownloadFile(string fileName)
        {

            //Build the File Path.
            string path = Server.MapPath("~/Files/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            // return File(bytes, "application/octet-stream", fileName);
            string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Content(base64);
        }
        public ActionResult EmployeeInformation(int? Id)
        {
            var JobTypes = _hrms.HrmJobTypes.ToList();
            var Designations = _hrms.Designations.ToList();
            var common = new Common()
            {
                JobTypeList = JobTypes,
                DesignationList = Designations
            };
            return View(common);
        }
        [HttpPost]
        public ActionResult InsertUpdateEmployeeQualification(IEnumerable<HrmEmployeeEducations> educations)
        {
            if (educations == null)
                return Json(new { success = false, message = "Please Enter Qualification..", JsonRequestBehavior.AllowGet });
            var id = educations.First().EmployeeId;
            var obj = _hrms.HrmEmployeeEducations.Where(x => x.EmployeeId == id).ToList();
            foreach (var education in obj)
            {
                var noUpdateEducation = educations.FirstOrDefault(x => x.Attachment == education.Attachment);
                if (education.Attachment == null || noUpdateEducation != null) continue;
                string path = Path.Combine(Server.MapPath("~/Files/"), education.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            if (obj != null && obj.Count > 0)
                _hrms.HrmEmployeeEducations.RemoveRange(obj);

            _hrms.HrmEmployeeEducations.AddRange(educations);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Qualification Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeExperience(IEnumerable<HrmEmployeementHistories> expereiences)
        {
            if (expereiences == null)
                return Json(new { success = false, message = "Please Enter Experience.", JsonRequestBehavior.AllowGet });
            var id = expereiences.First().EmployeeId;
            var obj = _hrms.HrmEmployeementHistories.Where(x => x.EmployeeId == id).ToList();
            foreach (var experience in obj)
            {
                var noUpdateEducation = expereiences.FirstOrDefault(x => x.Attachment == experience.Attachment);
                if (experience.Attachment == null || noUpdateEducation != null) continue;
                string path = Path.Combine(Server.MapPath("~/Files/"), experience.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            if (obj != null && obj.Count > 0)
                _hrms.HrmEmployeementHistories.RemoveRange(obj);

            _hrms.HrmEmployeementHistories.AddRange(expereiences);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Experience Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeSkill(IEnumerable<HrmSkill> skills)
        {
            if (skills == null)
                return Json(new { success = false, message = "Please Enter skills.", JsonRequestBehavior.AllowGet });
            var id = skills.First().EmployeeId;
            var obj = _hrms.HrmSkills.Where(x => x.EmployeeId == id).ToList();
            if (obj != null && obj.Count > 0)
                _hrms.HrmSkills.RemoveRange(obj);

            _hrms.HrmSkills.AddRange(skills);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Skills Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeHealth(IEnumerable<HrmEmployeeHealths> healths)
        {

            if (healths == null)
                return Json(new { success = false, message = "Please Enter healths.", JsonRequestBehavior.AllowGet });
            var id = healths.First().EmployeeId;
            var obj = _hrms.HrmEmployeeHealths.Where(x => x.EmployeeId == id).ToList();
            foreach (var health in obj)
            {
                var noUpdateEducation = healths.FirstOrDefault(x => x.Attachment == health.Attachment);
                if (health.Attachment == null || noUpdateEducation != null) continue;
                string path = Path.Combine(Server.MapPath("~/Files/"), health.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            if (obj != null && obj.Count > 0)
                _hrms.HrmEmployeeHealths.RemoveRange(obj);

            _hrms.HrmEmployeeHealths.AddRange(healths);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Healths Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeRelation(IEnumerable<HrmEmployeeRelations> relations)
        {

            if (relations == null)
                return Json(new { success = false, message = "Please Enter relations.", JsonRequestBehavior.AllowGet });
            var id = relations.First().EmployeeId;
            var obj = _hrms.HrmEmployeeRelations.Where(x => x.EmployeeId == id).ToList();
            if (obj != null && obj.Count > 0)
                _hrms.HrmEmployeeRelations.RemoveRange(obj);

            _hrms.HrmEmployeeRelations.AddRange(relations);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Relations Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeReference(IEnumerable<HrmEmployeeRefrences> refrences)
        {

            if (refrences == null)
                return Json(new { success = false, message = "Please Enter refrences.", JsonRequestBehavior.AllowGet });
            var id = refrences.First().EmployeeId;
            var obj = _hrms.HrmEmployeeRefrences.Where(x => x.EmployeeId == id).ToList();
            if (obj != null && obj.Count > 0)
                _hrms.HrmEmployeeRefrences.RemoveRange(obj);

            _hrms.HrmEmployeeRefrences.AddRange(refrences);

            if (_hrms.SaveChanges() > 0)
                return Json(new { success = true, message = "Refrences Added Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "There may something wrong.", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult LoadEmployeeInformation(int id)
        {
            var JobTypes = _hrms.HrmJobTypes.Select(x => new { x.Name, x.Id }).ToList();
            var Designation = _hrms.Designations.Select(x => new { x.Name, x.Id }).ToList();
            var educations = _hrms.HrmEmployeeEducations.Where(x => x.EmployeeId == id).ToArray();
            var experiences = _hrms.HrmEmployeementHistories.Where(x => x.EmployeeId == id).ToList();
            var skills = _hrms.HrmSkills.Where(x => x.EmployeeId == id).ToList();
            var healths = _hrms.HrmEmployeeHealths.Where(x => x.EmployeeId == id).ToList();
            var relations = _hrms.HrmEmployeeRelations.Where(x => x.EmployeeId == id).ToList();
            var references = _hrms.HrmEmployeeRefrences.Where(x => x.EmployeeId == id).ToList();
            var visadetail = _hrms.EmployeeVisaDetails.Select(x => new{x.VisaTitle, x.VisaExpiryDate,x.VisaIssuanceDate}).ToList();
            return Json(new
            {
                success = true,
                message = "Success",
                educations = educations,
                experiences = experiences,
                skills = skills,
                healths = healths,
                references = references,
                relations = relations,
                JobTypes = JobTypes,
                Designation = Designation,
                visadetails=visadetail
            }
            , JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Employee_Termination
        public ActionResult Terminations()
        {
            return View();
        }
        public ActionResult GetAllTerminations()
        {
            
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var data = _hrms.Terminations.Include(x => x.Department).Include(x => x.Designation).Include(x => x.HrmEmployee).Include(X => X.TerminationBy).ToList();
            var data = _hrms.Terminations.ToList();
            List<object> listOfObject = new List<object>();
            foreach (var item in data)
            {
                var newObject = new
                {
                    Termination = item,
                    Date=item.Date.ToString("dd/MM/yyyy"),
                    LastWorkingDate=item.LastWorkingDate.ToString("dd/MM/yyyy"),
                    TerminatedEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FirstName,
                    TerminatedByEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.TerminationBy)?.FirstName,
                    Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
                    Department = _hrms.Departments.FirstOrDefault(x => x.Id == item.DepartmentId)?.Name,
                };
                listOfObject.Add(newObject);
            }
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(listOfObject, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Termination(int? Id)
        {
            var Employeess = _hrms.HrmEmployees.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var terminationEmployee = _hrms.Terminations.ToList();//.FirstOrDefault(x => x.TerminatinId == Id.Value);
            var model = new Common
            {
                EmployeeList = Employeess,
                DesignationList = Designations,
                DepartmentList = Departments,
                Termination = terminationEmployee.FirstOrDefault()
            };
            return View(model);
        }

        public ActionResult LoadTerminationEmployeeChange(long JobId)
        {

            var record = ObjBLRecruitment.GetDataByEmployee_Sepration(JobId);

            return Json(record, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadTerminationEmployee(int id)
        {
            var terminationEmployee = _hrms.Terminations.FirstOrDefault(x => x.TerminatinId == id);


            var DeptName = _hrms.Departments.FirstOrDefault(x => x.Id == terminationEmployee.DepartmentId).Name;

            var Designation = _hrms.Designations.FirstOrDefault(x => x.Id == terminationEmployee.DesignationId).Name;


            return Json(new
            {
                Termination = terminationEmployee,
                Date = terminationEmployee?.Date.ToString("yyyy-MM-dd"),
                LastWorkingDate = terminationEmployee?.LastWorkingDate.ToString("yyyy-MM-dd"),
                DeptName = DeptName,
                Designation = Designation
            }
            , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertUpdateEmployeeTermination(Terminations terminations)
        {
            if (terminations.TerminatinId > 0)
            {

                _hrms.Entry(terminations).State = EntityState.Modified;
            }
            else
            {
                _hrms.Terminations.Add(terminations);
            }
            var rowAffected = 0;
            try
            {
                rowAffected = _hrms.SaveChanges();
            }
            catch (Exception)
            {
                terminations.LastWorkingDate = DateTime.Now;
                terminations.Date = DateTime.Now;
                rowAffected = _hrms.SaveChanges();
            }
             if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        public ActionResult DeleteTermination(int? Id)
        {
            Terminations emp = _hrms.Terminations.Where(x => x.TerminatinId == Id).FirstOrDefault<Terminations>();
            _hrms.Terminations.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion
        #region Employee_Transfers
        public ActionResult Transfers()
        {
            return View();
        }
        public ActionResult GetAllTransfers()
        {
            var data = _hrms.EmployeeTransfers.ToList();
            List<object> listOfObject = new List<object>();
            foreach (var item in data)
            {
                var newObject = new
                {
                    Transfer = item,
                    TransferDate = item.TrasnferDate.ToString("dd/MM/yyyy"),
                    TransferEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FirstName,
                    TransferByEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.TrasnferedBy)?.FirstName,
                    //DesignationName = item.Designation.ToString(), //_hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.Designation)?.FirstName,
                    //DepartmenName =item.Department.ToString(),     //_hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.)?.FirstName,
                    Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
                    NewDesignation = _hrms.Designations.FirstOrDefault(x => x.Id == item.NewDesignationId)?.Name,
                    FromDepartment = _hrms.Departments.FirstOrDefault(x => x.Id == item.FromDepartmentId)?.Name,
                    ToDepartment = _hrms.Departments.FirstOrDefault(x => x.Id == item.ToDepartmentId)?.Name,
                };
                listOfObject.Add(newObject);
            }
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(listOfObject, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Transfer(int? Id)
        {
            var Employeess = _hrms.HrmEmployees.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var terminationEmployee = _hrms.Terminations.ToList();//.FirstOrDefault(x => x.TerminatinId == Id.Value);
            var model = new Common
            {
                EmployeeList = Employeess,
                DesignationList = Designations,
                DepartmentList = Departments,
                Termination = terminationEmployee.FirstOrDefault()
            };
            return View(model);
        }
        public ActionResult LoadTransferEmployee(int id)
        {
            var transferEmployee = _hrms.EmployeeTransfers.FirstOrDefault(x => x.TransferId == id);

            var DesignationName = _hrms.Designations.Where(x => x.Id == transferEmployee.DesignationId).FirstOrDefault();

            var FromDepartmentName = _hrms.Departments.Where(x => x.Id == transferEmployee.FromDepartmentId).FirstOrDefault();
            

            return Json(new
            {
                Transfer = transferEmployee,
                TrasnferDate = transferEmployee?.TrasnferDate.ToString("yyyy-MM-dd"),
                DesignationName = DesignationName.Name,
                FromDepartmentName = FromDepartmentName.Name
            }
            , JsonRequestBehavior.AllowGet);
        }

        public ActionResult JobTitleChange(long JobId)
        {

            var record = ObjBLRecruitment.GetDataByJobChange(JobId);

            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertUpdateEmployeeTransfer(EmployeeTransfers transfers)
        {
            if (transfers.TransferId > 0)
            {
                _hrms.Entry(transfers).State = EntityState.Modified;
            }
            else
            {
                _hrms.EmployeeTransfers.Add(transfers);
            }

            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        public ActionResult DeleteTransfer(int Id)
        {
            EmployeeTransfers emp = _hrms.EmployeeTransfers.Where(x => x.TransferId == Id).FirstOrDefault<EmployeeTransfers>();
            _hrms.EmployeeTransfers.Remove(emp);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion

        public string AutoEmployeeCode(string stringCode)
        {
            var str = stringCode.Substring(0, 3);

            int intCode = Convert.ToInt32(stringCode.Substring(3));

            intCode++;

            var threeDidgit = intCode.ToString("000"); 

            str += threeDidgit;
          
            return str;
        }


        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase FileUpload)
        {
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    try
                    {
                        var LastEmp = _hrms.HrmEmployees.ToList().LastOrDefault();

                        string stringCode = "";

                        if (LastEmp != null)
                        {
                            stringCode = LastEmp.EmployeeCode.Substring(0, 3);

                            int intCode = Convert.ToInt32(LastEmp.EmployeeCode.Substring(3));

                            intCode++;

                            var threeDidgit = intCode.ToString("000");

                            stringCode += threeDidgit;
                        }
                        else
                        {
                            stringCode = "Emp" + 1.ToString("000");
                        }

                        bool firstTimeCode = true;


                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        using (var pac = new ExcelPackage(FileUpload.InputStream))
                        {
                            DataTable table = new DataTable();

                            if (pac.Workbook.Worksheets.Count > 0)
                            {
                                using (ExcelWorksheet workSheet = pac.Workbook.Worksheets.First())
                                {
                                    int Cols = workSheet.Dimension.End.Column;

                                    int Rows = workSheet.Dimension.End.Row;

                                    int rowIndex = 1;

                                    for (int c = 1; c <= Cols; c++)
                                    {
                                        table.Columns.Add(workSheet.Cells[rowIndex, c].Text);
                                    }

                                    rowIndex = 2;

                                    for (int r = rowIndex; r <= Rows; r++)
                                    {
                                        DataRow dr = table.NewRow();

                                        for (int c = 1; c <= Cols; c++)
                                        {
                                            dr[c - 1] = workSheet.Cells[r, c].Value;
                                        }

                                        table.Rows.Add(dr);
                                    }

                                    List<HrmEmployee> list = new List<HrmEmployee>();

                                    for (int i = 0; i < table.Rows.Count; i++)
                                    {
                                        HrmEmployee TU = new HrmEmployee();

                                        if (table.Rows[i]["TitleId"] == DBNull.Value || table.Rows[i]["TitleId"].ToString() == "NULL")
                                        {
                                            TU.TitleId = null;
                                        }
                                        else
                                        {
                                            TU.TitleId = Convert.ToInt32(table.Rows[i]["TitleId"]);
                                        }

                                        if (table.Rows[i]["FirstName"] == DBNull.Value || table.Rows[i]["FirstName"].ToString() == "NULL")
                                        {
                                            TU.FirstName = null;
                                        }
                                        else
                                        {
                                            TU.FirstName = table.Rows[i]["FirstName"].ToString();
                                        }

                                        if (table.Rows[i]["Middlename"] == DBNull.Value || table.Rows[i]["Middlename"].ToString() == "NULL")
                                        {
                                            TU.Middlename = null;
                                        }
                                        else
                                        {
                                            TU.Middlename = table.Rows[i]["Middlename"].ToString();
                                        }

                                        if (table.Rows[i]["LastName"] == DBNull.Value || table.Rows[i]["LastName"].ToString() == "NULL")
                                        {
                                            TU.LastName = null;
                                        }
                                        else
                                        {
                                            TU.LastName = table.Rows[i]["LastName"].ToString();
                                        }

                                        if(firstTimeCode == true)
                                        {
                                            firstTimeCode = false;

                                            TU.EmployeeCode = stringCode;
                                        }
                                        else
                                        {
                                            stringCode = AutoEmployeeCode(stringCode);

                                            TU.EmployeeCode = stringCode;
                                        }

                                        if (table.Rows[i]["Gender"] == DBNull.Value || table.Rows[i]["Gender"].ToString() == "NULL")
                                        {
                                            TU.Gender = null;
                                        }
                                        else
                                        {
                                            TU.Gender = table.Rows[i]["Gender"].ToString();
                                        }

                                        if (table.Rows[i]["FatherHusbandName"] == DBNull.Value || table.Rows[i]["FatherHusbandName"].ToString() == "NULL")
                                        {
                                            TU.FatherHusbandName = null;
                                        }
                                        else
                                        {
                                            TU.FatherHusbandName = table.Rows[i]["FatherHusbandName"].ToString();
                                        }

                                        if (table.Rows[i]["DateOfBirth"] == DBNull.Value || table.Rows[i]["DateOfBirth"].ToString() == "NULL")
                                        {
                                            TU.DateOfBirth = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["DateOfBirth"].ToString(), out parsedResult))
                                            {
                                                TU.DateOfBirth = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["DateOfBirth"]));
                                            }
                                            else
                                            {
                                                TU.DateOfBirth = Convert.ToDateTime(table.Rows[i]["DateOfBirth"].ToString());
                                            }
                                        }

                                        if (table.Rows[i]["IdentityCardNo"] == DBNull.Value || table.Rows[i]["IdentityCardNo"].ToString() == "NULL")
                                        {
                                            TU.IdentityCardNo = null;
                                        }
                                        else
                                        {
                                            TU.IdentityCardNo = table.Rows[i]["IdentityCardNo"].ToString();
                                        }

                                        if (table.Rows[i]["ReportToPerson"] == DBNull.Value || table.Rows[i]["ReportToPerson"].ToString() == "NULL")
                                        {
                                            TU.ReportToPerson = null;
                                        }
                                        else
                                        {
                                            TU.ReportToPerson = Convert.ToInt32(table.Rows[i]["ReportToPerson"]);
                                        }

                                        if (table.Rows[i]["IdentityExpiryDate"] == DBNull.Value || table.Rows[i]["IdentityExpiryDate"].ToString() == "NULL")
                                        {
                                            TU.IdentityExpiryDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["IdentityExpiryDate"].ToString(), out parsedResult))
                                            {
                                                TU.IdentityExpiryDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["IdentityExpiryDate"]));
                                            }
                                            else
                                            {
                                                TU.IdentityExpiryDate = Convert.ToDateTime(table.Rows[i]["IdentityExpiryDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["ReligionId"] == DBNull.Value || table.Rows[i]["ReligionId"].ToString() == "NULL")
                                        {
                                            TU.ReligionId = null;
                                        }
                                        else
                                        {
                                            TU.ReligionId = Convert.ToInt32(table.Rows[i]["ReligionId"]);
                                        }

                                        if (table.Rows[i]["MaritalStatus"] == DBNull.Value || table.Rows[i]["MaritalStatus"].ToString() == "NULL")
                                        {
                                            TU.MaritalStatus = null;
                                        }
                                        else
                                        {
                                            TU.MaritalStatus = table.Rows[i]["MaritalStatus"].ToString();
                                        }

                                        if (table.Rows[i]["Dependants"] == DBNull.Value || table.Rows[i]["Dependants"].ToString() == "NULL")
                                        {
                                            TU.Dependants = null;
                                        }
                                        else
                                        {
                                            TU.Dependants = Convert.ToInt32(table.Rows[i]["Dependants"]);
                                        }

                                        if (table.Rows[i]["NationalCountryId"] == DBNull.Value || table.Rows[i]["NationalCountryId"].ToString() == "NULL")
                                        {
                                            TU.NationalCountryId = null;
                                        }
                                        else
                                        {
                                            TU.NationalCountryId = Convert.ToInt32(table.Rows[i]["NationalCountryId"]);
                                        }

                                        if (table.Rows[i]["EthnicityId"] == DBNull.Value || table.Rows[i]["EthnicityId"].ToString() == "NULL")
                                        {
                                            TU.EthnicityId = null;
                                        }
                                        else
                                        {
                                            TU.EthnicityId = Convert.ToInt32(table.Rows[i]["EthnicityId"]);
                                        }

                                        if (table.Rows[i]["BloodGroup"] == DBNull.Value || table.Rows[i]["BloodGroup"].ToString() == "NULL")
                                        {
                                            TU.BloodGroup = null;
                                        }
                                        else
                                        {
                                            TU.BloodGroup = table.Rows[i]["BloodGroup"].ToString();
                                        }

                                        if (table.Rows[i]["HrmLanguageId"] == DBNull.Value || table.Rows[i]["HrmLanguageId"].ToString() == "NULL")
                                        {
                                            TU.HrmLanguageId = null;
                                        }
                                        else
                                        {
                                            TU.HrmLanguageId = Convert.ToInt32(table.Rows[i]["HrmLanguageId"]);
                                        }

                                        if (table.Rows[i]["DisabilitiesId"] == DBNull.Value || table.Rows[i]["DisabilitiesId"].ToString() == "NULL")
                                        {
                                            TU.DisabilitiesId = null;
                                        }
                                        else
                                        {
                                            TU.DisabilitiesId = Convert.ToInt32(table.Rows[i]["DisabilitiesId"]);
                                        }

                                        if (table.Rows[i]["EmployeeType"] == DBNull.Value || table.Rows[i]["EmployeeType"].ToString() == "NULL")
                                        {
                                            TU.EmployeeType = null;
                                        }
                                        else
                                        {
                                            TU.EmployeeType = table.Rows[i]["EmployeeType"].ToString();
                                        }

                                        if (table.Rows[i]["EmployeeGroupId"] == DBNull.Value || table.Rows[i]["EmployeeGroupId"].ToString() == "NULL")
                                        {
                                            TU.EmployeeGroupId = null;
                                        }
                                        else
                                        {
                                            TU.EmployeeGroupId = Convert.ToInt32(table.Rows[i]["EmployeeGroupId"]);
                                        }

                                        if (table.Rows[i]["HrmLocationOficeId"] == DBNull.Value || table.Rows[i]["HrmLocationOficeId"].ToString() == "NULL")
                                        {
                                            TU.HrmLocationOficeId = null;
                                        }
                                        else
                                        {
                                            TU.HrmLocationOficeId = Convert.ToInt32(table.Rows[i]["HrmLocationOficeId"]);
                                        }

                                        if (table.Rows[i]["DesignationId"] == DBNull.Value || table.Rows[i]["DesignationId"].ToString() == "NULL")
                                        {
                                            TU.DesignationId = null;
                                        }
                                        else
                                        {
                                            TU.DesignationId = Convert.ToInt32(table.Rows[i]["DesignationId"]);
                                        }

                                        if (table.Rows[i]["ReportToId"] == DBNull.Value || table.Rows[i]["ReportToId"].ToString() == "NULL")
                                        {
                                            TU.ReportToId = null;
                                        }
                                        else
                                        {
                                            TU.ReportToId = Convert.ToInt32(table.Rows[i]["ReportToId"]);
                                        }

                                        if (table.Rows[i]["DepartmentId"] == DBNull.Value || table.Rows[i]["DepartmentId"].ToString() == "NULL")
                                        {
                                            TU.DepartmentId = null;
                                        }
                                        else
                                        {
                                            TU.DepartmentId = Convert.ToInt32(table.Rows[i]["DepartmentId"]);
                                        }

                                        if (table.Rows[i]["SubDepartmentId"] == DBNull.Value || table.Rows[i]["SubDepartmentId"].ToString() == "NULL")
                                        {
                                            TU.SubDepartmentId = null;
                                        }
                                        else
                                        {
                                            TU.SubDepartmentId = Convert.ToInt32(table.Rows[i]["SubDepartmentId"]);
                                        }

                                        if (table.Rows[i]["HrmGradeId"] == DBNull.Value || table.Rows[i]["HrmGradeId"].ToString() == "NULL")
                                        {
                                            TU.HrmGradeId = null;
                                        }
                                        else
                                        {
                                            TU.HrmGradeId = Convert.ToInt32(table.Rows[i]["HrmGradeId"]);
                                        }

                                        if (table.Rows[i]["MachineId"] == DBNull.Value || table.Rows[i]["MachineId"].ToString() == "NULL")
                                        {
                                            TU.MachineId = null;
                                        }
                                        else
                                        {
                                            TU.MachineId = table.Rows[i]["MachineId"].ToString();
                                        }

                                        if (table.Rows[i]["dteJoiningDate"] == DBNull.Value || table.Rows[i]["dteJoiningDate"].ToString() == "NULL")
                                        {
                                            TU.dteJoiningDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["dteJoiningDate"].ToString(), out parsedResult))
                                            {
                                                TU.dteJoiningDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["dteJoiningDate"]));
                                            }
                                            else
                                            {
                                                TU.dteJoiningDate = Convert.ToDateTime(table.Rows[i]["dteJoiningDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["ProbationPeriod"] == DBNull.Value || table.Rows[i]["ProbationPeriod"].ToString() == "NULL")
                                        {
                                            TU.ProbationPeriod = null;
                                        }
                                        else
                                        {
                                            TU.ProbationPeriod = Convert.ToInt32(table.Rows[i]["ProbationPeriod"]);
                                        }

                                        if (table.Rows[i]["ConfirmationDate"] == DBNull.Value || table.Rows[i]["ConfirmationDate"].ToString() == "NULL")
                                        {
                                            TU.ConfirmationDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["ConfirmationDate"].ToString(), out parsedResult))
                                            {
                                                TU.ConfirmationDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["ConfirmationDate"]));
                                            }
                                            else
                                            {
                                                TU.ConfirmationDate = Convert.ToDateTime(table.Rows[i]["ConfirmationDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["ContractExpiryDate"] == DBNull.Value || table.Rows[i]["ContractExpiryDate"].ToString() == "NULL")
                                        {
                                            TU.ContractExpiryDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["ContractExpiryDate"].ToString(), out parsedResult))
                                            {
                                                TU.ContractExpiryDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["ContractExpiryDate"]));
                                            }
                                            else
                                            {
                                                TU.ContractExpiryDate = Convert.ToDateTime(table.Rows[i]["ContractExpiryDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["BasicSalary"] == DBNull.Value || table.Rows[i]["BasicSalary"].ToString() == "NULL")
                                        {
                                            TU.BasicSalary = null;
                                        }
                                        else
                                        {
                                            TU.BasicSalary = Convert.ToDouble(table.Rows[i]["BasicSalary"]);
                                        }

                                        if (table.Rows[i]["ContacNo"] == DBNull.Value || table.Rows[i]["ContacNo"].ToString() == "NULL")
                                        {
                                            TU.ContacNo = null;
                                        }
                                        else
                                        {
                                            TU.ContacNo = table.Rows[i]["ContacNo"].ToString();
                                        }

                                        if (table.Rows[i]["Email"] == DBNull.Value || table.Rows[i]["Email"].ToString() == "NULL")
                                        {
                                            TU.Email = null;
                                        }
                                        else
                                        {
                                            TU.Email = table.Rows[i]["Email"].ToString();
                                        }

                                        if (table.Rows[i]["LivingCountryId"] == DBNull.Value || table.Rows[i]["LivingCountryId"].ToString() == "NULL")
                                        {
                                            TU.LivingCountryId = null;
                                        }
                                        else
                                        {
                                            TU.LivingCountryId = Convert.ToInt32(table.Rows[i]["LivingCountryId"]);
                                        }

                                        if (table.Rows[i]["StateId"] == DBNull.Value || table.Rows[i]["StateId"].ToString() == "NULL")
                                        {
                                            TU.StateId = null;
                                        }
                                        else
                                        {
                                            TU.StateId = Convert.ToInt32(table.Rows[i]["StateId"]);
                                        }

                                        if (table.Rows[i]["CityId"] == DBNull.Value || table.Rows[i]["CityId"].ToString() == "NULL")
                                        {
                                            TU.CityId = null;
                                        }
                                        else
                                        {
                                            TU.CityId = Convert.ToInt32(table.Rows[i]["CityId"]);
                                        }

                                        if (table.Rows[i]["ZipCode"] == DBNull.Value || table.Rows[i]["ZipCode"].ToString() == "NULL")
                                        {
                                            TU.ZipCode = null;
                                        }
                                        else
                                        {
                                            TU.ZipCode = table.Rows[i]["ZipCode"].ToString();
                                        }

                                        if (table.Rows[i]["CurrentAddress"] == DBNull.Value || table.Rows[i]["CurrentAddress"].ToString() == "NULL")
                                        {
                                            TU.CurrentAddress = null;
                                        }
                                        else
                                        {
                                            TU.CurrentAddress = table.Rows[i]["CurrentAddress"].ToString();
                                        }

                                        if (table.Rows[i]["ProfilePicture"] == DBNull.Value || table.Rows[i]["ProfilePicture"].ToString() == "NULL")
                                        {
                                            TU.ProfilePicture = null;
                                        }
                                        else
                                        {
                                            TU.ProfilePicture = table.Rows[i]["ProfilePicture"].ToString();
                                        }

                                        if (table.Rows[i]["CurrentCountryId"] == DBNull.Value || table.Rows[i]["CurrentCountryId"].ToString() == "NULL")
                                        {
                                            TU.CurrentCountryId = null;
                                        }
                                        else
                                        {
                                            TU.CurrentCountryId = Convert.ToInt32(table.Rows[i]["CurrentCountryId"]);
                                        }

                                        if (table.Rows[i]["CurrentStateId"] == DBNull.Value || table.Rows[i]["CurrentStateId"].ToString() == "NULL")
                                        {
                                            TU.CurrentStateId = null;
                                        }
                                        else
                                        {
                                            TU.CurrentStateId = Convert.ToInt32(table.Rows[i]["CurrentStateId"]);
                                        }

                                        if (table.Rows[i]["CurrentCityId"] == DBNull.Value || table.Rows[i]["CurrentCityId"].ToString() == "NULL")
                                        {
                                            TU.CurrentCityId = null;
                                        }
                                        else
                                        {
                                            TU.CurrentCityId = Convert.ToInt32(table.Rows[i]["CurrentCityId"]);
                                        }

                                        if (table.Rows[i]["CurrentZipCode"] == DBNull.Value || table.Rows[i]["CurrentZipCode"].ToString() == "NULL")
                                        {
                                            TU.CurrentZipCode = null;
                                        }
                                        else
                                        {
                                            TU.CurrentZipCode = table.Rows[i]["CurrentZipCode"].ToString();
                                        }

                                        if (table.Rows[i]["UserName"] == DBNull.Value || table.Rows[i]["UserName"].ToString() == "NULL")
                                        {
                                            TU.UserName = null;
                                        }
                                        else
                                        {
                                            TU.UserName = table.Rows[i]["UserName"].ToString();
                                        }

                                        if (table.Rows[i]["Password"] == DBNull.Value || table.Rows[i]["Password"].ToString() == "NULL")
                                        {
                                            TU.Password = null;
                                        }
                                        else
                                        {
                                            TU.Password = table.Rows[i]["Password"].ToString();
                                        }

                                        if (table.Rows[i]["IswebloginAllowed"] == DBNull.Value || table.Rows[i]["IswebloginAllowed"].ToString() == "NULL")
                                        {
                                            TU.IswebloginAllowed = null;
                                        }
                                        else
                                        {
                                            if (table.Rows[i]["IswebloginAllowed"].ToString() == "1" || table.Rows[i]["IswebloginAllowed"].ToString() == "True")
                                            {
                                                TU.IswebloginAllowed = true;
                                            }
                                            else
                                            {
                                                TU.IswebloginAllowed = false;
                                            }
                                        }

                                        if (table.Rows[i]["IsDeleted"] == DBNull.Value || table.Rows[i]["IsDeleted"].ToString() == "NULL")
                                        {
                                            TU.IsDeleted = null;
                                        }
                                        else
                                        {
                                            if (table.Rows[i]["IsDeleted"].ToString() == "1" || table.Rows[i]["IsDeleted"].ToString() == "True")
                                            {
                                                TU.IsDeleted = true;
                                            }
                                            else
                                            {
                                                TU.IsDeleted = false;
                                            }
                                        }

                                        if (table.Rows[i]["SoftRoleinformation"].ToString() == "NULL" || table.Rows[i]["SoftRoleinformation"] == DBNull.Value)
                                        {
                                            TU.SoftRoleinformation = null;
                                        }
                                        else
                                        {
                                            TU.SoftRoleinformation = table.Rows[i]["SoftRoleinformation"].ToString();
                                        }

                                        if (table.Rows[i]["ApplicationUserId"].ToString() == "NULL" || table.Rows[i]["ApplicationUserId"] == DBNull.Value)
                                        {
                                            TU.ApplicationUserId = null;
                                        }
                                        else
                                        {
                                            TU.ApplicationUserId = table.Rows[i]["ApplicationUserId"].ToString();
                                        }

                                        if (table.Rows[i]["CostCenterId"] == DBNull.Value || table.Rows[i]["CostCenterId"].ToString() == "NULL")
                                        {
                                            TU.CostCenterId = null;
                                        }
                                        else
                                        {
                                            TU.CostCenterId = Convert.ToInt32(table.Rows[i]["CostCenterId"]);
                                        }

                                        if (table.Rows[i]["Active"] == DBNull.Value || table.Rows[i]["Active"].ToString() == "NULL")
                                        {
                                            TU.Active = null;
                                        }
                                        else
                                        {
                                            if (table.Rows[i]["Active"].ToString() == "1" || table.Rows[i]["Active"].ToString() == "True")
                                            {
                                                TU.Active = true;
                                            }
                                            else
                                            {
                                                TU.Active = false;
                                            }
                                        }

                                        if (table.Rows[i]["Country_Id"] == DBNull.Value || table.Rows[i]["Country_Id"].ToString() == "NULL")
                                        {
                                            TU.Country_Id = null;
                                        }
                                        else
                                        {
                                            TU.Country_Id = Convert.ToInt32(table.Rows[i]["Country_Id"]);
                                        }

                                        if (table.Rows[i]["PassportExpiryDate"] == DBNull.Value || table.Rows[i]["PassportExpiryDate"].ToString() == "NULL")
                                        {
                                            TU.PassportExpiryDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["PassportExpiryDate"].ToString(), out parsedResult))
                                            {
                                                TU.PassportExpiryDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["PassportExpiryDate"]));
                                            }
                                            else
                                            {
                                                TU.PassportExpiryDate = Convert.ToDateTime(table.Rows[i]["PassportExpiryDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["BiometricCode"] == DBNull.Value || table.Rows[i]["BiometricCode"].ToString() == "NULL")
                                        {
                                            TU.BiometricCode = null;
                                        }
                                        else
                                        {
                                            TU.BiometricCode = table.Rows[i]["BiometricCode"].ToString();
                                        }

                                        if (table.Rows[i]["Title"] == DBNull.Value || table.Rows[i]["Title"].ToString() == "NULL")
                                        {
                                            TU.Title = null;
                                        }
                                        else
                                        {
                                            TU.Title = table.Rows[i]["Title"].ToString();
                                        }

                                        if (table.Rows[i]["Passport"] == DBNull.Value || table.Rows[i]["Passport"].ToString() == "NULL")
                                        {
                                            TU.Passport = null;
                                        }
                                        else
                                        {
                                            TU.Passport = table.Rows[i]["Passport"].ToString();
                                        }

                                        if (table.Rows[i]["DrivingLicence"] == DBNull.Value || table.Rows[i]["DrivingLicence"].ToString() == "NULL")
                                        {
                                            TU.DrivingLicence = null;
                                        }
                                        else
                                        {
                                            TU.DrivingLicence = table.Rows[i]["DrivingLicence"].ToString();
                                        }

                                        if (table.Rows[i]["BirthCountryId"] == DBNull.Value || table.Rows[i]["BirthCountryId"].ToString() == "NULL")
                                        {
                                            TU.BirthCountryId = null;
                                        }
                                        else
                                        {
                                            TU.BirthCountryId = Convert.ToInt32(table.Rows[i]["BirthCountryId"]);
                                        }

                                        if (table.Rows[i]["HrmLanguage"] == DBNull.Value || table.Rows[i]["HrmLanguage"].ToString() == "NULL")
                                        {
                                            TU.HrmLanguage = null;
                                        }
                                        else
                                        {
                                            TU.HrmLanguage = table.Rows[i]["HrmLanguage"].ToString();
                                        }

                                        if (table.Rows[i]["ProbationType"] == DBNull.Value || table.Rows[i]["ProbationType"].ToString() == "NULL")
                                        {
                                            TU.ProbationType = null;
                                        }
                                        else
                                        {
                                            TU.ProbationType = table.Rows[i]["ProbationType"].ToString();
                                        }

                                        if (table.Rows[i]["OfficialNo"] == DBNull.Value || table.Rows[i]["OfficialNo"].ToString() == "NULL")
                                        {
                                            TU.OfficialNo = null;
                                        }
                                        else
                                        {
                                            TU.OfficialNo = table.Rows[i]["OfficialNo"].ToString();
                                        }

                                        if (table.Rows[i]["OfficialEmail"] == DBNull.Value || table.Rows[i]["OfficialEmail"].ToString() == "NULL")
                                        {
                                            TU.OfficialEmail = null;
                                        }
                                        else
                                        {
                                            TU.OfficialEmail = table.Rows[i]["OfficialEmail"].ToString();
                                        }

                                        if (table.Rows[i]["RegionId"] == DBNull.Value || table.Rows[i]["RegionId"].ToString() == "NULL")
                                        {
                                            TU.RegionId = null;
                                        }
                                        else
                                        {
                                            TU.RegionId = Convert.ToInt32(table.Rows[i]["RegionId"]);
                                        }

                                        if (table.Rows[i]["PermnantAddress"] == DBNull.Value || table.Rows[i]["PermnantAddress"].ToString() == "NULL")
                                        {
                                            TU.PermnantAddress = null;
                                        }
                                        else
                                        {
                                            TU.PermnantAddress = table.Rows[i]["PermnantAddress"].ToString();
                                        }

                                        if (table.Rows[i]["BankName"] == DBNull.Value || table.Rows[i]["BankName"].ToString() == "NULL")
                                        {
                                            TU.BankName = null;
                                        }
                                        else
                                        {
                                            TU.BankName = table.Rows[i]["BankName"].ToString();
                                        }

                                        if (table.Rows[i]["BranchCode"] == DBNull.Value || table.Rows[i]["BranchCode"].ToString() == "NULL")
                                        {
                                            TU.BranchCode = null;
                                        }
                                        else
                                        {
                                            TU.BranchCode = table.Rows[i]["BranchCode"].ToString();
                                        }

                                        if (table.Rows[i]["BranchName"] == DBNull.Value || table.Rows[i]["BranchName"].ToString() == "NULL")
                                        {
                                            TU.BranchName = null;
                                        }
                                        else
                                        {
                                            TU.BranchName = table.Rows[i]["BranchName"].ToString();
                                        }

                                        if (table.Rows[i]["AccountTitle"] == DBNull.Value || table.Rows[i]["AccountTitle"].ToString() == "NULL")
                                        {
                                            TU.AccountTitle = null;
                                        }
                                        else
                                        {
                                            TU.AccountTitle = table.Rows[i]["AccountTitle"].ToString();
                                        }

                                        if (table.Rows[i]["AccountNumber"] == DBNull.Value || table.Rows[i]["AccountNumber"].ToString() == "NULL")
                                        {
                                            TU.AccountNumber = null;
                                        }
                                        else
                                        {
                                            TU.AccountNumber = table.Rows[i]["AccountNumber"].ToString();
                                        }

                                        if (table.Rows[i]["AccountType"] == DBNull.Value || table.Rows[i]["AccountType"].ToString() == "NULL")
                                        {
                                            TU.AccountType = null;
                                        }
                                        else
                                        {
                                            TU.AccountType = table.Rows[i]["AccountType"].ToString();
                                        }

                                        if (table.Rows[i]["ShiftId"] == DBNull.Value || table.Rows[i]["ShiftId"].ToString() == "NULL")
                                        {
                                            TU.ShiftId = null;
                                        }
                                        else
                                        {
                                            TU.ShiftId = Convert.ToInt64(table.Rows[i]["ShiftId"]);
                                        }

                                        if (table.Rows[i]["VisaTitle"] == DBNull.Value || table.Rows[i]["VisaTitle"].ToString() == "NULL")
                                        {
                                            TU.VisaTitle = null;
                                        }
                                        else
                                        {
                                            TU.VisaTitle = table.Rows[i]["VisaTitle"].ToString();
                                        }

                                        if (table.Rows[i]["VisaNumber"] == DBNull.Value || table.Rows[i]["VisaNumber"].ToString() == "NULL")
                                        {
                                            TU.VisaNumber = null;
                                        }
                                        else
                                        {
                                            TU.VisaNumber = Convert.ToInt64(table.Rows[i]["VisaNumber"]);
                                        }

                                        if (table.Rows[i]["VisaIssuanceDate"] == DBNull.Value || table.Rows[i]["VisaIssuanceDate"].ToString() == "NULL")
                                        {
                                            TU.VisaIssuanceDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["VisaIssuanceDate"].ToString(), out parsedResult))
                                            {
                                                TU.VisaIssuanceDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["VisaIssuanceDate"]));
                                            }
                                            else
                                            {
                                                TU.VisaIssuanceDate = Convert.ToDateTime(table.Rows[i]["VisaIssuanceDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["VisaExpiryDate"] == DBNull.Value || table.Rows[i]["VisaExpiryDate"].ToString() == "NULL")
                                        {
                                            TU.VisaExpiryDate = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["VisaExpiryDate"].ToString(), out parsedResult))
                                            {
                                                TU.VisaExpiryDate = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["VisaExpiryDate"]));
                                            }
                                            else
                                            {
                                                TU.VisaExpiryDate = Convert.ToDateTime(table.Rows[i]["VisaExpiryDate"]);
                                            }
                                        }

                                        if (table.Rows[i]["LabourCardNumber"] == DBNull.Value || table.Rows[i]["LabourCardNumber"].ToString() == "NULL")
                                        {
                                            TU.VisaNumber = null;
                                        }
                                        else
                                        {
                                            TU.LabourCardNumber = Convert.ToInt64(table.Rows[i]["LabourCardNumber"]);
                                        }

                                        if (table.Rows[i]["LabourCardCode"] == DBNull.Value || table.Rows[i]["LabourCardCode"].ToString() == "NULL")
                                        {
                                            TU.LabourCardCode = null;
                                        }
                                        else
                                        {
                                            TU.LabourCardCode = table.Rows[i]["LabourCardCode"].ToString();
                                        }

                                        if (table.Rows[i]["LabourCardExpiry"] == DBNull.Value || table.Rows[i]["LabourCardExpiry"].ToString() == "NULL")
                                        {
                                            TU.LabourCardExpiry = null;
                                        }
                                        else
                                        {
                                            double parsedResult;

                                            if (double.TryParse(table.Rows[i]["LabourCardExpiry"].ToString(), out parsedResult))
                                            {
                                                TU.LabourCardExpiry = DateTime.FromOADate(Convert.ToDouble(table.Rows[i]["LabourCardExpiry"]));
                                            }
                                            else
                                            {
                                                TU.LabourCardExpiry = Convert.ToDateTime(table.Rows[i]["LabourCardExpiry"]);
                                            }
                                        }

                                        if (table.Rows[i]["InsuranceCardNumber"] == DBNull.Value || table.Rows[i]["InsuranceCardNumber"].ToString() == "NULL")
                                        {
                                            TU.InsuranceCardNumber = null;
                                        }
                                        else
                                        {
                                            TU.InsuranceCardNumber = Convert.ToInt64(table.Rows[i]["InsuranceCardNumber"]);
                                        }

                                        if (table.Rows[i]["ActiveVisa"] == DBNull.Value || table.Rows[i]["ActiveVisa"].ToString() == "NULL")
                                        {
                                            TU.ActiveVisa = null;
                                        }
                                        else
                                        {
                                            if(table.Rows[i]["ActiveVisa"].ToString() == "1" || table.Rows[i]["ActiveVisa"].ToString() == "True")
                                            {
                                                TU.ActiveVisa = true;
                                            }
                                            else
                                            {
                                                TU.ActiveVisa = false;
                                            }
                                        }

                                        if (table.Rows[i]["EmployeeStatus"] == DBNull.Value || table.Rows[i]["EmployeeStatus"].ToString() == "NULL")
                                        {
                                            TU.EmployeeStatus = null;
                                        }
                                        else
                                        {
                                            TU.EmployeeStatus = table.Rows[i]["EmployeeStatus"].ToString();
                                        }

                                        list.Add(TU);
                                    }

                                    _hrms.HrmEmployees.AddRange(list);

                                    _hrms.SaveChanges();

                                    TempData["Msg"] = "Data Inserted Successfully";
                                }
                            }
                            else
                            {
                                TempData["Msg"] = "Excel File Has No Worksheet";
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        TempData["Msg"] = "Error : " + ex.Message;
                    }
                }

                else
                {
                    TempData["Msg"] = "Only Excel File Format (.xlsx) Is Supported!";
                }
            }
            else
            {
                TempData["Msg"] = "Please Upload Excel File";
            }

            return RedirectToAction("Index");
        }




        //[HttpPost]
        //public ActionResult UploadExcel(HrmEmployee employee, HttpPostedFileBase FileUpload)
        //{

        //    List<string> data = new List<string>();
        //    if (FileUpload != null)
        //    {
        //        // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            //string filename = FileUpload.FileName;
        //            string fileExtension = System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);
        //            //string targetpath = Server.MapPath("~/Content/");
        //            string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
        //            if (System.IO.File.Exists(fileLocation))
        //            {
        //                System.IO.File.Delete(fileLocation);
        //            }
        //            FileUpload.SaveAs(fileLocation);
        //            string pathToExcelFile = fileLocation;
        //            var connectionString = "";
        //            if (fileExtension.EndsWith(".xls"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
        //            }
        //            else if (fileExtension.EndsWith(".xlsx"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
        //            }


        //            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
        //            var ds = new DataSet();
        //            adapter.Fill(ds, "ExcelTable");
        //            DataTable dtable = ds.Tables["ExcelTable"];

        //            string sheetName = "Sheet1";
        //            var excelFile = new ExcelQueryFactory(pathToExcelFile);
        //            var artistAlbums = from a in excelFile.Worksheet<HrmEmployee>(sheetName) select a;
        //            foreach (var a in artistAlbums)
        //            {
        //                try
        //                {
        //                    //if (a.Title != "" && a.FirstName != "" && a.Middlename != "" && a.LastName != "")
        //                    //{
        //                    HrmEmployee TU = new HrmEmployee();
        //                    TU.TitleId = a.TitleId;
        //                    TU.FirstName = a.FirstName;
        //                    TU.Middlename = a.Middlename;
        //                    TU.LastName = a.LastName;
        //                    TU.EmployeeCode = a.EmployeeCode;
        //                    TU.Gender = a.Gender;
        //                    TU.FatherHusbandName = a.FatherHusbandName;
        //                    TU.DateOfBirth = Convert.ToDateTime(a.DateOfBirth);
        //                  //  TU.DateOfBirth = a.DateOfBirth;
        //                    TU.IdentityCardNo = a.IdentityCardNo;
        //                    TU.IdentityExpiryDate = a.IdentityExpiryDate;
        //                    TU.ReligionId = a.ReligionId;
        //                    TU.MaritalStatus = a.MaritalStatus;
        //                    TU.Dependants = a.Dependants;
        //                    TU.NationalCountryId = a.NationalCountryId;
        //                    TU.EthnicityId = a.EthnicityId;
        //                    TU.BloodGroup = a.BloodGroup;
        //                    TU.HrmLanguageId = a.HrmLanguageId;
        //                    TU.DisabilitiesId = a.DisabilitiesId;
        //                    TU.EmployeeType = a.EmployeeType;
        //                    TU.EmployeeGroupId = a.EmployeeGroupId;
        //                    TU.HrmLocationOficeId = a.HrmLocationOficeId;
        //                    TU.DesignationId = a.DesignationId;
        //                    //TU.ReportToId = a.ReportToId;
        //                    TU.DepartmentId = a.DepartmentId;
        //                    TU.SubDepartmentId = a.SubDepartmentId;
        //                    TU.HrmGradeId = a.HrmGradeId;
        //                    TU.MachineId = a.MachineId;
        //                    TU.dteJoiningDate = Convert.ToDateTime(a.dteJoiningDate);
        //                   // TU.dteJoiningDate =a.dteJoiningDate;
        //                    TU.ProbationPeriod = a.ProbationPeriod;
        //                    TU.ConfirmationDate = a.ConfirmationDate;
        //                    TU.ContractExpiryDate = a.ContractExpiryDate;
        //                    TU.BasicSalary = a.BasicSalary;
        //                    TU.ContacNo = a.ContacNo;
        //                    TU.Email = a.Email;
        //                    //TU.LivingCountryId = a.LivingCountryId;
        //                    TU.StateId = a.StateId;
        //                    TU.CityId = a.CityId;
        //                    TU.ZipCode = a.ZipCode;
        //                    TU.CurrentAddress = a.CurrentAddress;
        //                    TU.ProfilePicture = a.ProfilePicture;
        //                    //TU.CurrentCountryId = a.CurrentCountryId;
        //                    TU.CurrentStateId = a.CurrentStateId;
        //                    TU.CurrentCityId = a.CurrentCityId;
        //                    TU.CurrentZipCode = a.CurrentZipCode;
        //                    TU.UserName = a.UserName;
        //                    TU.Password = a.Password;
        //                    TU.IswebloginAllowed = a.IswebloginAllowed;
        //                    //TU.SoftRoleinformation = a.SoftRoleinformation;
        //                    //TU.ApplicationUserId = a.ApplicationUserId;
        //                    TU.CostCenterId = a.CostCenterId;
        //                    TU.Active = a.Active;
        //                    //TU.Country_Id = a.Country_Id;
        //                    TU.PassportExpiryDate = Convert.ToDateTime(a.PassportExpiryDate);
        //                   // TU.PassportExpiryDate =a.PassportExpiryDate;
        //                    TU.BiometricCode = a.BiometricCode;
        //                    TU.Title = a.Title;
        //                    TU.Passport = a.Password;
        //                    TU.DrivingLicence = a.DrivingLicence;
        //                    TU.BirthCountryId = a.BirthCountryId;
        //                    TU.HrmLanguage = a.HrmLanguage;
        //                    TU.ProbationType = a.ProbationType;
        //                    TU.OfficialNo = a.OfficialNo;
        //                    TU.OfficialEmail = a.OfficialEmail;
        //                    TU.RegionId = a.RegionId;
        //                    TU.PermnantAddress = a.PermnantAddress;
        //                    TU.BankName = a.BankName;
        //                    TU.BranchCode = a.BranchCode;
        //                    TU.BranchName = a.BranchName;
        //                    TU.AccountTitle = a.AccountTitle;
        //                    TU.AccountNumber = a.AccountNumber;
        //                    TU.AccountType = a.AccountType;
        //                    TU.ReportToId = a.ReportToId;
        //                    TU.ReportToPerson = a.ReportToPerson;
        //                    TU.ShiftId = a.ShiftId;
        //                    _hrms.HrmEmployees.Add(TU);
        //                    _hrms.SaveChanges();

        //                }
        //                catch (DbEntityValidationException ex)
        //                {
        //                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //                    {
        //                        foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                        {
        //                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                        }
        //                    }
        //                }
        //            }
        //            //deleting excel file from folder
        //            //if ((System.IO.File.Exists(pathToExcelFile)))
        //            //{
        //            //    System.IO.File.Delete(pathToExcelFile);
        //            //}
        //           // return Json("success", JsonRequestBehavior.AllowGet);
        //            return RedirectToAction("Index", "EmployeeProfile");

        //        }
        //        else
        //        {
        //            //alert message for invalid file format
        //            data.Add("<ul>");
        //            data.Add("<li>Only Excel file format is allowed</li>");
        //            data.Add("</ul>");
        //            data.ToArray();
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        data.Add("<ul>");
        //        if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
        //        data.Add("</ul>");
        //        data.ToArray();
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        public ActionResult Employeesddl()
        {
            // var data = _hrms.HrmEmployees.Where(x => x.Active == true).ToList();

            var dd = _hrms.Regions.Where(x => x.Active == true).FirstOrDefault();
            List<HrmEmployee> empList = _hrms.HrmEmployees.ToList<HrmEmployee>();
            var result = empList.Select(S => new
            {
                Id = S.Id,
                Name = S.FirstName + " " + S.LastName
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #region EmployeeVisaDetail


        public ActionResult AutoCodeEmployeeVisaDetail()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.EmployeeVisaDetails.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.VisaTitle))

            {
                stringCode = LastCode.VisaTitle.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.VisaTitle.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "T-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EmployeeVisaDetail()
        {

            return View();
        }
        public ActionResult GetEmployeeVisaDetailList()
        {
            try
            {
                var dd = _hrms.EmployeeVisaDetails.Where(x => x.Active == true).FirstOrDefault();
                var VisaDetailList = _hrms.EmployeeVisaDetails.Select(x => x).ToList();
                var result = VisaDetailList.Select(S => new
                {
                    Id = S.Id,
                    VisaTitle = S.VisaTitle,
                    VisaNumber = S.VisaNumber,
                    VisaIssuanceDate = S.VisaIssuanceDate.ToString(),
                    VisaExpiryDate = S.VisaExpiryDate.ToString(),
                    LabourCardNumber = S.LabourCardNumber,
                    LabourCardCode = S.LabourCardCode,
                    LabourCardExpiry = S.LabourCardExpiry.ToString(),
                    InsuranceCardNumber = S.InsuranceCardNumber,
                    Active = S.Active,

                });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult EditEmployeeVisaDetail(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = _hrms.EmployeeVisaDetails.Where(x => x.Id == id).FirstOrDefault<EmployeeVisaDetail>();


            object obj = new
            {
                data,
                VisaIssuanceDate = data.VisaIssuanceDate?.ToString("yyyy-MM-dd"),
                //JoiningDate = data.JoiningDate?.ToString("dd/MM/yyyy"),
                VisaExpiryDate = data.VisaExpiryDate?.ToString("yyyy-MM-dd"),
                //DateOfBirth = data.DateOfBirth?.ToString("dd/MM/yyyy"),
                LabourCardExpiry = data.LabourCardExpiry?.ToString("yyyy-MM-dd"),

            };
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            // var data = JsonConvert.SerializeObject(obj, jss);
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddEmployeeVisaDetail(EmployeeVisaDetail obj)
        {
            try
            {


                bool IsrecExisit = _hrms.EmployeeVisaDetails.Any(x => x.VisaNumber == obj.VisaNumber);
                if (IsrecExisit != true)
                {

                    _hrms.EmployeeVisaDetails.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Employee Visa Detail is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateEmployeeVisaDetail(EmployeeVisaDetail obj)
        {

            try
            {

                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public ActionResult DeleteEmployeeVisaDetail(int id)
        {
            try
            {
                EmployeeVisaDetail rg = _hrms.EmployeeVisaDetails.Where(x => x.Id == id).FirstOrDefault<EmployeeVisaDetail>();
                _hrms.EmployeeVisaDetails.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion  EmployeeVisaDetail End





    }

}