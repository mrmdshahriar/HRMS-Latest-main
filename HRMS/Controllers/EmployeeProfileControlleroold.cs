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
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using TalenBAL.BAL;

namespace HRMS.Controllers
{
    public class EmployeeProfileController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        BLMaster ObjBLMaster = new BLMaster();

        #region Employee_Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EmployeesForm(string id)
        {
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

           
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var dd = _hrms.HrmEmployees.Where(x => x.FirstName != "").FirstOrDefault();
           //var items = _hrms.HrmEmployees.Where(x => x.FirstName != null).ToList();
           // var items = _hrms.HrmEmployees.Select(s =>s).ToList();

          
            var items = _hrms.HrmEmployees.Include(x => x.Department).Include(x => x.Designation).ToList();

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var data = new List<object>();      

            JsonResult json = Json(jss, JsonRequestBehavior.AllowGet);


            json.MaxJsonLength = 999999999;
            serializer.MaxJsonLength = 999999999;




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


        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            try
            {


                _hrms.Configuration.ProxyCreationEnabled = false;
           

            var items = _hrms.HrmEmployees.Include(x => x.Department).Include(x => x.Designation).ToList();

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
           
            var data = new List<object>();

             


                foreach (var item in items)
            {
                var DesignationName = _hrms.Designations.Where(x => x.Id == item.DesignationId).FirstOrDefault();
                var DepartmentName = _hrms.Departments.Where(x => x.Id == item.DepartmentId).FirstOrDefault();
                    if(item.FirstName !=null)
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
            var result = JsonConvert.SerializeObject(data, jss);

                var serializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };

                // Perform your serialization
                serializer.Serialize(result);


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
                    //JoiningDate = data.JoiningDate?.ToString("dd/MM/yyyy"),
                    DateOfBirth = data.DateOfBirth?.ToString("yyyy-MM-dd"),
                    //DateOfBirth = data.DateOfBirth?.ToString("dd/MM/yyyy"),
                    ConfirmationDate = data.ConfirmationDate?.ToString("yyyy-MM-dd"),
                    //ConfirmationDate = data.ConfirmationDate?.ToString("dd/MM/yyyy"),
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
                Designation = Designation
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
        public ActionResult LoadTerminationEmployee(int id)
        {
            var terminationEmployee = _hrms.Terminations.FirstOrDefault(x => x.TerminatinId == id);
            return Json(new
            {
                Termination = terminationEmployee,
                Date = terminationEmployee?.Date.ToString("yyyy-MM-dd"),
                LastWorkingDate = terminationEmployee?.LastWorkingDate.ToString("yyyy-MM-dd"),
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
                    TransferDate=item.TrasnferDate.ToString("dd/MM/yyyy"),
                    TransferEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.EmployeeId)?.FirstName,
                    TransferByEmployeeName = _hrms.HrmEmployees.FirstOrDefault(x => x.Id == item.TrasnferedBy)?.FirstName,
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
            return Json(new
            {
                Transfer = transferEmployee,
                TrasnferDate = transferEmployee?.TrasnferDate.ToString("yyyy-MM-dd")
            }
            , JsonRequestBehavior.AllowGet);
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



        [HttpPost]
        public ActionResult UploadExcel(HrmEmployee employee, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    //string filename = FileUpload.FileName;
                    string fileExtension = System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);
                    //string targetpath = Server.MapPath("~/Content/");
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    FileUpload.SaveAs(fileLocation);
                    string pathToExcelFile = fileLocation;
                    var connectionString = "";
                    if (fileExtension.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (fileExtension.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }


                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<HrmEmployee>(sheetName) select a;
                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            //if (a.Title != "" && a.FirstName != "" && a.Middlename != "" && a.LastName != "")
                            //{
                            HrmEmployee TU = new HrmEmployee();
                            TU.TitleId = a.TitleId;
                            TU.FirstName = a.FirstName;
                            TU.Middlename = a.Middlename;
                            TU.LastName = a.LastName;
                            TU.EmployeeCode = a.EmployeeCode;
                            TU.Gender = a.Gender;
                            TU.FatherHusbandName = a.FatherHusbandName;
                            TU.DateOfBirth = Convert.ToDateTime(a.DateOfBirth);
                            TU.IdentityCardNo = a.IdentityCardNo;
                            TU.IdentityExpiryDate = a.IdentityExpiryDate;
                            TU.ReligionId = a.ReligionId;
                            TU.MaritalStatus = a.MaritalStatus;
                            TU.Dependants = a.Dependants;
                            TU.NationalCountryId = a.NationalCountryId;
                            TU.EthnicityId = a.EthnicityId;
                            TU.BloodGroup = a.BloodGroup;
                            TU.HrmLanguageId = a.HrmLanguageId;
                            TU.DisabilitiesId = a.DisabilitiesId;
                            TU.EmployeeType = a.EmployeeType;
                            TU.EmployeeGroupId = a.EmployeeGroupId;
                            TU.HrmLocationOficeId = a.HrmLocationOficeId;
                            TU.DesignationId = a.DesignationId;
                            //TU.ReportToId = a.ReportToId;
                            TU.DepartmentId = a.DepartmentId;
                            TU.SubDepartmentId = a.SubDepartmentId;
                            TU.HrmGradeId = a.HrmGradeId;
                            TU.MachineId = a.MachineId;
                            TU.dteJoiningDate = Convert.ToDateTime(a.dteJoiningDate);
                            TU.ProbationPeriod = a.ProbationPeriod;
                            TU.ConfirmationDate = a.ConfirmationDate;
                            TU.ContractExpiryDate = a.ContractExpiryDate;
                            TU.BasicSalary = a.BasicSalary;
                            TU.ContacNo = a.ContacNo;
                            TU.Email = a.Email;
                            //TU.LivingCountryId = a.LivingCountryId;
                            TU.StateId = a.StateId;
                            TU.CityId = a.CityId;
                            TU.ZipCode = a.ZipCode;
                            TU.CurrentAddress = a.CurrentAddress;
                            TU.ProfilePicture = a.ProfilePicture;
                            //TU.CurrentCountryId = a.CurrentCountryId;
                            TU.CurrentStateId = a.CurrentStateId;
                            TU.CurrentCityId = a.CurrentCityId;
                            TU.CurrentZipCode = a.CurrentZipCode;
                            TU.UserName = a.UserName;
                            TU.Password = a.Password;
                            TU.IswebloginAllowed = a.IswebloginAllowed;
                            //TU.SoftRoleinformation = a.SoftRoleinformation;
                            //TU.ApplicationUserId = a.ApplicationUserId;
                            TU.CostCenterId = a.CostCenterId;
                            TU.Active = a.Active;
                            //TU.Country_Id = a.Country_Id;
                            TU.PassportExpiryDate = Convert.ToDateTime(a.PassportExpiryDate);
                            TU.BiometricCode = a.BiometricCode;
                            TU.Title = a.Title;
                            TU.Passport = a.Password;
                            TU.DrivingLicence = a.DrivingLicence;
                            TU.BirthCountryId = a.BirthCountryId;
                            TU.HrmLanguage = a.HrmLanguage;
                            TU.ProbationType = a.ProbationType;
                            TU.OfficialNo = a.OfficialNo;
                            TU.OfficialEmail = a.OfficialEmail;
                            TU.RegionId = a.RegionId;
                            TU.PermnantAddress = a.PermnantAddress;
                            TU.BankName = a.BankName;
                            TU.BranchCode = a.BranchCode;
                            TU.BranchName = a.BranchName;
                            TU.AccountTitle = a.AccountTitle;
                            TU.AccountNumber = a.AccountNumber;
                            TU.AccountType = a.AccountType;
                            TU.ReportToId = a.ReportToId;
                            TU.ReportToPerson = a.ReportToPerson;
                            TU.ShiftId = a.ShiftId;
                            _hrms.HrmEmployees.Add(TU);
                            _hrms.SaveChanges();

                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder
                    //if ((System.IO.File.Exists(pathToExcelFile)))
                    //{
                    //    System.IO.File.Delete(pathToExcelFile);
                    //}
                   // return Json("success", JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Index", "EmployeeProfile");
                    
                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

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