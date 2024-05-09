using HRMS.Models;
using iTextSharp.text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class PerformanceController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();

        // GET: Performance
        public ActionResult Performance()
        {
             
            return View();
        }

        public ActionResult DepartmentList()
           {
            var dd = _hrms.Departments.Where(x => x.Active == true).FirstOrDefault();
            List<Department> empList = _hrms.Departments.ToList<Department>();
            var result = empList.Select(S => 
            
            new Department()
            {
                Id = S.Id,
                Name = S.Name 
            }).ToList();
            
            ViewBag.deptName = result;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        



        #region KeyObjectives

        public ActionResult KeyObjective()
       {
            List<Department> RegionsList = _hrms.Departments.ToList();
            var result = RegionsList.Select(S => new Department()
            {
                Id = S.Id,
                Name = S.Name,
                Active = S.Active

            }).Where(x => x.Active == true).ToList();
            ViewBag.deptName = result;
            return View();
        }

       
        public ActionResult GetKeyObjectiveForDepartment(String DepName)
        {
            try
            {
                Session["DeptName"] = DepName; 

                var data = (from cs in _hrms.KeyObjectives
                            join dept in _hrms.Departments 
                            on cs.DepId equals dept.Id
                            join emp in _hrms.HrmEmployees on cs.AssignedTo equals emp.Id
                            where dept.Name == DepName && cs.Active == true

                            select new
                            {
                                Id = cs.Id,
                                AssignedTo=cs.AssignedTo,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineObjective = cs.DefineObjective,
                                AssignDate = cs.AssignDate,
                                EndDate = cs.EndDate,
                                Priority = cs.Priority,
                                AssignedPercentage = cs.AssignedPercentage,
                                CompletedPercentage = cs.CompletedPercentage,
                                Status = cs.Status
                            }).ToList();


                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult GetEmployeeResultsData(String DepName)
        {
            try
            {
                Session["DeptName"] = DepName;

                var data = (from kreslt in _hrms.KeyResults
                            join kobj in _hrms.KeyObjectives
                            on kreslt.KeyObjectiveId equals kobj.Id
                            join dept in _hrms.Departments
                            on kobj.DepId equals dept.Id
                            join emp in _hrms.HrmEmployees
                            on kreslt.AssignedTo equals emp.Id
                            where dept.Name == DepName && kreslt.Active == true

                            select new
                            {
                                Id = kreslt.Id,
                                AssignedTo = kreslt.AssignedTo,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineKeyResult = kreslt.DefineKeyResult,
                                AssignDate = kreslt.AssignDate,
                                EndDate = kreslt.EndDate,
                                Priority = kreslt.Priority,
                                AssignedPercentage = kreslt.AssignedPercentage,
                                CompletedPercentage = kreslt.CompletedPercentage,
                                Status = kreslt.Status
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetKeyObjectiveForDepartment2(int? AssignedTo)
        {
            try
            {
                var data = (from kreslt in _hrms.KeyResults
                            join kobj in _hrms.KeyObjectives
                            on kreslt.KeyObjectiveId equals kobj.Id
                            join dept in _hrms.Departments
                            on kobj.DepId equals dept.Id
                            join emp in _hrms.HrmEmployees
                            on kreslt.AssignedTo equals emp.Id
                            where kreslt.AssignedTo == AssignedTo

                            select new
                            {
                                Id = kreslt.Id,
                                AssignedTo = kreslt.AssignedTo,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineKeyResult = kreslt.DefineKeyResult,
                                AssignDate = kreslt.AssignDate,
                                EndDate = kreslt.EndDate,
                                Priority = kreslt.Priority,
                                AssignedPercentage = kreslt.AssignedPercentage,
                                CompletedPercentage = kreslt.CompletedPercentage,
                                Status = kreslt.Status
                            }).ToList();


                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetKeyObjectiveList()
        {
            try
            {
                var deptName = Session["DeptName"]?.ToString();

                var data = (from cs in _hrms.KeyObjectives.AsEnumerable()
                            join emp in _hrms.HrmEmployees
                            on cs.AssignedTo equals emp.Id
                            join dpt in _hrms.Departments
                            on cs.DepId equals dpt.Id
                            where dpt.Name == Session["DeptName"]?.ToString()
                            select new
                            {
                                Id = cs.Id,
                                AssignedTo = cs.AssignedTo,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineObjective = cs.DefineObjective,
                                AssignDate = cs.AssignDate,
                                EndDate = cs.EndDate,
                                Priority = cs.Priority,
                                AssignedPercentage = cs.AssignedPercentage,
                                CompletedPercentage = cs.CompletedPercentage,
                                Status = cs.Status
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditKeyObjective(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.KeyObjectives.AsEnumerable()
                        join dept in _hrms.Departments
                        on cs.DepId equals dept.Id
                        join emp in _hrms.HrmEmployees
                        on cs.AssignedTo equals emp.Id
                        where cs.Id==id
                        select new
                        {
                            Id = cs.Id,
                            DepId = cs.DepId,
                            AssignedTo = cs.AssignedTo,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            DefineObjective = cs.DefineObjective,
                            AssignDate = cs.AssignDate?.ToString("yyyy-MM-dd"),
                            EndDate = cs.EndDate?.ToString("yyyy-MM-dd"),
                            Status= cs.Status,
                            Priority= cs.Priority,
                            AssignedPercentage = cs.AssignedPercentage,
                            CompletedPercentage = cs.CompletedPercentage,
                            Active = cs.Active
                        }).FirstOrDefault();

            //var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddKeyObjective(KeyObjective obj)
        {
            try
            {
                _hrms.KeyObjectives.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateKeyObjective(KeyObjective obj)
        {

            try
            {
                //obj.Id = 2;
                //var recrod = _hrms.SpecialAllowances.Where(x => x.Id == obj.Id).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.PayRollMonth = obj.PayRollMonth;
                //recrod.Effect = obj.Effect; 
                //recrod.AllowanceType = obj.AllowanceType;
                //recrod.EmployeeId = obj.EmployeeId;

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
        public ActionResult DeleteKeyObjective(int id)
        {
            try
            {
                KeyObjective rg = _hrms.KeyObjectives.Where(x => x.Id == id).FirstOrDefault<KeyObjective>();
                _hrms.KeyObjectives.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion KeyObjective




        #region KeyResults

        public void getKeyObjResultViewModel()
        {
            List<KeyObjResultViewModel> keyObjResultViewModel = new List<KeyObjResultViewModel>();
            Session["KeyObjResultViewModel"] = keyObjResultViewModel;
        }
        public ActionResult KeyResults(string deptName)
        {
            Session["DeptName"] = deptName;

            ViewBag.OutputMsg = "";

            if(TempData["Message"]!= null)
            {
                ViewBag.OutputMsg = TempData["Message"].ToString();

                TempData["Message"] = null;
            }

            var KeyObjResultViewModel = Session["KeyObjResultViewModel"];


            var result = (from q1 in _hrms.KeyResults.AsEnumerable()
                          join q2 in _hrms.KeyObjectives
                          on q1.KeyObjectiveId equals q2.Id
                          join q3 in _hrms.Departments
                          on q2.DepId equals q3.Id
                          join emp in _hrms.HrmEmployees
                          on q2.AssignedTo equals emp.Id


                          where q3.Name == deptName && q2.Active == true && q1.Active == true && q3.Active == true
                          select new KeyObjResultViewModel
                          {
                              Id = q1.Id,
                              KeyObjectiveId = q1.KeyObjectiveId,
                              AssignedPercentage = q1.AssignedPercentage,
                              DefineKeyResult= q1.DefineKeyResult,
                              DefineObjective=q2.DefineObjective,
                              AssignDate=q1.AssignDate,
                              EndDate=q1.EndDate,
                              CompletedPercentage=q1.CompletedPercentage,
                              Priority=q1.Priority,
                              Status=q1.Status,
                              AssignedTo=q1.AssignedTo,
                              DepartmentName=q3.Name,
                              FirstName = emp.FirstName +" "+emp.LastName

                          }).ToList();

            return View(result);
        }

        public ActionResult GetKeyResultsForDepartment(String DepName)
        {
            try
            {
                var data = (from cs in _hrms.KeyResults
                            join dept in _hrms.Departments
                            on cs.KeyObjectiveId equals dept.Id
                            join emp in _hrms.HrmEmployees on cs.AssignedTo equals emp.Id
                            where dept.Name == DepName
                            

                            select new
                            {
                                Id = cs.Id,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineKeyResult = cs.DefineKeyResult,
                                AssignDate = cs.AssignDate,
                                EndDate = cs.EndDate,
                                Priority = cs.Priority,
                                AssignedPercentage = cs.AssignedPercentage,
                                CompletedPercentage = cs.CompletedPercentage,
                            }).ToList();


                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetKeyResultsList()
        {
            try
            {

                var data = (from cs in _hrms.KeyResults
                            join emp in _hrms.HrmEmployees
                            on cs.AssignedTo equals emp.Id

                            select new
                            {
                                Id = cs.Id,
                                AssignedTo = cs.AssignedTo,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                DefineKeyResult = cs.DefineKeyResult,
                                AssignDate = cs.AssignDate,
                                EndDate = cs.EndDate,
                                Priority = cs.Priority,
                                AssignedPercentage = cs.AssignedPercentage,
                                CompletedPercentage = cs.CompletedPercentage,
                                Remarks=cs.Remarks,
                                CompletionDate=cs.CompletionDate,
                                PendingDays=cs.PendingDays
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditKeyResults(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.KeyResults
                        join emp in _hrms.HrmEmployees on cs.AssignedTo equals emp.Id

                        select new
                        {
                            Id = cs.Id,
                            AssignedTo = cs.AssignedTo,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            DefineKeyResult = cs.DefineKeyResult,
                            AssignDate = cs.AssignDate,
                            EndDate = cs.EndDate,
                            Priority = cs.Priority,
                            AssignedPercentage = cs.AssignedPercentage,
                            CompletedPercentage = cs.CompletedPercentage,



                        }).ToList();

            //var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddKeyResults(KeyResult obj)
        {
            try
            {
                _hrms.KeyResults.Add(obj);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                //bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                //if (IsrecExisit != true)
                //{


                //}
                //else
                //{
                //    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateKeyResults(KeyResult obj)
        {

            try
            {
                //obj.Id = 2;
                //var recrod = _hrms.SpecialAllowances.Where(x => x.Id == obj.Id).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.PayRollMonth = obj.PayRollMonth;
                //recrod.Effect = obj.Effect; 
                //recrod.AllowanceType = obj.AllowanceType;
                //recrod.EmployeeId = obj.EmployeeId;

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
        public ActionResult DeleteKeyResults(int id)
        {
            try
            {
                KeyResult rg = _hrms.KeyResults.Where(x => x.Id == id).FirstOrDefault<KeyResult>();
                _hrms.KeyResults.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion KeyResults

        #region EmployeeResults
        public ActionResult EmployeeResults()
        {
            List<Department> RegionsList = _hrms.Departments.ToList();
            var result = RegionsList.Select(S => new Department()
            {
                Id = S.Id,
                Name = S.Name,

            }).ToList();
            ViewBag.deptName = result;

            return View();
        }

        [HttpPost]
        public ActionResult EditEmployeeResults(int? id)
        {

            var data = _hrms.KeyResults.Where(x => x.Id == id).FirstOrDefault();

            return Json(new { Id = data.Id, Active = data.Active, CompletedPercentage = data.CompletedPercentage, DefineKeyResult = data.DefineKeyResult, Status = data.Status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEmployeeResults(KeyResult obj)
        {
            try
            {

                KeyResult originalObj = _hrms.KeyResults.Find(obj.Id);

                originalObj.Active = obj.Active;

                originalObj.CompletedPercentage = obj.CompletedPercentage;

                originalObj.DefineKeyResult = obj.DefineKeyResult;

                originalObj.Status = obj.Status;

                _hrms.Entry(originalObj).State = EntityState.Modified;

                _hrms.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateEmployeeResults(KeyResult obj)
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
        #endregion EmployeeResults


        #region ManagerKeyResult

        public ActionResult ManagerKeyResult()
        {
            List<Department> RegionsList = _hrms.Departments.ToList();
            var result = RegionsList.Select(S => new Department()
            {
                Id = S.Id,
                Name = S.Name,

            }).ToList();
            ViewBag.deptName = result;
            return View();
        }

        public ActionResult GetManagerKeyResultList(int? AssignedTo)
        {
            try
            {
                var ManagerKeyResults = _hrms.ManagerKeyResults.Where(x => x.Active == true && x.EmpId == AssignedTo).ToList();

                var result = ManagerKeyResults.Select(S => new
                {
                    Id = S.Id,
                    DateOfJoining=S.DateOfJoining?.ToString("dd/MMM/yyyy"),
                    GrossSalary=S.GrossSalary,
                    FitForPromotion=S.FitForPromotion,
                    FitInCurrentPossition=S.FitInCurrentPossition,
                    NotFitForPromotionButLikelyToBecome=S.NotFitForPromotionButLikelyToBecome,
                    UnfitForPromotionHasReachedHisCeiling=S.UnfitForPromotionHasReachedHisCeiling,
                    EligibleForBonus=S.EligibleForBonus,
                    EligibleForIncrementPercentage=S.EligibleForIncrementPercentage,
                    Active = S.Active,         
                }).FirstOrDefault();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult EditManagerKeyResult(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var ManagerKeyResultList = _hrms.ManagerKeyResults.Where(x => x.Id == id && x.Active == true).ToList();


            var result = ManagerKeyResultList.Select(x => new
            {
                Active = x.Active,
                DateOfJoining = x.DateOfJoining?.ToString("yyyy-MM-dd"),
                EligibleForBonus  = x.EligibleForBonus,
                EligibleForIncrementPercentage = x.EligibleForIncrementPercentage,
                Id = x.Id,
                EmpId = x.EmpId,
                FitForPromotion = x.FitForPromotion,
                FitInCurrentPossition = x.FitInCurrentPossition,
                GrossSalary = x.GrossSalary,
                NotFitForPromotionButLikelyToBecome = x.NotFitForPromotionButLikelyToBecome,
                UnfitForPromotionHasReachedHisCeiling = x.UnfitForPromotionHasReachedHisCeiling

            }).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddManagerKeyResult(ManagerKeyResult obj)
        {
            try
            {
                 obj.CreatedOn = DateTime.Now;

                _hrms.ManagerKeyResults.Add(obj);

                _hrms.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateManagerKeyResult(ManagerKeyResult obj)
        {      
            try
            {
                ManagerKeyResult ManagerObj = _hrms.ManagerKeyResults.Find(obj.Id);

                ManagerObj.EmpId = obj.EmpId;

                ManagerObj.DateOfJoining = obj.DateOfJoining;

                ManagerObj.GrossSalary = obj.GrossSalary;

                ManagerObj.FitForPromotion = obj.FitForPromotion;

                ManagerObj.FitInCurrentPossition = obj.FitInCurrentPossition;

                ManagerObj.NotFitForPromotionButLikelyToBecome = obj.NotFitForPromotionButLikelyToBecome;

                ManagerObj.EligibleForBonus = obj.EligibleForBonus;

                ManagerObj.EligibleForIncrementPercentage = obj.EligibleForIncrementPercentage;

                ManagerObj.Active = obj.Active;

                ManagerObj.LastModifiedOn = DateTime.Now;

                _hrms.Entry(ManagerObj).State = EntityState.Modified;

                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteManagerKeyResult(int id)
        {
            try
            {
                ManagerKeyResult rg = _hrms.ManagerKeyResults.Where(x => x.Id == id).FirstOrDefault<ManagerKeyResult>();
                _hrms.ManagerKeyResults.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult EmployeeChangeArrear(long JobId)
        {
            var data = (from emp in _hrms.HrmEmployees

                        join dsg in _hrms.Designations on emp.DesignationId equals dsg.Id
                        join dpt in _hrms.Departments on emp.DepartmentId equals dpt.Id
                        where emp.Id == JobId
                        select new
                        {

                            EmployeeNumber = emp.EmployeeCode,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            Designation = dsg.Name,
                            Department = dpt.Name,

                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Employeesddl()
        {
            var deptName = Session["DeptName"]?.ToString();

            List<HrmEmployee> empList = _hrms.HrmEmployees.Include(x=>x.Department).Where(y=>y.Department.Name == deptName).ToList();

            var data = (from emp in _hrms.HrmEmployees
                        join dpt in _hrms.Departments 
                        on emp.DepartmentId equals dpt.Id
                        where dpt.Name == deptName
                        select new
                        {
                            Id = emp.Id,
                            Name = emp.FirstName + " " + emp.LastName
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllEmployesForModalEdit()
        {
            // var data = _hrms.HrmEmployees.Where(x => x.Active == true).ToList();

            List<HrmEmployee> empList = _hrms.HrmEmployees.Where(x => x.Active == true).ToList<HrmEmployee>();

            var result = empList.Select(S => new
            {
                Id = S.Id,
                Name = S.FirstName + " " + S.LastName
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion ManagerKeyResult
    }
}