using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class UserController : Controller
    {
        Models.HRMS _hrms = new Models.HRMS();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        #region start User 
        public ActionResult Users()
        {
            return View();
        }


        public ActionResult GetUserList()
        {
            try
            {
                List<User> UserList = _hrms.Users.ToList<User>();
                List<UserType> UserTypeList = _hrms.UserTypes.ToList<UserType>();
                var innerjoin = from s in UserTypeList // outer sequence
                                join st in UserList //inner sequence 
                                on s.Id equals st.UserTypeId // key selector 
                                select new
                                { // result selector 
                                    Id = st.UserId,
                                    Name = st.Name,
                                    UserTypeId = Convert.ToInt64(st.UserTypeId),
                                    Active = st.IsActive,
                                    UserTypeName = s.Name
                                };
                return Json(innerjoin, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.Users.Where(x => x.UserId == id).FirstOrDefault<User>();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult AddUser(User obj)
        {
            try
            {
                bool IsrecExisit = _hrms.Users.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.Users.Add(obj);
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
                    return Json(new { success = false, message = "User is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateUser(User obj)
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
        public ActionResult DeleteUser(int id)
        {
            try
            {
                User u = _hrms.Users.Where(x => x.UserId == id).FirstOrDefault<User>();
                _hrms.Users.Remove(u);
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

        #endregion


        #region start User Type

        public ActionResult UserType()
        {
            return View();
        }

        public ActionResult GetUserTypeList()
        {
            try
            {
                List<UserType> UserTypeList = _hrms.UserTypes.ToList<UserType>();
                var result = UserTypeList.Select(S => new
                {
                    Id = S.Id,
                    Name = S.Name

                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { throw ex; }
        }


        [HttpGet]
        public ActionResult EditUserType(int id)
        {
            try
            {
                _hrms.Configuration.ProxyCreationEnabled = false;

                var result = _hrms.UserTypes.Where(x => x.Id == id).FirstOrDefault<UserType>();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) { throw ex; }

        }

        [HttpPost]
        public ActionResult AddUserType(UserType obj)
        {
            try
            {
                bool IsrecExisit = _hrms.UserTypes.Any(x => x.Name == obj.Name);
                if (IsrecExisit != true)
                {
                    _hrms.UserTypes.Add(obj);
                    _hrms.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { success = false, message = "User Type is Already Exists.", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpPost]
        public ActionResult UpdateUserType(UserType obj)
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
        public ActionResult DeleteUserType(int id)
        {
            try
            {
                UserType UType = _hrms.UserTypes.Where(x => x.Id == id).FirstOrDefault<UserType>();
                _hrms.UserTypes.Remove(UType);
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

        #endregion end userType
    }
}