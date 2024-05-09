using HRMS.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using TalenBAL.BAL;

namespace HRMS.Controllers
{
    public class RecruitmentController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();

        Guid objGuid = Guid.NewGuid();
        JobRequisitions ObjJobRequisitions = new JobRequisitions();
        string DocPhysicalPath = WebConfigurationManager.AppSettings["DocPhysicalPath"];
        string DocPhysicalPathImage = WebConfigurationManager.AppSettings["DocPhysicalPathImage"];
        string DocLivePath = WebConfigurationManager.AppSettings["DocLivePath"];
        BLRecruitment ObjBLRecruitment = new BLRecruitment();
        BLCommon ObjBLCommon = new BLCommon();
        IList<RecruitmentView> IListRecruitmentView = new List<RecruitmentView>();
        // GET: Recruitment

        // GET: Recruitment

        public ActionResult JobCreate()
        {
            ViewBag.JobTypeDDL = ObjBLRecruitment.GetAllJobType();           
            ViewBag.DepartmentDDL = ObjBLRecruitment.GetAllDepartment();
            ViewBag.DesignationDDL = ObjBLRecruitment.GetAllDesignation();
            ViewBag.HrmSkillsDDL = ObjBLRecruitment.GetAllSkills();
            ViewBag.ShiftDDL = ObjBLRecruitment.GetAllShifts();
            return View();
        }


