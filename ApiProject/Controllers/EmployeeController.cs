using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TalenBAL;
using TalenBAL.BALModel;
using TalenBAL.BAL;
using TalenAPI.Models;
using System.Web.Http.Cors;

namespace TalenAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EmployeeController : ApiController
    {
      
        ResponseModel ObjResponseModel = new ResponseModel();
        Messages ObjMessages = new Messages();
        Configuration ObjConfiguration = new Configuration();
        BLEmployee ObjBLEmployee = new BLEmployee();
        GeneralViewModel ObjGeneralView = new GeneralViewModel();
        BLEmployeeProfile ObjEmployee = new BLEmployeeProfile();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
       
        [HttpPost]
        [Route("api/Employee/GetEmployee")]
        public HttpResponseMessage GetEmployee(string Auth , long EmployeeId)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLEmployee.GetEmployeeById(EmployeeId);
                  
                    if (record != null)
                    {
                        ObjResponseModel.Status = true;
                        ObjResponseModel.Message = ObjMessages.SuccessMessage;
                        ObjResponseModel.Result = record;
                        return Request.CreateResponse(ObjResponseModel);
                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.NotAvailable;
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
                ObjResponseModel.Message = "api/employee/GetEmployee : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }




        [HttpPost]
        [Route("api/Employee/UpdateEmployeeinfo")]
        public HttpResponseMessage UpdateEmployeeinfo(string Auth, long EmployeeId, string ContacNo, string Email, string CurrentAddress, string CurrentZipCode, DateTime IdentityExpiryDate, DateTime PassportExpiryDate)
        {

            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLEmployee.UpdateEmployeeinfo(EmployeeId, ContacNo, Email, CurrentAddress, CurrentZipCode, IdentityExpiryDate, PassportExpiryDate);            

                    if (record != null)
                    {
                        ObjResponseModel.Status = true;
                        ObjResponseModel.Message = ObjMessages.UpdateSuccess;
                        ObjResponseModel.Result = record;
                        return Request.CreateResponse(ObjResponseModel);
                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.NotAdd;
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
                ObjResponseModel.Message = "api/Employee/UpdateEmployeeinfo : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }



    }
}
