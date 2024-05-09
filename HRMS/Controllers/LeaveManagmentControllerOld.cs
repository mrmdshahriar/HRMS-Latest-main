using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using HRMS.Models;
using LinqToExcel;
using Newtonsoft.Json;

namespace HRMS.Controllers
{
    public class LeaveManagmentController : Controller
    {
        // GET: LeaveManagment

        Models.HRMS _hrms = new Models.HRMS();

        #region Leave Type
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoCodeGenrate()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.LeaveTypes.Where(x => x.IsActive == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.Code))

            {
                stringCode = LastCode.Code.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.Code.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "LT-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LeaveTypeList()
        {
            try
            {
                var Result = _hrms.LeaveTypes.Select(s => s).ToList();

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult EditLeaveType(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.LeaveTypes.Where(x => x.Id == id).FirstOrDefault<LeaveType>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { throw ex; }

        }

        [HttpPost]
        public ActionResult AddLeaveType(LeaveType obj)
        {
            try
            {
                bool IsrecExisit = _hrms.LeaveTypes.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.LeaveTypes.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Leave Type is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateJLeaveType(LeaveType obj)
        {
            try
            {
                _hrms.Entry(obj).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Updated Successfully",
                    JsonRequestBehavior.AllowGet
                });
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult DeleteLeaveType(int id)
        {
            try
            {
                LeaveType leave = _hrms.LeaveTypes.Where(x => x.Id == id).FirstOrDefault<LeaveType>();
                _hrms.LeaveTypes.Remove(leave);
                _hrms.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Deleted Successfully",
                    JsonRequestBehavior.AllowGet
                });
            }
            catch (Exception ex) { throw ex; }
        }


        #endregion Leave Type End

        #region start Leave Request
        public ActionResult LeaveRequest()
        {

            return View();
        }


        public ActionResult LeaveRequestList()
        {
            try
            {

                //var LeaveRequest = _hrms.LeaveRequests.Select(s => s).ToList();
                //var LeaveType = _hrms.LeaveTypes.Select(s => s).ToList();
                ////var employee = _hrms.HrmEmployees.Where(x => x.Active == true).ToList();
                //var employee = _hrms.HrmEmployees.Select(s => s.Active == true).ToList();





                //    var Result = from st in LeaveRequest
                //                 join d in LeaveType on st.LeaveTypeId equals d.Id into table1
                //                 from d in table1.ToList()
                //                 join i in employee on st.Employee equals i.Id into table2
                //                 from i in table2.ToList()

                //                 select new
                //                 {
                //                     Id = st.Pk_LeaveRequest,
                //                     Name = d.Name,
                //                     DateFrom = st.DateFrom.ToString(),
                //                     DateTo = st.DateTo.ToString(),
                //                     Active = st.IsActive,
                //                     LeaveDays = st.LeaveDays,
                //                     Reason = st.Reason,
                //                     EmployeeName = i.FirstName,
                //                     Status = st.Status

                //                 };
                //    return Json(Result, JsonRequestBehavior.AllowGet);
                //}


                var Result = from st in _hrms.LeaveRequests
                             join d in _hrms.LeaveTypes on st.LeaveTypeId equals d.Id into table1
                             from d in table1.ToList()
                             join i in _hrms.HrmEmployees on st.Employee equals i.Id into table2
                             from i in table2.ToList()

                             select new
                             {
                                 Id = st.Pk_LeaveRequest,
                                 Name = d.Name,
                                 DateFrom = st.DateFrom.ToString(),
                                 DateTo = st.DateTo.ToString(),
                                 Active = st.IsActive,
                                 LeaveDays = st.LeaveDays,
                                 Reason = st.Reason,
                                 EmployeeName = i.FirstName,
                                 Status = st.Status


                             };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult LeaveTypeEdit(int id)
        {
            try
            {
                var Result = _hrms.LeaveRequests.Where(x => x.Pk_LeaveRequest == id).FirstOrDefault<LeaveRequest>();

                string DateFrom = Result.DateFrom.ToString();
                Result.CreatedBy = DateFrom;
                string dateto = Result.DateTo.ToString();
                Result.LastModifiedBy = dateto;
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AddLeaveRequest()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddLeaveRequest(LeaveRequest request)
        {
            try
            {
                var EmployeeDataList = _hrms.LeaveRequests.Where(x => x.Employee == request.Employee).ToList();

                foreach(var x in EmployeeDataList)
                {
                    if(request.DateFrom == x.DateFrom && request.DateTo == x.DateTo && ( x.Status != "Rejected" ||x.Status == "Pending" || x.Status == "Approved"|| x.Status == "Adjustment"))
                    {
                        return Json(new { success = false, message = "Sorry! you are unable to apply more than once.", JsonRequestBehavior.AllowGet });
                    }

                    if (request.DateFrom >= x.DateFrom && request.DateTo <= x.DateTo && ( x.Status != "Rejected" || x.Status == "Pending" || x.Status == "Approved" || x.Status == "Adjustment"))
                    {
                        return Json(new { success = false, message = "Sorry! you are unable to apply more than once.", JsonRequestBehavior.AllowGet });
                    }
                }


                var statu = "Pending";
                request.Status = statu;
                _hrms.LeaveRequests.Add(request);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult UpdateLeaveRequest(LeaveRequest request)
        {
            try
            {

                _hrms.Entry(request).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult ApprovedLeaveRequest(LeaveRequest request)
        {
            try
            {
                string status = "Approved";
                request.Status = status;
                _hrms.Entry(request).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult RejectedLeaveRequest(LeaveRequest request)
        {
            try
            {
                string status = "Rejected";
                request.Status = status;
                _hrms.Entry(request).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AdjustmentLeaveRequest(LeaveRequest request)
        {
            try
            {
                string status = "Adjustment";
                request.Status = status;
                _hrms.Entry(request).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteLeaveRequest(int id)
        {
            try
            {
                LeaveRequest leave = _hrms.LeaveRequests.Where(x => x.Pk_LeaveRequest == id).FirstOrDefault<LeaveRequest>();
                _hrms.LeaveRequests.Remove(leave);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion End Requset leave




        #region start Leave Status
        public ActionResult LeaveStatus()
        {

            return View();
        }


        [HttpGet]
        public ActionResult AddLeaveStatus()
        {
            return View();
        }

        #endregion End Leave Status

        #region LeavePolicy




        public ActionResult LeavePolicy()
        {
            return View();
        }



        public ActionResult GetLeavePolicyList()
        {
            try
            {

                var data = (from cs in _hrms.LeavePolicies
                            join emp in _hrms.LeaveTypes on cs.LeaveTypeId equals emp.Id

                            select new
                            {
                                Id = cs.Id,
                                LeaveTypeId = cs.LeaveTypeId,
                                Name = emp.Name,
                                LeaveDays = cs.LeaveDays
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditLeavePolicy(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var data = (from cs in _hrms.LeavePolicies
                        join emp in _hrms.LeaveTypes on cs.LeaveTypeId equals emp.Id

                        select new
                        {
                            Id = cs.Id,
                            LeaveTypeId = cs.LeaveTypeId,
                            Name = emp.Name,
                            LeaveDays = cs.LeaveDays
                        }).ToList();

            var result = _hrms.Arrears.Where(x => x.Id == id).FirstOrDefault<Arrear>();
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddLeavePolicy(LeavePolicy obj)
        {
            try
            {
                _hrms.LeavePolicies.Add(obj);
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
        public ActionResult UpdateLeavePolicy(LeavePolicy obj)
        {
            obj.Id = 1;

            try
            {
                ////obj.Id = 2;
                //var recrod = _hrms.EmployeeCosts.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                //recrod.Amount = obj.Amount;
                //recrod.Reason = obj.Reason;
                //recrod.DeductionMode = obj.DeductionMode;
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
        public ActionResult DeleteLeavePolicy(int id)
        {
            try
            {
                LeavePolicy rg = _hrms.LeavePolicies.Where(x => x.Id == id).FirstOrDefault<LeavePolicy>();
                _hrms.LeavePolicies.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion LeavePolicy




        #region Rejoining

        public ActionResult Rejoining()
        {
            return View();
        }

        public ActionResult RejoiningData(int? employeeId)
        {
            var EmployeeDataObj = _hrms.LeaveRequests.Where(x => x.Employee == employeeId).OrderByDescending(x => x.DateTo).FirstOrDefault();


            return Json(new { success = true, message = "Ok", EmployeeDataObj = EmployeeDataObj }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReJoinigSave(LeaveRequest request)
        {
            try
            {
                var LeaveRequestObj = _hrms.LeaveRequests.Where(x => x.Pk_LeaveRequest == request.Pk_LeaveRequest).FirstOrDefault();

                LeaveRequestObj.RejoiningDate = request.RejoiningDate;

                LeaveRequestObj.ExtraDays = request.ExtraDays;

                _hrms.Entry(LeaveRequestObj).State = EntityState.Modified;

                _hrms.SaveChanges();

                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, JsonRequestBehavior.AllowGet });
            }
        }





        #endregion Rejoining

        public ActionResult CheckAnnualLeavesAvailable(int employeeId, string leaveType)
        {
            HrmEmployee EmployeeObj = _hrms.HrmEmployees.Where(x => x.Id == employeeId).FirstOrDefault();

            double eligibilityDays = 0;

            if (EmployeeObj.ProbationPeriod != null)
            {
                DateTime ProbationEndDate = (DateTime)EmployeeObj.dteJoiningDate?.Date.AddMonths(EmployeeObj.ProbationPeriod);


                int employeeWorkedDays = (DateTime.Now - EmployeeObj.dteJoiningDate).Value.Days;

                int employeeWorkedMonths = employeeWorkedDays / 30;

                double tenure = ((double)employeeWorkedDays / 365);


                if (DateTime.Now.Date <= ProbationEndDate)
                {
                    return Json(new { success = false, message = "Sorry! Not Eligible For Annual Leave, Apply Unpaid Leaves" }, JsonRequestBehavior.AllowGet);
                }

                if(leaveType.Trim() == "Annual Leave")
                {      
                    if(tenure < 1)
                    {
                        eligibilityDays = employeeWorkedMonths * 2;
                    }
                    if (tenure >= 1)
                    {
                        eligibilityDays = employeeWorkedMonths * 2.5;
                    }
                }

                if (leaveType.Trim() == "Sick Leave")
                {
                    var SickLeavePolicyDaysObj = (from lvp in _hrms.LeavePolicies
                                                  join lvtyp in _hrms.LeaveTypes
                                                  on lvp.LeaveTypeId equals lvtyp.Id
                                                  where lvtyp.Name == "Sick Leave"
                                                  select lvp).FirstOrDefault();

                    var SickPolicyDays = SickLeavePolicyDaysObj.LeaveDays;

                    eligibilityDays = SickPolicyDays.Value;
                }

                if (leaveType.Trim() == "Maternity Leave")
                {
                    if(EmployeeObj.Gender != "Female")
                    {
                        return Json(new { success = false, message = "Sorry! Only Females Are Eligible For Maternity Leave" }, JsonRequestBehavior.AllowGet);
                    }

                     var MaternityPolicyDaysObj = (from lvp in _hrms.LeavePolicies
                                                   join lvtyp in _hrms.LeaveTypes
                                                   on lvp.LeaveTypeId equals lvtyp.Id
                                                   where lvtyp.Name == "Maternity Leave"
                                                   select lvp).FirstOrDefault();

                    var MaternityPolicyDays = MaternityPolicyDaysObj.LeaveDays;

                    if (tenure < 1)
                    {
                        eligibilityDays = ((double)MaternityPolicyDays / 12.0) * employeeWorkedMonths;
                    }
                    if (tenure >= 1)
                    {
                        eligibilityDays = MaternityPolicyDays.Value;
                    }
                }
            }

            return Json(new { success = true, message = "Ok", EligibilityDays = eligibilityDays }, JsonRequestBehavior.AllowGet);
        }


            [HttpGet]
        public ActionResult Leaveddl()
        {
            // var data = _hrms.HrmEmployees.Where(x => x.Active == true).ToList();

            var dd = _hrms.LeaveTypes.Where(x => x.IsActive == true).FirstOrDefault();
            List<LeaveType> empList = _hrms.LeaveTypes.ToList<LeaveType>();
            var result = empList.Select(S => new
            {
                Id = S.Id,
                Name = S.Name
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult HolyDay()
        {
            return View();
        }



        public ActionResult HolyDaysList()
        {
            try
            {
                var result = _hrms.PublicHolidays.Select(s => s).ToList();

                var Result = from st in result
                             select new
                             {
                                 Id = st.Pk_PublicId,
                                 Name = st.Name,
                                 DateFrom = st.DateFrom.ToString(),
                                 DateTo = st.DateTo.ToString(),
                                 Active = st.IsActive,
                                 LeaveDays = st.LeaveDays,
                                 Color = st.Color
                             };
                //string date

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult HolyDaysEdit(int id)
        {
            try
            {
                var Result = _hrms.PublicHolidays.Where(x => x.Pk_PublicId == id).FirstOrDefault<PublicHoliday>();
                string DateFrom = Result.DateFrom.ToString();
                Result.CreatedBy = DateFrom;
                string dateto = Result.DateTo.ToString();
                Result.LastModifiedBy = dateto;
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult AddHolyDays()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHolyDays(PublicHoliday holiday)
        {
            try
            {
                _hrms.PublicHolidays.Add(holiday);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UpdatePublicHolyday(PublicHoliday holiday)
        {
            try
            {
                _hrms.Entry(holiday).State = EntityState.Modified;
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult getEmployeeList()
        {
            try {
                IList<HrmEmployee> IList = new List<HrmEmployee>();
                var emp = _hrms.HrmEmployees.Where(x => x.FirstName != "").ToList();
                foreach (var item in emp)
                {
                    HrmEmployee ObjModel = new HrmEmployee();
                    ObjModel.Id = item.Id;
                    ObjModel.TitleId = item.TitleId;
                    ObjModel.FirstName = item.FirstName;
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
                   // ObjModel.dteJoiningDate = item.dteJoiningDate;
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
                return Json(result, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                throw ex;
            }
            }
    }
}