        public ActionResult Index()
        {
            return RedirectToAction(nameof(JobsRequestion));
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

        #region Employee_JobRequestion
        public ActionResult JobsRequestion()
        {

            //var jobs = ObjBLRecruitment.GetDefaultjobs();
            //foreach (var item in jobs)
            //{
            //    var department = ObjBLCommon.GetDepartmentById(item.DepartmentId);
            //    var designation = ObjBLCommon.GetDesignationId(item.DesignationId);
            //    var shift = ObjBLCommon.GetShiftId(item.ShiftId);
            //    var jobtype = ObjBLCommon.GetJobTypeId(item.JobType);
            //    RecruitmentView Obj = new RecruitmentView();
            //    Obj.Id = item.ReqId;
            //    Obj.AddvertiseNo = item.AddvertiseNo;
            //    Obj.JobTypeId = Convert.ToInt64(item.JobType);
            //    Obj.JobTypeName = jobtype.Name;
            //    Obj.JobTitle = item.JobTitle;
            //    Obj.DesignationId = Convert.ToInt64(item.DesignationId);
            //    Obj.DesignationName = designation.Name;
            //    Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
            //    Obj.DepartmentName = department.Name;
            //    Obj.ShiftId = Convert.ToInt64(item.ShiftId);
            //    Obj.ShiftCode = shift.Code;
            //    Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
            //    Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
            //    Obj.MInQualification = item.MInQualification;
            //    Obj.Location = item.Location;
            //    Obj.Gender = item.Gender;
            //    Obj.Age = Convert.ToInt32(item.Age);
            //    Obj.Skills = item.Skills;
            //    Obj.LastDate = Convert.ToDateTime(item.LastDate);
            //    Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
            //    Obj.Currency = item.Currency;
            //    Obj.Status = Convert.ToInt32(item.Status);
            //    IListRecruitmentView.Add(Obj);
            //}

            //ViewBag.JobReqTable = jobs;





            return View();
        }
        public ActionResult GetAllJobsRequestion()
        {
            //var data = _hrms.JobRequisitions.ToList();
            //List<object> listOfObject = new List<object>();
            //foreach (var item in data)
            //{
            //    var newObject = new
            //    {
            //        JobRequestion = item,
            //        LastDate = item.LastDate.ToString("dd/MM/yyyy"),
            //        JobType = _hrms.HrmJobTypes.FirstOrDefault(x => x.Id == item.JobType)?.Name,
            //        Designation = _hrms.Designations.FirstOrDefault(x => x.Id == item.DesignationId)?.Name,
            //        Department = _hrms.Departments.FirstOrDefault(x => x.Id == item.DepartmentId)?.Name,
            //    };

               
            //    listOfObject.Add(newObject);
            //}
            var record = ObjBLRecruitment.GetAllJobsMaster();
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var result = JsonConvert.SerializeObject(record, jss);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult JobRequestion(int? Id)
        {

            ViewBag.JobTitleDDL = ObjBLRecruitment.GetAllJobMaster();
            var jobRequisitions = _hrms.JobRequisitions.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var JobTypes = _hrms.HrmJobTypes.ToList();
            var model = new Common
            {
                JobRequisitions = jobRequisitions,
                DesignationList = Designations,
                DepartmentList = Departments,
                JobTypeList = JobTypes
            };
            return View(model);
        }

        public ActionResult JobRequestionEdit(int? Id)
        {
            var jobRequisitions = _hrms.JobRequisitions.ToList();
            var Designations = _hrms.Designations.ToList();
            var Departments = _hrms.Departments.ToList();
            var JobTypes = _hrms.HrmJobTypes.ToList();
            var model = new Common
            {
                JobRequisitions = jobRequisitions,
                DesignationList = Designations,
                DepartmentList = Departments,
                JobTypeList = JobTypes
            };
            return View(model);
        }
        public ActionResult LoadJobsRequestion(long id)
        {
            //var jobRequstion = _hrms.JobRequisitions.FirstOrDefault(x => x.ReqId == id);
            var Result = ObjBLRecruitment.GetAllJobsMasterbyId(id);

            return Json(Result, JsonRequestBehavior.AllowGet);
            //return Json(new
            //{
            //    jobRequstion = jobRequstion,
            //   // LastDate = jobRequstion?.LastDate.ToString("yyyy-MM-dd")
            //}
            //, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertUpdateJobsRequestion(JobRequisitions jobRequisition)
        {
            var obj = _hrms.JobRequisitions.Where(x => x.ReqId == jobRequisition.ReqId).FirstOrDefault();
            if (obj != null && !string.IsNullOrEmpty(obj.Attachment) && !obj.Attachment.Equals(jobRequisition.Attachment))
            {
                string path = Path.Combine(Server.MapPath("~/Files/"), obj.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            if (obj != null)
                _hrms.Entry(obj).State = EntityState.Detached;
            if (jobRequisition.ReqId > 0)
            {
                _hrms.Entry(jobRequisition).State = EntityState.Modified;
            }
            else
            {
                _hrms.JobRequisitions.Add(jobRequisition);
            }

            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }


        public ActionResult AddUpdateJobsRequestion(FormCollection form, HttpPostedFileBase file1)
        {
            string fileName = "";
            //foreach (string upload in Request.Files)
            //{
            //    if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

            //    HttpPostedFileBase file = Request.Files[upload];
            //    fileName = objGuid.ToString() + file.FileName;
            //    var path = Path.Combine(Server.MapPath(DocPhysicalPath), fileName);
            //    file.SaveAs(path);

            //}
            long RequestId = 0;
            if(form["JobRequestId"] != null || form["JobRequestId"]!="")
            {
                RequestId = Convert.ToInt64(form["JobRequestId"]);
            }
            ObjJobRequisitions.AddvertiseNo = form["AddvertiseNo"];
            ObjJobRequisitions.JobId = Convert.ToInt64(form["JobTitleDDL"]);
            ObjJobRequisitions.LastDate = Convert.ToDateTime(form["txtLastDate1"]);
            ObjJobRequisitions.Currency = form["txtCurrency"];
            ObjJobRequisitions.Active = true;
            var IsrecordExist = _hrms.JobRequisitions.Where(x => x.ReqId == RequestId).FirstOrDefault();
            if(IsrecordExist == null)
            {
                _hrms.JobRequisitions.Add(ObjJobRequisitions);
                _hrms.SaveChanges();
            }
            else
            {
                IsrecordExist.AddvertiseNo = ObjJobRequisitions.AddvertiseNo;
                IsrecordExist.JobId = ObjJobRequisitions.JobId;
                IsrecordExist.LastDate = ObjJobRequisitions.LastDate;
                IsrecordExist.Currency = ObjJobRequisitions.Currency;
                _hrms.SaveChanges();

            }

            //ObjJobRequisitions.JobType = Convert.ToInt32(form["JobType"]);
            //ObjJobRequisitions.JobTitle = form["JobTitle"];
            //ObjJobRequisitions.DesignationId = Convert.ToInt32(form["DesignationId"]);
            //ObjJobRequisitions.DepartmentId = Convert.ToInt32(form["DepartmentId"]);
            //ObjJobRequisitions.ShiftId = Convert.ToInt32(form["ShiftId"]);
            //ObjJobRequisitions.MinExpereince = Convert.ToInt32(form["MinExpereince"]);
            //ObjJobRequisitions.MaxExpereince = Convert.ToInt32(form["MaxExpereince"]);
            //ObjJobRequisitions.MInQualification = form["MInQualification"];
            //ObjJobRequisitions.Location = form["Location"];
            //ObjJobRequisitions.Gender = form["Gender"];
            //ObjJobRequisitions.Age = Convert.ToInt32(form["Age"]);
            //ObjJobRequisitions.Skills = form["Skills"];

            //ObjJobRequisitions.ExpectedSalary = Convert.ToInt32(form["ExpectedSalary"]);

            // ObjJobRequisitions.Attachment = fileName;
           
            //return RedirectToAction("GetAllJobsRequestion");
            return RedirectToAction("Index", "Recruitment");
            // return RedirectToAction("GetAllJobsRequestion", "Recruitment");



            //var obj = _hrms.JobRequisitions.Where(x => x.ReqId == jobRequisition.ReqId).FirstOrDefault();
            //if (obj != null && !string.IsNullOrEmpty(obj.Attachment) && !obj.Attachment.Equals(jobRequisition.Attachment))
            //{
            //    string path = Path.Combine(Server.MapPath("~/Files/"), obj.Attachment);
            //    FileInfo file = new FileInfo(path);
            //    if (file.Exists)//check file exsit or not
            //        file.Delete();
            //}
            //if (obj != null)
            //    _hrms.Entry(obj).State = EntityState.Detached;
            //if (jobRequisition.ReqId > 0)
            //{
            //    _hrms.Entry(jobRequisition).State = EntityState.Modified;
            //}
            //else
            //{
            //    _hrms.JobRequisitions.Add(jobRequisition);
            //}

            //var rowAffected = _hrms.SaveChanges();
            //if (rowAffected > 0)
            //    return Json(new { success = true, message = "Successfully Submit.", JsonRequestBehavior.AllowGet });
            //else
            //return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });

        }
        public ActionResult DeleteJobsRequestion(int Id)
        {
            JobRequisitions job = _hrms.JobRequisitions.Where(x => x.ReqId == Id).FirstOrDefault<JobRequisitions>();
            if (job != null && !string.IsNullOrEmpty(job.Attachment))
            {
                string path = Path.Combine(Server.MapPath("~/Files/"), job.Attachment);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not
                    file.Delete();
            }
            _hrms.JobRequisitions.Remove(job);
            var rowAffected = _hrms.SaveChanges();
            if (rowAffected > 0)
                return Json(new { success = true, message = "Delete Successfuly.", JsonRequestBehavior.AllowGet });
            else
                return Json(new { success = false, message = "Something went wrong.", JsonRequestBehavior.AllowGet });
        }
        #endregion

        #region Job Master Start
        public ActionResult Job()
        {
            return View();
        }


        public ActionResult GetJobList()
        {
            try
            {
                // var data = ObjBLRecruitment.GetAppliedCAndiateList();
                //var JobList = _hrms.Jobs.Select(s => s).ToList();
                //var DepartmentList = _hrms.Departments.Select(s => s).ToList();
                //var DesignationList = _hrms.Designations.Select(s => s).ToList();
                //var Result = (from st in JobList
                //             join d in DepartmentList on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in DesignationList on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join sf in _hrms.ShiftMasters on st.ShiftId equals sf.ShiftId into table3
                //             from sf in table3.ToList()

                //             select new
                //             {
                //                 Id = st.JobId,
                //                 Name = st.JobTitle,
                //                 ClosingDate = st.ClosingDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Shift = st.ShiftId,
                //                 ShiftName = sf.ShiftName
                //             }).ToList();

                var data = (from jb in _hrms.Jobs
                            join dp in _hrms.Departments on jb.DepartmentId equals dp.Id
                            join dg in _hrms.Designations on jb.DesignationId equals dg.Id
                            join sf in _hrms.ShiftMasters on jb.ShiftId  equals sf.ShiftId
                            select new
                            {
                                Id = jb.JobId,
                                Name = jb.JobTitle,
                                ClosingDate = jb.ClosingDate.ToString(),
                                Active = jb.Active,
                                DepertmentName = dp.Name,
                                DesignationName = dg.Name,
                                Shift = jb.ShiftId,
                                ShiftName = sf.ShiftName
                            });
        
                return Json(data, JsonRequestBehavior.AllowGet);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpGet]
        public ActionResult EditJobList(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Jobs.Where(x => x.JobId == id).FirstOrDefault<Job>();
                //DateTime dt=DateTime.Parse(s);
                var abc = result.ClosingDate.ToString();
                result.CreatedBy = abc;
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddJobMaster(Job obj)
        {
            try
            {
                obj.CreatedBy = "1";
                obj.CreatedOn = DateTime.Now;
                _hrms.Jobs.Add(obj);

                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateJobMaster(Job obj)
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
        public ActionResult DeleteJobMaster(int id)
        {
            try
            {
                Job jb = _hrms.Jobs.Where(x => x.JobId == id).FirstOrDefault<Job>();
                _hrms.Jobs.Remove(jb);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Job Master End


        #region Applied Candidates Start


        public ActionResult HrmAppliedCandidate()
        {

            return View();
        }


        public ActionResult AppliedCandidateList()
        {
            try
            {
                var data = ObjBLRecruitment.GetAppliedCAndiateList();
                //ViewData["ViewResume"] = data.Attachment;
                //var record = (from mt in _hrms.ApplyCandidates
                //             join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //             from jb in table1.ToList()
                //             join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //             from dp in table2.ToList()
                //             join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //             from dg in table3.ToList()
                //             join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //             from jt in table4.ToList()
                //             join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //             from sf in table5.ToList()

                //             where mt.Status == "applied"
                //             select new
                //             {
                //                 Id = mt.AppliedId,
                //                 AddvertiseNo = jb.AddvertiseNo,
                //                 Name = mt.CandidateName,
                //                 FatherName = mt.FatherName,
                //                 CNIC = mt.CNIC,
                //                 ContactNumber = mt.ContactNumber,
                //                 Email = mt.Email,
                //                 JobType = jt.Name,
                //                 JobTitle = jb.JobTitle,
                //                 Shift = sf.ShiftName,
                //                 MinExpereince = jb.MinExpereince,
                //                 MaxExpereince = jb.MaxExpereince,
                //                 MInQualification = jb.MInQualification,
                //                 Location = jb.Location,
                //                 Gender = jb.Gender,
                //                 Age = jb.Age,
                //                 Skills = jb.Skills,
                //                 ExpectedSalary = jb.ExpectedSalary,
                //                 Currency = jb.Currency,
                //                 Photo = DocPhysicalPath + mt.Photo,
                //                 ApplyDate = mt.ApplyDate.ToString(),
                //                 AvailableDate = mt.AvailableDate.ToString(),
                //                 Active = mt.IsActive,
                //                 DepertmentName = dp.Name,
                //                 DesignationName = dg.Name,
                //                 Status = mt.Status,
                //                 Attachment = DocPhysicalPath + mt.Attachment


                //             }).ToList();


                return Json(data, JsonRequestBehavior.AllowGet);
                //List<AppliedCandidat> AppliedCandidatList = _hrms.Applieds.ToList<AppliedCandidat>();
                //List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                //var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //var DepartmentList = _hrms.Designations.Select(s => s).ToList();
                //var DesignationList = _hrms.Designations.Select(s => s).ToList();
                //var Result = from st in AppliedCandidatList
                //             join d in DepartmentList on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in DesignationList on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                //             from j in table3.ToList()
                //             where st.Status == "applied"
                //             select new
                //             {
                //                 Id = st.AppliedId,
                //                 Name = st.CandidateName,
                //                 JobTitle = j.JobTitle,
                //                 ApplyDate = st.ApplyDate.ToString(),
                //                 AvailableDate = st.AvailableDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Status = st.Status,
                //                 Attachment = DocPhysicalPath + st.Attachment

                //             };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AppliedCandidate(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = ObjBLRecruitment.GetApplyCandidatebyId(id);

              



                //var result = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table2.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()
                //              where mt.AppliedId == id
                //              //where mt.Status == "applied" 
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  ShiftName = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepartmentId = jb.DepartmentId,
                //                  DepertmentName = dp.Name,
                //                  DesignationId = jb.DesignationId,
                //                  DesignationName = dg.Name,
                //                  AppliedFrom = mt.AppliedFrom,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();

                ViewBag.Applied = result;
                //var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                //var applyDate = result.ApplyDate.ToString();
                //result.CreatedBy = applyDate;
                //var availableDate = result.AvailableDate.ToString();
                //result.LastModifiedBy = availableDate;
                //var fileView = DocPhysicalPath + result.Attachment;
                //result.Attachment = fileView;
                //var imageView = DocPhysicalPathImage + result.Photo;
                //result.Photo = imageView;
                Session["shortList"] = "shortList";
                //ViewData["shortList"] = "shortList";

                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AddAppliedCandidate()
        {
            //AppliedCandidat model = new AppliedCandidat();
            //if (id!=null&&id>0)
            //{
            //    _hrms.Configuration.ProxyCreationEnabled = false;

            //    model = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
            //    if (model == null)
            //    {
            //        model = new AppliedCandidat();
            //    }
            //}

            return View();
        }

        [HttpPost]
        public ActionResult AddAppliedCandidate(AppliedCandidat obj)
        {
            try
            {

                string _imgname = string.Empty;
                string fileGuid = string.Empty;

                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["Photo"];

                    var Attachment = System.Web.HttpContext.Current.Request.Files["Attachment"];
                    if (pic.ContentLength > 0 || Attachment.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(pic.FileName);
                        string _filename;
                        var _ext = Path.GetExtension(pic.FileName);
                        int fileExtPos = fileName.LastIndexOf(".");

                        _filename = fileName.Substring(0, fileExtPos);
                        if (_filename.Contains(" "))
                        {
                            _filename = _filename.Replace(" ", "_");

                        }

                        var extensionCv = Path.GetExtension(Attachment.FileName);

                        _imgname = Guid.NewGuid().ToString();
                        string _comPath = Path.Combine(Server.MapPath(DocPhysicalPathImage) + _filename + _imgname + _ext);
                        obj.Photo = _filename + _imgname + _ext;
                        if (extensionCv == ".jpg" || extensionCv == ".jpeg" || extensionCv == ".png" || extensionCv == ".pdf" || extensionCv == ".doc")
                        {
                            string fname = Attachment.FileName;

                            if (fname.Contains(" "))
                            {
                                fname = fname.Replace(" ", "_");

                            }

                            fileGuid = Guid.NewGuid().ToString();
                            var extension = Path.GetExtension(Attachment.FileName);
                            obj.Attachment = fileGuid + fname;
                            fname = Path.Combine(Server.MapPath(DocPhysicalPath), fileGuid + fname);
                            // Get the complete folder path and store the file inside it.

                            //obj.Attachment = "~/Content/Assets/UploadImage/fileUpload/" + fname;
                            //ViewBag.Msg = _comPath;
                            var path = _comPath;
                            obj.Status = "applied";

                            _hrms.Applieds.Add(obj);
                            if (_hrms.SaveChanges() > 0)
                            {
                                // Saving Image in Original Mode
                                pic.SaveAs(path);
                                Attachment.SaveAs(fname);

                                // resizing image
                                MemoryStream ms = new MemoryStream();
                                WebImage img = new WebImage(_comPath);

                                if (img.Width > 200)
                                    img.Resize(200, 200);
                                img.Save(_comPath);
                                // end resize
                            }
                        }
                    }
                }

                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult ShorListUpdate(int Id)
        {

            var record = _hrms.ApplyCandidates.Where(x => x.AppliedId == Id).FirstOrDefault();
            record.Status = "shortlist";
            _hrms.Entry(record).State = EntityState.Modified;
            _hrms.SaveChanges();
            return View();
        }

        public ActionResult UpdateAppliedCandidate(AppliedInterviewViewModel model)
        {
            try
            {

                if (Session["shortList"] == "shortList")
                {

                    //var objApplied = new AppliedCandidat
                    //{
                    //    AppliedId = model.AppliedId,
                    //    CandidateName = model.CandidateName,
                    //    FatherName = model.FatherName,
                    //    CNIC = model.CNIC,
                    //    ContactNumber = model.ContactNumber,
                    //    Email = model.Email,
                    //    JobTitle = model.JobTitle,
                    //    JobType = model.JobType,
                    //    ShiftId = model.ShiftId,
                    //    DepartmentId = model.DepartmentId,
                    //    DesignationId = model.DesignationId,
                    //    MinExpereince = model.MinExpereince,
                    //    MaxExpereince = model.MaxExpereince,
                    //    MInQualification = model.MInQualification,
                    //    AddvertiseNo = model.AddvertiseNo,
                    //    Location = model.Location,
                    //    Gender = model.Gender,
                    //    Skills = model.Skills,
                    //    ExpectedSalary = model.ExpectedSalary,
                    //    AppliedFrom = model.AppliedFrom,
                    //    Age = model.Age,
                    //    Currency = model.Currency,
                    //    Status = model.Status,
                    //    Photo = model.Photo,
                    //    Attachment = model.Attachment,
                    //    ApplyDate = model.ApplyDate,
                    //    AvailableDate = model.AvailableDate,
                    //    Active = model.Active
                    //};
                    var record = _hrms.ApplyCandidates.Where(x => x.AppliedId == model.AppliedId).FirstOrDefault();
                    record.Status = "shortList";
                    _hrms.Entry(record).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    //var status = "shortList";
                    //objApplied.Status = status;
                    //_hrms.Entry(objApplied).State = EntityState.Modified;
                    //_hrms.SaveChanges();
                    Session.Abandon();
                }
                if (Session["interview"] == "interview")
                {
                    //var objApplied = new AppliedCandidat
                    //{
                    //    AppliedId = model.AppliedId,
                    //    CandidateName = model.CandidateName,
                    //    FatherName = model.FatherName,
                    //    CNIC = model.CNIC,
                    //    ContactNumber = model.ContactNumber,
                    //    Email = model.Email,
                    //    JobTitle = model.JobTitle,
                    //    JobType = model.JobType,
                    //    ShiftId = model.ShiftId,
                    //    DepartmentId = model.DepartmentId,
                    //    DesignationId = model.DesignationId,
                    //    MinExpereince = model.MinExpereince,
                    //    MaxExpereince = model.MaxExpereince,
                    //    MInQualification = model.MInQualification,
                    //    AddvertiseNo = model.AddvertiseNo,
                    //    Location = model.Location,
                    //    Gender = model.Gender,
                    //    Skills = model.Skills,
                    //    ExpectedSalary = model.ExpectedSalary,
                    //    AppliedFrom = model.AppliedFrom,
                    //    Age = model.Age,
                    //    Currency = model.Currency,
                    //    Status = model.Status,
                    //    Photo = model.Photo,
                    //    Attachment = model.Attachment,
                    //    ApplyDate = model.ApplyDate,
                    //    AvailableDate = model.AvailableDate,
                    //    Active = model.Active
                    //};

                    //var status = "interview";
                    //objApplied.Status = status;

                    //_hrms.Entry(objApplied).State = EntityState.Modified;
                    //_hrms.SaveChanges();
                    var record = _hrms.ApplyCandidates.Where(x => x.AppliedId == model.AppliedId).FirstOrDefault();
                    record.Status = "interview";
                    _hrms.Entry(record).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    Session.Abandon();
                }
                if (Session["selected"] == "selected")
                {
                    var objInterview = new InterviewAssessment
                    {
                        AppliedId = model.AppliedId,
                        Education = model.Education,
                        ComputerLiteracy = model.ComputerLiteracy,
                        Intelligence = model.Intelligence,
                        ExperienceInterviewed = model.ExperienceInterviewed,
                        ExperienceCompanyBusiness = model.ExperienceCompanyBusiness,
                        JobKnowledgeSkill = model.JobKnowledgeSkill,
                        Personality = model.Personality,
                        CommunicationSkills = model.CommunicationSkills,
                        DevelopmentMotivation = model.DevelopmentMotivation,
                        PersonalAptitude = model.PersonalAptitude,
                        Comments = model.Comments
                        
                };

                    //var objApplied = new AppliedCandidat
                    //{
                    //    AppliedId = model.AppliedId,
                    //    CandidateName = model.CandidateName,
                    //    FatherName = model.FatherName,
                    //    CNIC = model.CNIC,
                    //    ContactNumber = model.ContactNumber,
                    //    Email = model.Email,
                    //    JobTitle = model.JobTitle,
                    //    JobType = model.JobType,
                    //    ShiftId = model.ShiftId,
                    //    DepartmentId = model.DepartmentId,
                    //    DesignationId = model.DesignationId,
                    //    MinExpereince = model.MinExpereince,
                    //    MaxExpereince = model.MaxExpereince,
                    //    MInQualification = model.MInQualification,
                    //    AddvertiseNo = model.AddvertiseNo,
                    //    Location = model.Location,
                    //    Gender = model.Gender,
                    //    Skills = model.Skills,
                    //    ExpectedSalary = model.ExpectedSalary,
                    //    AppliedFrom = model.AppliedFrom,
                    //    Age = model.Age,
                    //    Currency = model.Currency,
                    //    Status = model.Status,
                    //    Photo = model.Photo,
                    //    Attachment = model.Attachment,
                    //    ApplyDate = model.ApplyDate,
                    //    AvailableDate = model.AvailableDate,
                    //    Active = model.Active
                    //};

                    _hrms.InterviewAssessments.Add(objInterview);
                    //var status = "Selected";
                    //objApplied.Status = status;
                    //_hrms.Entry(objApplied).State = EntityState.Modified;
                    var record = _hrms.ApplyCandidates.Where(x => x.AppliedId == model.AppliedId).FirstOrDefault();
                    record.Status = "Selected";
                    _hrms.Entry(record).State = EntityState.Modified;
                    _hrms.SaveChanges();
                   
                    Session.Abandon();

                }
                string _imgname = string.Empty;
                string fileGuid = string.Empty;

                //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                //{
                //var pic = System.Web.HttpContext.Current.Request.Files["Photo"];

                //var Attachment = System.Web.HttpContext.Current.Request.Files["Attachment"];
                //if (pic.ContentLength > 0 || Attachment.ContentLength > 0)
                //{
                //var fileName = Path.GetFileName(pic.FileName);
                //string _filename;
                //var _ext = Path.GetExtension(pic.FileName);
                //int fileExtPos = fileName.LastIndexOf(".");

                //_filename = fileName.Substring(0, fileExtPos);

                //_imgname = Guid.NewGuid().ToString();
                //string _comPath = Path.Combine(Server.MapPath("~/Content/Assets/UploadImage/images/") + _filename + _imgname + _ext);
                //obj.Photo = _filename + _imgname + _ext;

                //string fname = Attachment.FileName;
                //fileGuid = Guid.NewGuid().ToString();
                //var extension = Path.GetExtension(Attachment.FileName);
                //obj.Attachment = fileGuid + fname;
                //fname = Path.Combine(Server.MapPath("~/Content/Assets/UploadImage/fileUpload/"), fileGuid + fname);
                // Get the complete folder path and store the file inside it.

                //obj.Attachment = "~/Content/Assets/UploadImage/fileUpload/" + fname;
                //ViewBag.Msg = _comPath;
                //var path = _comPath;


                //if (_hrms.SaveChanges() > 0)
                //{
                //    // Saving Image in Original Mode
                //    pic.SaveAs(path);
                //    Attachment.SaveAs(fname);

                //    // resizing image
                //    MemoryStream ms = new MemoryStream();
                //    WebImage img = new WebImage(_comPath);

                //    if (img.Width > 200)
                //        img.Resize(200, 200);
                //    img.Save(_comPath);
                // end resize
                //    }
                //}
                //}

                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });


                //_hrms.Entry(obj).State = EntityState.Modified;
                //_hrms.SaveChanges();
                //return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteAppliedCandidate(int id)
        {
            try
            {
                AppliedCandidat jb = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                _hrms.Applieds.Remove(jb);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult ShortList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewApplied(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                //var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                //var applyDate = result.ApplyDate.ToString();
                //result.CreatedBy = applyDate;
                //var availableDate = result.AvailableDate.ToString();
                //result.LastModifiedBy = availableDate;
                var result = (from mt in _hrms.ApplyCandidates
                              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                              from jb in table1.ToList()
                              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                              from dp in table2.ToList()
                              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                              from dg in table2.ToList()
                              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                              from jt in table4.ToList()
                              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                              from sf in table5.ToList()

                              where mt.Status == "Applied"
                              select new
                              {
                                  Id = mt.AppliedId,
                                  AddvertiseNo = jb.AddvertiseNo,
                                  CandidateName = mt.CandidateName,
                                  FatherName = mt.FatherName,
                                  CNIC = mt.CNIC,
                                  ContactNumber = mt.ContactNumber,
                                  Email = mt.Email,
                                  JobType = jt.Name,
                                  JobTitle = jb.JobTitle,
                                  ShiftName = sf.ShiftName,
                                  MinExpereince = jb.MinExpereince,
                                  MaxExpereince = jb.MaxExpereince,
                                  MInQualification = jb.MInQualification,
                                  Location = jb.Location,
                                  Gender = jb.Gender,
                                  Age = jb.Age,
                                  Skills = jb.Skills,
                                  ExpectedSalary = jb.ExpectedSalary,
                                  Currency = jb.Currency,
                                  Photo = DocPhysicalPath + mt.Photo,
                                  ApplyDate = mt.ApplyDate.ToString(),
                                  AvailableDate = mt.AvailableDate.ToString(),
                                  Active = mt.IsActive,
                                  DepertmentName = dp.Name,
                                  DesignationName = dg.Name,
                                  AppliedFrom = mt.AppliedFrom,
                                  Status = mt.Status,
                                  Attachment = DocPhysicalPath + mt.Attachment


                              }).ToList();
                Session["interview"] = "interview";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult getShortList()
        {
            try
            {
                //var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                ////List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                ////List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                //var Result = from st in AppliedCandidatList
                //             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                //             from j in table3.ToList()
                //             where st.Status == "shortList"
                //             select new
                //             {
                //                 Id = st.AppliedId,
                //                 Name = st.CandidateName,
                //                 JobTitle = j.JobTitle,
                //                 ApplyDate = st.ApplyDate.ToString(),
                //                 AvailableDate = st.AvailableDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Status = st.Status,
                //                 Attachment = st.Attachment

                //             };
                var record = ObjBLRecruitment.GetApplyCandidatebyDataByStatus("shortList");
                //var record = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table3.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "shortList"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  Shift = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ViewShortList(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = ObjBLRecruitment.GetApplyCandidatebyId(id);
                //var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                //var applyDate = result.ApplyDate.ToString();
                //result.CreatedBy = applyDate;
                //var availableDate = result.AvailableDate.ToString();
                //result.LastModifiedBy = availableDate;
                //var result = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table2.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "shortList"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  ShiftName = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  AppliedFrom = mt.AppliedFrom,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();
                Session["interview"] = "interview";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Interview()
        {
            return View();
        }

        public ActionResult InterviewList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                //var Result = from st in AppliedCandidatList
                //             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                //             from j in table3.ToList()
                //             where st.Status == "interview"
                //             select new
                //             {
                //                 Id = st.AppliedId,
                //                 Name = st.CandidateName,
                //                 JobTitle = j.JobTitle,
                //                 ApplyDate = st.ApplyDate.ToString(),
                //                 AvailableDate = st.AvailableDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Status = st.Status,
                //                 Attachment = st.Attachment

                //             };
                var record = ObjBLRecruitment.GetApplyCandidatebyDataByStatus("interview");
                //var result = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table3.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "interview"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  ShiftName = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  AppliedFrom = mt.AppliedFrom,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();

                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult ViewInterView(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = ObjBLRecruitment.GetApplyCandidatebyId(id);
                //var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                //var applyDate = result.ApplyDate.ToString();
                //result.CreatedBy = applyDate;
                //var availableDate = result.AvailableDate.ToString();
                //result.LastModifiedBy = availableDate;
                //var record = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table2.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "interview"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  Shift = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();
                Session["selected"] = "selected";
                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public ActionResult IntervewResult()
         {
            return View();
        }

        public ActionResult GetIntervewResult()
        {
            try
            {



                var record = (from ia in _hrms.InterviewAssessments
                              join mt in _hrms.ApplyCandidates on ia.AppliedId equals mt.AppliedId
                              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId
                              join jp in _hrms.Jobs on jb.JobId equals jp.JobId


                              select new
                              {
                                  Id = mt.AppliedId,
                                  AddvertiseNo = jb.AddvertiseNo,
                                  CandidateName = mt.CandidateName,
                                  JobTitle = jp.JobTitle,
                                  Education  = ia.Education,
                                  ComputerLiteracy = ia.ComputerLiteracy,
                                  Intelligence = ia.Intelligence,
                                  ExperienceInterviewed = ia.ExperienceInterviewed,
                                  ExperienceCompanyBusiness = ia.ExperienceCompanyBusiness,
                                  JobKnowledgeSkill = ia.JobKnowledgeSkill,
                                  Personality = ia.Personality,
                                  CommunicationSkills = ia.CommunicationSkills,
                                  DevelopmentMotivation = ia.DevelopmentMotivation,
                                  PersonalAptitude = ia.PersonalAptitude,
                                  Comments = ia.Comments


                              }).ToList();
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Selected()
        {
            return View();
        }

        public ActionResult SelectedList()
        {
            try
            {
                var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //var Result = from st in AppliedCandidatList
                //             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                //             from j in table3.ToList()
                //             where st.Status == "Selected"
                //             select new
                //             {
                //                 Id = st.AppliedId,
                //                 Name = st.CandidateName,
                //                 JobTitle = j.JobTitle,
                //                 ApplyDate = st.ApplyDate.ToString(),
                //                 AvailableDate = st.AvailableDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Status = st.Status,
                //                 Attachment = st.Attachment

                //             };
                var record = ObjBLRecruitment.GetApplyCandidatebyDataByStatus("Selected");
                //var record = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table3.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "Selected"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  Shift = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetFillForOfferLetter(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                //var result = ObjBLRecruitment.GetAppliedCAndiateList();
                var record = ObjBLRecruitment.GetApplyCandidatebyDataByStatus("OfferLetter");
                //var result = (from mt in _hrms.ApplyCandidates
                //              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                //              from jb in table1.ToList()
                //              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                //              from dp in table2.ToList()
                //              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                //              from dg in table2.ToList()
                //              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                //              from jt in table4.ToList()
                //              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                //              from sf in table5.ToList()

                //              where mt.Status == "applied"
                //              select new
                //              {
                //                  Id = mt.AppliedId,
                //                  AddvertiseNo = jb.AddvertiseNo,
                //                  CandidateName = mt.CandidateName,
                //                  FatherName = mt.FatherName,
                //                  CNIC = mt.CNIC,
                //                  ContactNumber = mt.ContactNumber,
                //                  Email = mt.Email,
                //                  JobType = jt.Name,
                //                  JobTitle = jb.JobTitle,
                //                  ShiftName = sf.ShiftName,
                //                  MinExpereince = jb.MinExpereince,
                //                  MaxExpereince = jb.MaxExpereince,
                //                  MInQualification = jb.MInQualification,
                //                  Location = jb.Location,
                //                  Gender = jb.Gender,
                //                  Age = jb.Age,
                //                  Skills = jb.Skills,
                //                  ExpectedSalary = jb.ExpectedSalary,
                //                  Currency = jb.Currency,
                //                  Photo = DocPhysicalPath + mt.Photo,
                //                  ApplyDate = mt.ApplyDate.ToString(),
                //                  AvailableDate = mt.AvailableDate.ToString(),
                //                  Active = mt.IsActive,
                //                  DepertmentName = dp.Name,
                //                  DesignationName = dg.Name,
                //                  AppliedFrom = mt.AppliedFrom,
                //                  Status = mt.Status,
                //                  Attachment = DocPhysicalPath + mt.Attachment


                //              }).ToList();

                ViewBag.Applied = record;
                //var result = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>();
                //var applyDate = result.ApplyDate.ToString();
                //result.CreatedBy = applyDate;
                //var availableDate = result.AvailableDate.ToString();
                //result.LastModifiedBy = availableDate;
                //var fileView = DocPhysicalPath + result.Attachment;
                //result.Attachment = fileView;
                //var imageView = DocPhysicalPathImage + result.Photo;
                //result.Photo = imageView;
                Session["shortList"] = "shortList";
                //ViewData["shortList"] = "shortList";
                return Json(record, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult OfferLetter(FormCollection Form)
        {

            try
            {
                OfferLetter obj = new OfferLetter();

                
                long id = Convert.ToInt64(Form["appliedId"]);
                obj.EmployeeId = Convert.ToInt64(Form["AppliedId"]);
                obj.DesignationId = Convert.ToInt64(Form["designation"]);
                obj.DepartmentId = Convert.ToInt64(Form["department"]);
                obj.JoiningDate = Convert.ToDateTime(Form["date"]);
                obj.Salary = Convert.ToDecimal(Form["Salary"]);
                string JobTitle = Form["txtJobTitle1"];
                string DesignationName = Form["DDLDesignation1"];
                string DepartmentName = Form["DDLDepartment1"];

            
                obj.CreatedBy = 1;
                obj.CreatedOn = DateTime.Now;
                //var data = _hrms.Applieds.Where(x => x.AppliedId == id).FirstOrDefault<AppliedCandidat>()
                var data = _hrms.ApplyCandidates.Where(x => x.AppliedId == id).FirstOrDefault<ApplyCandidate>();
                bool IsrecExisit = _hrms.OfferLetters.Any(x => x.EmployeeId == obj.EmployeeId);
                if (IsrecExisit != true)
                {

                    data.Status = "OfferLetter";
                    _hrms.Entry(data).State = EntityState.Modified;
                    _hrms.SaveChanges();
                    _hrms.OfferLetters.Add(obj);
                    _hrms.SaveChanges();

                    var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                    //List<OfferLetter> offerlist = _hrms.OfferLetters.ToList<OfferLetter>();
                    //List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                    //var abc = (from st in _hrms.ApplyCandidates
                    //             join d in _hrms.OfferLetters on st.AppliedId equals d.EmployeeId into table3
                    //             from d in table3.ToList()

                    //             join rq in _hrms.JobRequisitions on st.JobId equals rq.ReqId into table4
                    //             from rq in table4.ToList()


                    //             join i in _hrms.Designations on rq.DesignationId equals i.Id into table2
                    //             from i in table2.ToList()
                    //             where st.AppliedId == Convert.ToInt32(obj.EmployeeId)
                    //             select new
                    //             {
                    //                 Id = st.AppliedId,
                    //                 Name = st.CandidateName,
                    //                 JobTitle = rq.JobTitle,
                    //                 ApplyDate = d.JoiningDate.ToString(),
                    //                 Salary = d.Salary,
                    //                 designation = i.Name
                    //             }).ToList();


                    var abc = (from mt in _hrms.ApplyCandidates
                                join jr in _hrms.JobRequisitions on mt.JobId equals jr.ReqId
                                join jb in _hrms.Jobs on jr.JobId equals jb.JobId
                                join dg in _hrms.Designations on jb.DesignationId equals dg.Id
                                where mt.AppliedId == id
                                select new
                                {
                                    Id = mt.AppliedId,
                                    Name = mt.CandidateName,
                                    JobTitle = jr.JobTitle,
                                    designation = dg.Name
                                }

                           ).ToList();





                    // var abc = Result.ToList();

                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

                    PdfDocument document = new PdfDocument();
                    pdfDoc.Open();

                    //Top Heading
                    Chunk chunk = new Chunk("Offer Letter", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC, BaseColor.MAGENTA));

                    pdfDoc.Add(chunk);

                    //Horizontal Line
                    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    pdfDoc.Add(line);

                    //Table
                    PdfPTable table1 = new PdfPTable(2);
                    table1.WidthPercentage = 100;
                    //0=Left, 1=Centre, 2=Right
                    table1.HorizontalAlignment = 0;
                    table1.SpacingBefore = 20f;
                    table1.SpacingAfter = 30f;




                    //Add table to document
                    pdfDoc.Add(table1);

                    //Horizontal Line
                    line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    pdfDoc.Add(line);

                    Paragraph para1 = new Paragraph();
                    para1.Add("To,");
                    pdfDoc.Add(para1);
                    Paragraph para2 = new Paragraph();
                    para2.Add("Name \t\t:    " + abc[0].Name);
                    para2.SpacingBefore = 10f;
                    para2.SpacingAfter = 10f;
                    pdfDoc.Add(para2);

                    Paragraph para3 = new Paragraph();
                    para3.Add("CONGRATULATIONS!");
                    para3.SpacingBefore = 10f;
                    para3.SpacingAfter = 10f;
                    pdfDoc.Add(para3);


                    Paragraph para4 = new Paragraph();
                    para4.Add("Subject: Offer for Appointment for\t\t   " + abc[0].designation);
                    para4.SpacingBefore = 10f;
                    para4.SpacingAfter = 10f;
                    pdfDoc.Add(para4);


                    Paragraph para5 = new Paragraph();
                    para5.Add("Dear \t\t       " + abc[0].Name);
                    para5.SpacingBefore = 10f;
                    para5.SpacingAfter = 10f;
                    pdfDoc.Add(para5);

                    Paragraph para6 = new Paragraph();
                    para6.Add("With reference to your application and subsequent interview with us.\nwe are pleased to offer you the following position:");
                    pdfDoc.Add(para6);

                    ////Cell
                    //cell = new PdfPCell();
                    //chunk = new Chunk("This Month's Transactions of your Credit Card");
                    //cell.AddElement(chunk);
                    //cell.Colspan = 5;
                    //cell.BackgroundColor = BaseColor.PINK;
                    //table1.AddCell(cell);


                    PdfPCell cell1 = new PdfPCell(new Phrase("Position"));

                    table1.AddCell(cell1);
                    cell1 = new PdfPCell(new Phrase(abc[0].designation));
                    table1.AddCell(cell1);

                    PdfPCell cell2 = new PdfPCell(new Phrase("Company Location"));
                    table1.AddCell(cell2);
                    cell2 = new PdfPCell(new Phrase("ABC"));
                    table1.AddCell(cell2);


                    PdfPCell cell3 = new PdfPCell(new Phrase("Probation"));
                    table1.AddCell(cell3);
                    cell3 = new PdfPCell(new Phrase("3 Month"));
                    table1.AddCell(cell3);



                    PdfPCell cell4 = new PdfPCell(new Phrase("Salary"));
                    table1.AddCell(cell4);
                    cell4 = new PdfPCell(new Phrase(obj.Salary.ToString()));
                    table1.AddCell(cell4);


                    PdfPCell cell5 = new PdfPCell(new Phrase("Joining Date"));
                    table1.AddCell(cell5);
                    cell5 = new PdfPCell(new Phrase(obj.JoiningDate.ToShortDateString()));
                    table1.AddCell(cell5);
                    pdfDoc.Add(table1);


                    Paragraph para = new Paragraph();
                    para.Add("You are requested to return the duplicate copy of the offer of appointment signed by you in token of your acceptance or Email back to us using your personal email address to our official id tendering your consent.");
                    para.SpacingBefore = 5f;
                    para.SpacingAfter = 5f;
                    pdfDoc.Add(para);


                    Paragraph para8 = new Paragraph();
                    para8.Add("We welcome you and look forward to a long and successful association");
                    para8.SpacingBefore = 5f;
                    para8.SpacingAfter = 5f;
                    pdfDoc.Add(para8);

                    Paragraph para9 = new Paragraph();
                    para9.Add("Yours sincerely,");
                    para9.SpacingBefore = 5f;
                    para9.SpacingAfter = 5f;
                    pdfDoc.Add(para9);


                    Paragraph para11 = new Paragraph();
                    para11.Add("Signature");
                    para11.SpacingBefore = 5f;
                    para11.SpacingAfter = 5f;
                    pdfDoc.Add(para11);

                    //Horizontal Line
                    line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    pdfDoc.Add(line);

                    pdfWriter.CloseStream = false;
                    pdfDoc.Close();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", );

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    return RedirectToActionPermanent("Offerletters", "Recruitment");
                }
                else
                {
                    ViewBag.SuccessCreate = "Saved successfully";
                }
                return RedirectToActionPermanent("Offerletters", "Recruitment");
                //return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult Offerletters()
        {
            return View();
        }


        public ActionResult OfferlettersList()
        {
            try
            {
                //var AppliedCandidatList = _hrms.Applieds.Select(s => s).ToList();
                //var Result = from st in AppliedCandidatList
                //             join d in _hrms.Departments on st.DepartmentId equals d.Id into table1
                //             from d in table1.ToList()
                //             join i in _hrms.Designations on st.DesignationId equals i.Id into table2
                //             from i in table2.ToList()
                //             join j in _hrms.Jobs on Convert.ToInt32(st.JobTitle) equals j.JobId into table3
                //             from j in table3.ToList()
                //             where st.Status == "OfferLetter"
                //             select new
                //             {
                //                 Id = st.AppliedId,
                //                 Name = st.CandidateName,
                //                 JobTitle = j.JobTitle,
                //                 ApplyDate = st.ApplyDate.ToString(),
                //                 AvailableDate = st.AvailableDate.ToString(),
                //                 Active = st.Active,
                //                 DepertmentName = d.Name,
                //                 DesignationName = i.Name,
                //                 Status = st.Status,
                //                 Attachment = DocPhysicalPath + st.Attachment

                //             };

                 var record = (from mt in _hrms.ApplyCandidates
                              join jb in _hrms.JobRequisitions on mt.JobId equals jb.ReqId into table1
                              from jb in table1.ToList()
                              join dp in _hrms.Departments on jb.DepartmentId equals dp.Id into table2
                              from dp in table2.ToList()
                              join dg in _hrms.Designations on jb.DesignationId equals dg.Id into table3
                              from dg in table3.ToList()
                              join jt in _hrms.HrmJobTypes on jb.JobType equals jt.Id into table4
                              from jt in table4.ToList()
                              join sf in _hrms.ShiftMasters on jb.ShiftId equals sf.ShiftId into table5
                              from sf in table5.ToList()

                              where mt.Status == "OfferLetter"
                               select new
                              {
                                  Id = mt.AppliedId,
                                  AddvertiseNo = jb.AddvertiseNo,
                                  CandidateName = mt.CandidateName,
                                  FatherName = mt.FatherName,
                                  CNIC = mt.CNIC,
                                  ContactNumber = mt.ContactNumber,
                                  Email = mt.Email,
                                  JobType = jt.Name,
                                  JobTitle = jb.JobTitle,
                                  Shift = sf.ShiftName,
                                  MinExpereince = jb.MinExpereince,
                                  MaxExpereince = jb.MaxExpereince,
                                  MInQualification = jb.MInQualification,
                                  Location = jb.Location,
                                  Gender = jb.Gender,
                                  Age = jb.Age,
                                  Skills = jb.Skills,
                                  ExpectedSalary = jb.ExpectedSalary,
                                  Currency = jb.Currency,
                                  Photo = DocPhysicalPath + mt.Photo,
                                  ApplyDate = mt.ApplyDate.ToString(),
                                  AvailableDate = mt.AvailableDate.ToString(),
                                  Active = mt.IsActive,
                                  DepertmentName = dp.Name,
                                  DesignationName = dg.Name,
                                  Status = mt.Status,
                                  Attachment = DocPhysicalPath + mt.Attachment


                              }).ToList();
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Applied Candidates End


        [HttpPost]
        public ActionResult AddInterView(InterviewAssessment obj)
        {
            try
            {
                _hrms.InterviewAssessments.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult AutoCodeGenrate()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.JobRequisitions.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.AddvertiseNo))

            {
                stringCode = LastCode.AddvertiseNo.Substring(0, 4);
                int intCode = Convert.ToInt16(LastCode.AddvertiseNo.Substring(4));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "Adv-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);
            // return View(Code);
            //Common employeeDropDowns = new Common
            //{

            //    Code = stringCode
            //};


            //return View(employeeDropDowns);
        }

        public ActionResult JobTitleChange(long JobId)
        {

            var record = ObjBLRecruitment.GetDataByJobChange(JobId);

            return Json(record, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PostJob(long Id)
        {

            var record = _hrms.JobRequisitions.Where(x => x.ReqId == Id).FirstOrDefault();
            if(record != null)
            {
                record.Status = 1;
                _hrms.SaveChanges();
            }

            return Json(record, JsonRequestBehavior.AllowGet);
        }
    }
}