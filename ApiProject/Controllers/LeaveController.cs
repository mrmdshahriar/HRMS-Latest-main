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
using ApiProject.Models;
using System.Web.Http.Cors;

namespace TalenAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LeaveController : ApiController
    {
       
        ResponseModel ObjResponseModel = new ResponseModel();
        Messages ObjMessages = new Messages();
        Configuration ObjConfiguration = new Configuration();
        BLLeave ObjBLLeave = new BLLeave();
        GeneralViewModel ObjGeneralView = new GeneralViewModel();
        BLEmployeeProfile ObjEmployee = new BLEmployeeProfile();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        LeaveViewModel ObjLeaveViewModel = new LeaveViewModel();
         [HttpPost]
        [Route("api/leave/LeaveTypes")]
        public HttpResponseMessage LeaveTypes(string Auth)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLLeave.GetAllLeaveType();
                  
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
                ObjResponseModel.Message = "api/leave/LeaveTypes : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }


        [HttpPost]
        [Route("api/leave/LeavesByEmployee")]
        public HttpResponseMessage LeavesByEmployee(string Auth, long EmployeeId)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLLeave.GetLeavesByEmployee(EmployeeId);

                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                           
                            //ObjLeaveViewModel.Id = item.EmployeeId;

                            //ObjLeaveViewModel.EmployeeId = record.Employee;
                            //ObjLeaveViewModel.EmployeeCode = record.EmployeeCode;
                            //ObjLeaveViewModel.EmployeeName = record.FirstName + " " + record.LastName;
                            //ObjLeaveViewModel.LeaveTypeId = record.LeaveTypeId;
                            //ObjLeaveViewModel.LeaveTypeName = record.Name;
                            //ObjLeaveViewModel.DateFrom = record.DateFrom.ToString("MMMM,dd,yyyy");
                            //ObjLeaveViewModel.DateTo = record.DateTo.ToString("MMMM,dd,yyyy");
                            //ObjLeaveViewModel.LeaveDays = record.LeaveDays;
                            //ObjLeaveViewModel.Reason = record.Reason;
                            //ObjLeaveViewModel.Status = record.Status;
                        }


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
                ObjResponseModel.Message = "api/leave/LeavesByEmployee : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }


        [HttpPost]
        [Route("api/leave/AddLeaveRequest")]
        public HttpResponseMessage AddLeaveRequest(string Auth, long EmployeeId, long LeaveTypeId, DateTime DateFrom, DateTime DateTo,
                                                   int LeaveDays, string Reason)
        {
        
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLLeave.AddLeaveRequest(Auth,EmployeeId,LeaveTypeId,DateFrom,DateTo,LeaveDays,Reason);

                    if (record != null)
                    {
                        ObjResponseModel.Status = true;
                        ObjResponseModel.Message = ObjMessages.AddSuccess;
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
                ObjResponseModel.Message = "api/leave/AddLeaveRequest : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }


        [HttpPost]
        [Route("api/leave/LeaveStatus")]
        public HttpResponseMessage LeaveStatus(string Auth)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLLeave.GetLeaveStatus();

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
                ObjResponseModel.Message = "api/leave/LeaveStatus : " + ObjMessages.ExceptionError;
                return Request.CreateResponse(ObjResponseModel);
            }

        }
    }
}
