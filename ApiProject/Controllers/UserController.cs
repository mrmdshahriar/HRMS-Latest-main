using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TalenAPI.Models;
using TalenBAL;
using TalenBAL.BAL;
using TalenBAL.BALModel;
using TalenBAL.User;

namespace TalenAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
       
        Models.ResponseModel ObjResponseModel = new Models.ResponseModel();
        Messages ObjMessages = new Messages();
        Configuration ObjConfiguration = new Configuration();
        BLUserLogin ObjBLUserLogin = new BLUserLogin();
        GeneralViewModel ObjGeneralView = new GeneralViewModel();
        BLEmployeeProfile ObjEmployee = new BLEmployeeProfile();

        [HttpPost]
        [Route("api/User/UserLogin")]
        public HttpResponseMessage UserLogin(string Auth, string LoginId, string Password)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLUserLogin.LoginUser(LoginId, Password);
                    var EmployeeRecord = ObjEmployee.GetEmployeeById(record.EmployeeId);
                    var UserTypeRecord = ObjBLUserLogin.GetUserTypeById(Convert.ToInt64(record.UserTypeId));
                    ObjGeneralView.UserId = record.UserId;
                    ObjGeneralView.EmployeeId = record.EmployeeId;
                    ObjGeneralView.EmployeeName = EmployeeRecord.FirstName + " " + EmployeeRecord.LastName;
                    ObjGeneralView.UserTypeId = record.UserTypeId;
                    ObjGeneralView.UserTypeName = UserTypeRecord.Name;
                    ObjGeneralView.LoginId = record.LoginId;
                    ObjGeneralView.Password = record.Password;
                    ObjGeneralView.CNICExpiry = EmployeeRecord.IdentityExpiryDate;
                    ObjGeneralView.PassportExpiry = EmployeeRecord.PassportExpiryDate;
                    ObjGeneralView.ContactNo = EmployeeRecord.ContacNo;
                    ObjGeneralView.Email = EmployeeRecord.Email;
                    ObjGeneralView.Address = EmployeeRecord.CurrentAddress;
                    if (record != null)
                    {
                        ObjResponseModel.Status = true;
                        ObjResponseModel.Message = ObjMessages.SignInSuccess;
                        ObjResponseModel.Result = ObjGeneralView;
                        return Request.CreateResponse(ObjResponseModel);
                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.IncorrectIdPaswd;
                        return Request.CreateResponse(ObjResponseModel);
                    }
                }
                else
                {
                    ObjResponseModel.Status = false;
                    ObjResponseModel.Message = ObjMessages.AuthCodeError;
                    return Request.CreateResponse(ObjResponseModel);
                }

            }
            catch (Exception ex)
            {
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/User/UserLogin : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }
    }
}
