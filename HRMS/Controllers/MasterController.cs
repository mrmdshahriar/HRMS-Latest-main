using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using Newtonsoft.Json;

namespace HRMS.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        Models.HRMS _hrms = new Models.HRMS();
        public ActionResult Index()
        {
            return View();
        }

        #region Regions Start

        public ActionResult Regions()
        {
            return View();
        }
        public ActionResult AutoCodeGenrate()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.Regions.Where(x => x.Active == true).ToList().LastOrDefault();
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
                stringCode = "Rg-" + 1.ToString("D2");
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

        public ActionResult GetRegionsList()
        {
            try
            {


                var dd = _hrms.Regions.Where(x => x.Active == true).FirstOrDefault();
                List<Region> RegionsList = _hrms.Regions.ToList<Region>();
                var result = RegionsList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    Code = S.Code,
                    Active = S.Active
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditRegion(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.Regions.Where(x => x.Id == id).FirstOrDefault<Region>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddRegion(Region obj)
        {
            try
            {


                bool IsrecExisit = _hrms.Regions.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {

                    _hrms.Regions.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateRegion(Region obj)
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
        public ActionResult Delete(int id)
        {
            try
            {
                Region rg = _hrms.Regions.Where(x => x.Id == id).FirstOrDefault<Region>();
                _hrms.Regions.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Rigons


        #region TradeLicense
        public ActionResult AutoCodeTradeLicense()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.TradeLicenses.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.MOLCode))

            {
                stringCode = LastCode.MOLCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.MOLCode.Substring(3));
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
        public ActionResult TradeLicense()
        {
            //try
            //{


            //var record = _hrms.TradeLicenses.Where(s => s.Active == true).ToList();
            //ViewBag.TradeLicense = record;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return View();
        }
        public ActionResult GetTradeLicenseList()
        {
            try
            {
                var dd = _hrms.Regions.Where(x => x.Active == true).FirstOrDefault();
                var TradeLicenseList = _hrms.TradeLicenses.Select(x => x).ToList();
                var result = TradeLicenseList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    Code = S.MOLCode,
                    Active = S.Active,
                    TradeLicenseExpiry = S.TradeLicenseExpiry.ToString(),
                    MOLExpiry = S.MOLExpiry.ToString(),
                    VisaQuotaAvailability = S.VisaQuotaAvailability
                });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult EditTradeLicense(int id)
        {
           
            //var result = _hrms.TradeLicenses.Where(x => x.Id == id).FirstOrDefault<TradeLicense>();
            //return Json(result, JsonRequestBehavior.AllowGet);

            try
            {
                var stringCode = string.Empty;
                _hrms.Configuration.ProxyCreationEnabled = false;
                var data = _hrms.TradeLicenses
                       .Where(x => x.Id == id).FirstOrDefault();
                object obj = new
                {
                    data,
                    MOLExpiry = data.MOLExpiry?.ToString("yyyy-MM-dd"),
                    TradeLicenseExpiry = data.TradeLicenseExpiry?.ToString("yyyy-MM-dd"),
                  
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
        public ActionResult AddTradeLicense(TradeLicense obj)
        {
            try
            {


                bool IsrecExisit = _hrms.TradeLicenses.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {

                    _hrms.TradeLicenses.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "TradeLicenses is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateTradeLicense(TradeLicense obj)
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
        public ActionResult DeleteTradeLicense(int id)
        {
            try
            {
                TradeLicense rg = _hrms.TradeLicenses.Where(x => x.Id == id).FirstOrDefault<TradeLicense>();
                _hrms.TradeLicenses.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion  TradeLicense End

        #region Country Start
        public ActionResult Country()
        {
            return View();
        }

        public ActionResult AutoCodeGenrateCountry()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.Countries.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.CountryCode))

            {
                stringCode = LastCode.CountryCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.CountryCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "CT-" + 1.ToString("D2");
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

        [HttpGet]
        public ActionResult GetCountryList()
        {
            try
            {
                var CountriesList = _hrms.Countries.Select(x => x).ToList();
                var RegionsList = _hrms.Regions.Select(x => x).ToList();
                var innerjoin = from s in CountriesList
                                join st in RegionsList on s.RegionId equals st.Id into table1
                                from st in table1.ToList()
                                select new
                                { // result selector
                                    Id = s.Id,
                                    Name = s.Name,
                                    RegionId = s.RegionId,
                                    Active = s.Active,
                                    RegionName = st.Name,
                                    Code = s.CountryCode
                                };
                return Json(innerjoin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditCountry(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = _hrms.Countries.Where(x => x.Id == id).FirstOrDefault<Country>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddCountry(Country obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Countries.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Countries.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Country Name is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateCountry(Country obj)
        {

            _hrms.Entry(obj).State = EntityState.Modified;
            _hrms.SaveChanges();

            return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult DeleteCountry(int id)
        {
            try
            {
                Country ct = _hrms.Countries.Where(x => x.Id == id).FirstOrDefault<Country>();
                _hrms.Countries.Remove(ct);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion  Country End

        #region State Start
        public ActionResult State()
        {
            return View();
        }
        public ActionResult GetStateList()
        {
            try
            {
                var StatesList = _hrms.States.Select(x => x).ToList();
                var CountriesList = _hrms.Countries.Select(x => x).ToList();
                var innerjoin = from s in StatesList
                                join st in CountriesList on s.CountryId equals st.Id into table1
                                from st in table1.ToList()
                                select new
                                { // result selector
                                    Id = s.Id,
                                    Name = s.Name,
                                    CountryId = s.CountryId,
                                    Active = s.Active,
                                    CountryName = st.Name
                                };

                return Json(innerjoin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditState(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = _hrms.States.Where(x => x.Id == id).FirstOrDefault<State>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddState(State obj)
        {
            try
            {
                bool IsrecExisit = _hrms.States.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.States.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "State Name is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateState(State obj)
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
        public ActionResult DeleteState(int id)
        {
            try
            {
                State st = _hrms.States.Where(x => x.Id == id).FirstOrDefault<State>();
                _hrms.States.Remove(st);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion  State End

        #region City Start

        public ActionResult City()
        {
            return View();
        }
        public ActionResult GetCityList()
        {
            try
            {
                var CitiesList = _hrms.Cities.Select(x => x).ToList();
                var StatesList = _hrms.States.Select(x => x).ToList();
                var innerjoin = from st in CitiesList
                                join s in StatesList on st.StateId equals s.Id into table1
                                from s in table1.ToList()
                                select new
                                { // result selector
                                    Id = st.Id,
                                    Name = st.Name,
                                    StateId = st.StateId,
                                    Active = st.Active,
                                    StateName = s.Name
                                };

                return Json(innerjoin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult EditCity(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = _hrms.Cities.Where(x => x.Id == id).FirstOrDefault<City>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddCity(City obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Cities.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Cities.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "City Name is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateCity(City obj)
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
        public ActionResult DeleteCity(int id)
        {
            try
            {
                City ct = _hrms.Cities.Where(x => x.Id == id).FirstOrDefault<City>();
                _hrms.Cities.Remove(ct);
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
        #endregion  City End

        #region Job Type Start

        public ActionResult JobType()
        {
            return View();
        }

        public ActionResult AutoCodeGenrateJobType()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.HrmJobTypes.Where(x => x.Active == true).ToList().LastOrDefault();
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
                stringCode = "JT-" + 1.ToString("D2");
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
        public ActionResult GetJobTypeList()
        {
            try
            {
                List<HrmJobType> JobTypeList = _hrms.HrmJobTypes.ToList<HrmJobType>();
                var result = JobTypeList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    Active = S.Active,
                    Code = S.Code

                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditJobType(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.HrmJobTypes.Where(x => x.Id == id).FirstOrDefault<HrmJobType>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult AddJobType(HrmJobType obj)
        {
            try
            {
                bool IsrecExisit = _hrms.HrmJobTypes.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.HrmJobTypes.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Job Type is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateJobType(HrmJobType obj)
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
        public ActionResult DeleteJobType(int id)
        {
            try
            {
                HrmJobType hr = _hrms.HrmJobTypes.Where(x => x.Id == id).FirstOrDefault<HrmJobType>();
                _hrms.HrmJobTypes.Remove(hr);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Job Type End


        #region Hrm Skills Start

        public ActionResult HrmSkillse()
        {
            return View();
        }

        public ActionResult GetHrmSkillsList()
        {
            try
            {
                var Res = _hrms.HrmSkills.Select(s => s).ToList();

                return Json(Res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditHrmSkills(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;
                var result = _hrms.HrmSkills.Where(x => x.Id == id).FirstOrDefault<HrmSkill>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult AddHrmSkills(HrmSkill obj)
        {
            try
            {
                bool IsrecExisit = _hrms.HrmSkills.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.HrmSkills.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "Saved Successfully",
                        JsonRequestBehavior.AllowGet
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Skill is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateHrmSkills(HrmSkill obj)
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
        public ActionResult DeleteHrmSkills(int id)
        {
            try
            {
                HrmSkill sk = _hrms.HrmSkills.Where(x => x.Id == id).FirstOrDefault<HrmSkill>();
                _hrms.HrmSkills.Remove(sk);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Hrm Skills End

        #region Designation Start

        public ActionResult Designation()
        {
            return View();
        }

        public ActionResult AutoCodeGenrateDesignation()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.Designations.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.DesignationCode))

            {
                stringCode = LastCode.DesignationCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.DesignationCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "DS-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDesignationList()
        {
            try
            {
                List<Designation> DesignationList = _hrms.Designations.ToList<Designation>();
                var result = DesignationList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    DesignationCode = S.DesignationCode,
                    Active = S.Active

                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditDesignation(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Designations.Where(x => x.Id == id).FirstOrDefault<Designation>();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddDesignation(Designation obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Designations.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Designations.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Designation is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateDesignation(Designation obj)
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
        public ActionResult DeleteDesignation(int id)
        {
            try
            {
                Designation ds = _hrms.Designations.Where(x => x.Id == id).FirstOrDefault<Designation>();
                _hrms.Designations.Remove(ds);
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

        #endregion Designation End


        #region Department Start

        public ActionResult Department()
        {
            return View();
        }
        public ActionResult AutoCodeGenrateDepartment()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.Departments.Where(x => x.Active == true).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.DepartmentCode))

            {
                stringCode = LastCode.DepartmentCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.DepartmentCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "DT-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDepartmentList()
        {
            try
            {
                List<Department> DepartmentList = _hrms.Departments.ToList<Department>();
                var result = DepartmentList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    DepartmentCode = S.DepartmentCode,
                    Active = S.Active
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditDepartment(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Departments.Where(x => x.Id == id).FirstOrDefault<Department>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpPost]
        public ActionResult AddDepartment(Department obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Departments.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Departments.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Department is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateDepartment(Department obj)
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
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                Department ds = _hrms.Departments.Where(x => x.Id == id).FirstOrDefault<Department>();
                _hrms.Departments.Remove(ds);
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

        #endregion Department End


        #region EmployeeGroup Start

        public ActionResult EmployeeGroup()
        {
            return View();
        }
        public ActionResult AutoCodeGenrateEmployeeGroup()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.EmployeeGroups.Where(x => x.IsActive == true).ToList().LastOrDefault();
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
                stringCode = "EG-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEmployeeGroupList()
        {
            try
            {
                List<EmployeeGroup> EmployeeGroupList = _hrms.EmployeeGroups.ToList<EmployeeGroup>();
                var result = EmployeeGroupList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    Code = S.Code,
                    Active = S.IsActive
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditEmployeeGroup(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.EmployeeGroups.Where(x => x.Id == id).FirstOrDefault<EmployeeGroup>();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddEmployeeGroup(EmployeeGroup obj)
        {
            try
            {
                bool IsrecExisit = _hrms.EmployeeGroups.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.EmployeeGroups.Add(obj);
                    _hrms.SaveChanges();

                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "EmployeeGroups is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateEmployeeGroup(EmployeeGroup obj)
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
        public ActionResult DeleteEmployeeGroup(int id)
        {
            try
            {
                EmployeeGroup emp = _hrms.EmployeeGroups.Where(x => x.Id == id).FirstOrDefault<EmployeeGroup>();
                _hrms.EmployeeGroups.Remove(emp);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion EmployeeGroup End

        #region Organization Start

        public ActionResult Organization()
        {
            return View();
        }


        public ActionResult AutoCodeGenrateOrganization()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.Organizations.Select(x => x).ToList().LastOrDefault();
            if (LastCode != null && !string.IsNullOrEmpty(LastCode.ShortCode))

            {
                stringCode = LastCode.ShortCode.Substring(0, 3);
                int intCode = Convert.ToInt16(LastCode.ShortCode.Substring(3));
                intCode++;
                var threeDidgit = intCode.ToString("D2"); // = "001"
                stringCode += threeDidgit;
            }
            else
            {
                stringCode = "OS-" + 1.ToString("D2");
            }
            var Code = stringCode;
            return Json(Code, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetOrganizationList()
        {
            try
            {
                List<Organization> OrganizationList = _hrms.Organizations.ToList<Organization>();
                var result = OrganizationList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    ShortCode = S.ShortCode
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult EditOrganization(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Organizations.Where(x => x.Id == id).FirstOrDefault<Organization>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult AddOrganization(Organization obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Organizations.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Organizations.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Organization is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateOrganization(Organization obj)
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
        public ActionResult DeleteOrganization(int id)
        {
            try
            {
                Organization ds = _hrms.Organizations.Where(x => x.Id == id).FirstOrDefault<Organization>();
                _hrms.Organizations.Remove(ds);
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

        #endregion Organization End


        #region Cost
        public ActionResult Cost()
        {
            return View();
        }

        public ActionResult AutoCodeCost()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.CostingTabs.Where(x => x.Active == true).ToList().LastOrDefault();
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
                stringCode = "Rg-" + 1.ToString("D2");
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

        public ActionResult GetCostList()
        {
            try
            {


                //var dd = _hrms.CostingTabs.Where(x => x.Active == true).FirstOrDefault();
                //List<CostingTab> CostList = _hrms.CostingTabs.ToList<CostingTab>();
                //var result = CostList.Select(S => new
                //{
                //    Id = S.Id,
                //    Name = S.Name,
                //    Code = S.Code,
                //    Active = S.Active
                //});
                var result = _hrms.CostingTabs.Where(x => x.Active == true).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditCost(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.CostingTabs.Where(x => x.Id == id).FirstOrDefault<CostingTab>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddCost(CostingTab obj)
        {
            try
            {
                bool IsrecExisit = _hrms.CostingTabs.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {

                    _hrms.CostingTabs.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateCost(CostingTab obj)
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
        public ActionResult DeleteCost(int id)
        {
            try
            {
                CostingTab rg = _hrms.CostingTabs.Where(x => x.Id == id).FirstOrDefault<CostingTab>();
                _hrms.CostingTabs.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AutoCodeGenrateTabCost()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.CostingTabs.Where(x => x.Active == true).ToList().LastOrDefault();
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
                stringCode = "TC-" + 1.ToString("D2");
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

        #endregion Cost


        #region Deduction

        public ActionResult Deduction()
        {
            return View();
        }

        

        public ActionResult GetDeductionList()
        {
            try
            {


                var record = (from d in _hrms.Deductions.AsEnumerable()
                              join e in _hrms.HrmEmployees on d.EmployeeId equals e.Id
                              select new
                              {
                                  Id = d.Id,
                                  EmployeeName = e.FirstName + " "+ e.LastName,
                                  Amount = d.Amount,
                                  DeductionMode = d.DeductionMode,
                                  DeductionMonth = d?.DeductionMonth?.ToString("dd-MMM-yyyy"),
                                  
                              }).ToList();
                //var result = _hrms.Deductions.Where(x => x.Active == true).ToList();
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditDeduction(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.Deductions.AsEnumerable().Where(x => x.Id == id).Select(x => new
            {

                Id = x.Id,
                Amount = x.Amount,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                Active = x.Active,
                DeductionMode = x.DeductionMode,
                DeductionMonth = x.DeductionMonth?.ToString("yyyy-MM-dd"),
                EmployeeId = x.EmployeeId,
                IsDeleted = x.IsDeleted,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedOn = x.LastModifiedOn,
                Reason = x.Reason

            }).FirstOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddDeduction(Deduction obj)
        {
            try
            {
                _hrms.Deductions.Add(obj);
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
        public ActionResult UpdateDeduction(Deduction obj)
        {

            try
            {
                //obj.Id = 2;
                var recrod = _hrms.Deductions.Where(x => x.EmployeeId == obj.EmployeeId).FirstOrDefault();
                recrod.Amount = obj.Amount;
                recrod.Reason = obj.Reason;
                recrod.DeductionMode = obj.DeductionMode;
                recrod.EmployeeId = obj.EmployeeId;
                recrod.DeductionMonth = obj.DeductionMonth;


                _hrms.Entry(recrod).State = EntityState.Modified;
                _hrms.SaveChanges();

                return Json(new { success = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeleteDeduction(int id)
        {
            try
            {
                Deduction rg = _hrms.Deductions.Where(x => x.Id == id).FirstOrDefault<Deduction>();
                _hrms.Deductions.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



       
        #endregion Deduction

        #region LoanType


        public ActionResult LoanType()
        {
            return View();
        }
        public ActionResult AutoCodeGenrateLoanType()
        {
            var stringCode = string.Empty;
            _hrms.Configuration.ProxyCreationEnabled = false;
            //var emp = _hrms.HrmEmployees.Where(x => x.Active == true).ToList().LastOrDefault();
            var LastCode = _hrms.LoanTypes.Where(x => x.Active == true).ToList().LastOrDefault();
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
                stringCode = "Rg-" + 1.ToString("D2");
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

        public ActionResult GetLoanTypeList()
        {
            try
            {


                var dd = _hrms.LoanTypes.Where(x => x.Active == true).FirstOrDefault();
                List<LoanType> LoanTypeList = _hrms.LoanTypes.ToList<LoanType>();
                var result = LoanTypeList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name,
                    Code = S.Code,
                    Active = S.Active
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult EditLoanType(int id)
        {
            _hrms.Configuration.ProxyCreationEnabled = false;

            var result = _hrms.LoanTypes.Where(x => x.Id == id).FirstOrDefault<LoanType>();
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddLoanType(LoanType obj)
        {
            try
            {


                bool IsrecExisit = _hrms.LoanTypes.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {

                    _hrms.LoanTypes.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "Region Name is Already Exists.", JsonRequestBehavior.AllowGet });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult UpdateLoanType(LoanType obj)
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
        public ActionResult DeleteLoanType(int id)
        {
            try
            {
                LoanType rg = _hrms.LoanTypes.Where(x => x.Id == id).FirstOrDefault<LoanType>();
                _hrms.LoanTypes.Remove(rg);
                _hrms.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion LoanType

    }
}