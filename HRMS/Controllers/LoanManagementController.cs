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
    public class LoanManagementController : Controller
    {

        Models.HRMS _hrms = new Models.HRMS();
        // GET: LoanManagement
        public ActionResult Index()
        {
            return View();
        }

        #region LoanSanctions start

        public ActionResult LoanSanction()
        {

            return View();
        }

        //public ActionResult AutoCodeGenrateLoanSanction()
        //{
        //    var stringCode = string.Empty;
        //    _hrms.Configuration.ProxyCreationEnabled = false;
        //    //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
        //    var LastCode = _hrms.LoanSanctions.Where(x => x.Active == true).ToList().LastOrDefault();
        //    if (LastCode != null && !string.IsNullOrEmpty(LastCode.LoanAmount))

        //    {
        //        stringCode = LastCode.Code.Substring(0, 3);
        //        int intCode = Convert.ToInt16(LastCode.Code.Substring(3));
        //        intCode++;
        //        var threeDidgit = intCode.ToString("D2"); // = "001"
        //        stringCode += threeDidgit;
        //    }
        //    else
        //    {
        //        stringCode = "Rg-" + 1.ToString("D2");
        //    }
        //    var Code = stringCode;
        //    return Json(Code, JsonRequestBehavior.AllowGet);
        //    // return View(Code);
        //    //Common employeeDropDowns = new Common
        //    //{

        //    //    Code = stringCode
        //    //};


        //    //return View(employeeDropDowns);
        //}

        public ActionResult GetLoanSanctionList()
        {
            try
            {
                var data = (from cs in _hrms.LoanSanctions.AsEnumerable()
                            join emp in _hrms.HrmEmployees on cs.EmployeeId equals emp.Id
                            where cs.EmployeeId == emp.Id
                            //where cs.Active == true && emp.Active == true
                            select new
                            {
                                Id = cs.Id,
                                EmployeeId = cs.EmployeeId,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                LoanAmount = cs.LoanAmount,
                                DateIssued = cs.DateIssued?.ToString("yyyy-MMM-dd"),
                                LoanDeductionStartDate = cs.LoanDeductionStartDate?.ToString("yyyy-MMM-dd"),
                                TentativeReturnMonth = cs.TentativeReturnMonth,
                                EmiCalculation=cs.EmiCalculation,
                                //LoanDefermentDate=cs.LoanDefermentDate?.ToString("yyyy-MMM-dd"),
                                Active = cs.Active
                            }).ToList();

                var result = data.Where(x => x.Active == true).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditLoanSanction(int id)
        {
            //try
            //{
            //    var stringCode = string.Empty;
            //    _hrms.Configuration.ProxyCreationEnabled = false;

            //    var data = _hrms.LoanSanctions.Where(x => x.Id == id).FirstOrDefault<LoanSanction>();
            //    object obj = new
            //    {
            //        data,
            //        DateIssued = data.DateIssued?.ToString("yyyy-MM-dd"),
            //        TentativeReturnDate = data.TentativeReturnDate?.ToString("yyyy-MM-dd"),
            //        LoanDefermentDate = data.LoanDefermentDate?.ToString("yyyy-MM-dd"),

            //    };
            //    JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //    var result = JsonConvert.SerializeObject(obj, jss);
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}
            //catch(Exception e)
            //{
            //    return Json(e, JsonRequestBehavior.AllowGet);
            //}

           // _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.LoanSanctions.Where(x => x.Id == id).AsEnumerable().Select(x => new {

                EmployeeId = x.EmployeeId,
                LoanAmount = x.LoanAmount,
                DateIssued = x.DateIssued?.ToString("yyyy-MM-dd"),
                LoanDeductionStartDate = x.LoanDeductionStartDate?.ToString("yyyy-MM-dd"),
                TentativeReturnMonth = x.TentativeReturnMonth,
                EmiCalculation = x.EmiCalculation,
                //LoanDefermentDate = x.LoanDefermentDate?.ToString("yyyy-MM-dd"),
                Active = x.Active

            }).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddLoanSanction(LoanSanction obj)
        {
            var emp =   _hrms.HrmEmployees.Where(x => x.Id == obj.EmployeeId).FirstOrDefault();


            if((double)obj.LoanAmount > emp.BasicSalary*3)
            {
                return Json(new { success = false, message = " Sorry! Your Loan Amount is out of range ", JsonRequestBehavior.AllowGet });

            }


           if((double)obj.LoanAmount > emp.BasicSalary*1 && obj.TentativeReturnMonth ==1)
            {
                
                return Json(new { success = false, message = " Sorry! Your Loan Amount will not return in a month ", JsonRequestBehavior.AllowGet });

            }
           if((double)obj.LoanAmount > emp.BasicSalary*2 && obj.TentativeReturnMonth == 2)
            {
                return Json(new { success = false, message = " Sorry! Your Loan Amount will not return in Two months ", JsonRequestBehavior.AllowGet });

            }
           if((double)obj.LoanAmount > emp.BasicSalary*3 && obj.TentativeReturnMonth == 3)
            {
                return Json(new { success = false, message = " Sorry! Your Loan Amount Will not return in 3 months ", JsonRequestBehavior.AllowGet });

            }
           

            try
            {
                if ((double)obj.LoanAmount < emp.BasicSalary * 1 && obj.TentativeReturnMonth >= 1)
                {
                    obj.EmiCalculation = obj.LoanAmount ;

                }
                if ((double)obj.LoanAmount < emp.BasicSalary * 2 && obj.TentativeReturnMonth >= 2)
                {
                    obj.EmiCalculation =  obj.LoanAmount ;
                }
                if ((double)obj.LoanAmount < emp.BasicSalary * 3 && obj.TentativeReturnMonth >= 3)
                {
                    obj.EmiCalculation = obj.LoanAmount ;
                }
                if ((double)obj.LoanAmount == emp.BasicSalary * 3 && obj.TentativeReturnMonth >= 3)
                {
                    obj.EmiCalculation = 30 * obj.LoanAmount / 100;
                }

                bool IsrecExisit = _hrms.LoanSanctions.Any(x => x.Id == obj.Id);

                if (IsrecExisit != true)
                {
                    
                    obj.CreatedOn = DateTime.Now;
                    _hrms.LoanSanctions.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = " This Record is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateLoanSanction(LoanSanction obj)
        {
            try
            {
                obj.LastModifiedOn = DateTime.Now;

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
        public ActionResult DeleteLoanSanction(int id)
        {
            try
            {
                LoanSanction rg = _hrms.LoanSanctions.Where(x => x.Id == id).FirstOrDefault<LoanSanction>();
                _hrms.LoanSanctions.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public ActionResult EmployeeChangeSalarySetup(long JobId)
        //{
        //    var data = (from emp in _hrms.HrmEmployees

        //                join loan in _hrms.LoanSanctions on emp.Id equals loan.EmployeeId

        //                where emp.Id == JobId
        //                select new
        //                {
        //                    //EmployeeNumber = emp.EmployeeCode,
        //                    //EmployeeName = emp.FirstName + " " + emp.LastName,
        //                    //Designation = dsg.Name,
        //                    //Department = dpt.Name,
        //                    //BasicSalary = emp.BasicSalary



        //                }).ToList();

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}




        #endregion LoanSanctions



    }
}


