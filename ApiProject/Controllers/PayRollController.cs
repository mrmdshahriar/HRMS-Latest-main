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
using System.Web;
using System.Threading.Tasks;
using Safehaul.Models;
using System.Collections.Specialized;
using System.IO;
using System.Web.Http.Cors;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TalenAPI.Controllers
{
    [EnableCors("*", "*", "*")] 
    public class PayRollController : ApiController
    {
        ResponseModel ObjResponseModel = new ResponseModel();
        Messages ObjMessages = new Messages();
        Configuration ObjConfiguration = new Configuration();
        BLPayRoll ObjBLPayRoll = new BLPayRoll();
        GeneralViewModel ObjGeneralView = new GeneralViewModel();
        BLEmployeeProfile ObjEmployee = new BLEmployeeProfile();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        LeaveViewModel ObjLeaveViewModel = new LeaveViewModel();
        SalaryRequest ObjSalaryRequest = new SalaryRequest();
        LoanRequest ObjLoanRequest = new LoanRequest();
        ExpnesesRequest ObjExpnesesRequest = new ExpnesesRequest();
        

        [HttpPost]
        [Route("api/PayRoll/SalaryRequest")]
        public HttpResponseMessage SalaryRequest(string Auth,long EmployeeId,string SalaryMonth,string SalaryMode,decimal Amount)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetSalaryMonthByEmployee(EmployeeId, SalaryMonth);

                    if (record == null)
                    {
                        bool result = ObjBLPayRoll.AddSalaryRequest(EmployeeId, SalaryMonth, SalaryMode, Amount);
                   
                       if(result == true)
                        {
                            ObjSalaryRequest.EmployeeId = EmployeeId;
                            ObjSalaryRequest.SalaryMonth = SalaryMonth;
                            ObjSalaryRequest.SalaryMode = SalaryMode;
                            ObjSalaryRequest.Amount = Amount;
                            ObjSalaryRequest.Status = "Pending";
                            ObjSalaryRequest.CreatedBy = EmployeeId;
                            ObjSalaryRequest.CreatedOn = DateTime.Now;
                            ObjResponseModel.Status = true;
                            ObjResponseModel.Message = ObjMessages.AddSuccess;
                            ObjResponseModel.Result = ObjSalaryRequest;
                            return Request.CreateResponse(ObjResponseModel);
                        }
                        else
                        {
                            ObjResponseModel.Status = false;
                            ObjResponseModel.Message = ObjMessages.Something;
                            ObjResponseModel.Result = "";
                            return Request.CreateResponse(ObjResponseModel);
                        }
                        
                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.Alreadyexist;
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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/SalaryRequest: " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
                return Request.CreateResponse(ObjResponseModel);

            }

        }              

        [HttpPost]
        [Route("api/PayRoll/LoanRequest")]
        public HttpResponseMessage LoanRequest(string Auth, long EmployeeId, decimal Amount, int Duration, DateTime RequestDate,string Purpose )
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetLoanByEmployee(EmployeeId, RequestDate);

                    if (record == null)
                    {
                        bool result = ObjBLPayRoll.AddLoanRequest(EmployeeId, Amount, Duration, RequestDate,Purpose);

                        if (result == true)
                        {
                            ObjLoanRequest.EmployeeId = EmployeeId;
                            ObjLoanRequest.Amount = Amount;
                            ObjLoanRequest.Duration = Duration;
                            ObjLoanRequest.RequestDate = RequestDate;
                            ObjLoanRequest.Purpose = Purpose;
                            ObjLoanRequest.Status = "Pending";
                            ObjLoanRequest.CreatedBy = EmployeeId;
                            ObjLoanRequest.CreatedOn = DateTime.Now;
                            ObjResponseModel.Status = true;
                            ObjResponseModel.Message = ObjMessages.AddSuccess;
                            ObjResponseModel.Result = ObjLoanRequest;
                            return Request.CreateResponse(ObjResponseModel);
                        }
                        else
                        {
                            ObjResponseModel.Status = false;
                            ObjResponseModel.Message = ObjMessages.Something;
                            ObjResponseModel.Result = "";
                            return Request.CreateResponse(ObjResponseModel);
                        }

                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.Alreadyexist;
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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/LoanRequest: " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
                return Request.CreateResponse(ObjResponseModel);

            }

        }

        [HttpPost]
        [Route("api/PayRoll/ExpensesRequest")]
        public async Task<HttpResponseMessage> ExpensesRequestAsync()
        {
            var DocFle = new List<string>();
            var FilePath = "";
            string guid = Guid.NewGuid().ToString();
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                NameValueCollection formData = provider.FormData;
                string Auth = formData["Auth"].ToString();

                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var httpRequest = HttpContext.Current.Request;
                    var postedFile = httpRequest.Files;

                ObjExpnesesRequest.EmployeeId = Convert.ToInt64(formData["EmployeeId"]);
                    ObjExpnesesRequest.Amount = Convert.ToDecimal(formData["Amount"]);
                    ObjExpnesesRequest.RequestDate = Convert.ToDateTime(formData["RequestDate"]);
              
                ObjExpnesesRequest.Purpose = formData["Purpose"].ToString();
                

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {

                        var PostedFile = httpRequest.Files[file];                      
                        FilePath = Path.Combine(ObjConfiguration.docPhysicalPath() + guid + PostedFile.FileName);
                        //PostedFile.SaveAs(FilePath);
                        //DocFle.Add(FilePath);
                        ObjExpnesesRequest.Attachment = guid + PostedFile.FileName;
                            string[] CloudPath = ObjConfiguration.CloudainryApiPath();

                            var myAccount = new Account { ApiKey = CloudPath[0], ApiSecret = CloudPath[1], Cloud = CloudPath[2] };
                            Cloudinary _cloudinary = new Cloudinary(myAccount);

                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(guid+PostedFile.FileName , PostedFile.InputStream),
                                Folder = "TalentFusion",

                            };
                            var uploadResult = _cloudinary.Upload(uploadParams);
                          
                            ObjExpnesesRequest.Attachment = uploadResult.SecureUri.ToString() ;


                        }
                }
                    else
                    {
                        ObjExpnesesRequest.Attachment = "";
                    }

                
                    var record = ObjBLPayRoll.GetLoanByEmployee(Convert.ToInt64(ObjExpnesesRequest.EmployeeId), Convert.ToDateTime(ObjExpnesesRequest.RequestDate));
                    if (record == null)
                    {

                        bool result = ObjBLPayRoll.AddExpensesRequest(Convert.ToInt64(ObjExpnesesRequest.EmployeeId), Convert.ToDecimal(ObjExpnesesRequest.Amount), Convert.ToDateTime(ObjExpnesesRequest.RequestDate), ObjExpnesesRequest.Purpose, ObjExpnesesRequest.Attachment);

                        if (result == true)
                        {
                    
                           
                            ObjExpnesesRequest.Status = "Pending";
                            ObjExpnesesRequest.CreatedBy = ObjExpnesesRequest.EmployeeId;
                            ObjExpnesesRequest.CreatedOn = DateTime.Now;
                            ObjResponseModel.Status = true;
                            ObjResponseModel.Message = ObjMessages.AddSuccess;
                            ObjResponseModel.Result = ObjExpnesesRequest;
                            return Request.CreateResponse(ObjResponseModel);
                        }
                        else
                        {
                            ObjResponseModel.Status = false;
                            ObjResponseModel.Message = ObjMessages.Something;
                            ObjResponseModel.Result = "";
                            return Request.CreateResponse(ObjResponseModel);
                        }

                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.Alreadyexist;
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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/ExpensesRequest: " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
                return Request.CreateResponse(ObjResponseModel);
            }
            return Request.CreateResponse(ObjResponseModel);
        }


        [HttpPost]
        [Route("api/PayRoll/EmployeeTask")]
        public async Task<HttpResponseMessage> EmployeeTask()
        {
            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            NameValueCollection formData = provider.FormData;
            string Auth = formData["Auth"].ToString();
            ObjExpnesesRequest.EmployeeId = Convert.ToInt64(formData["EmployeeId"]);
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetEmployeeTask(Convert.ToInt64(ObjExpnesesRequest.EmployeeId));
                    if(record != null)
                    {
                        ObjResponseModel.Status = true;
                        ObjResponseModel.Message = ObjMessages.SuccessMessage;
                        ObjResponseModel.Result = record;
                        return Request.CreateResponse(ObjResponseModel);
                    }
                    else
                    {
                        ObjResponseModel.Status = false;
                        ObjResponseModel.Message = ObjMessages.DriverNotAvailable;
                        ObjResponseModel.Result = record;
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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/EmployeeTask: " + ObjMessages.ExceptionError + " - Error: " + msg;
                return Request.CreateResponse(ObjResponseModel);
            }

        }

        [HttpPost]
        [Route("api/PayRoll/SalaryByEmployee")]
        public async Task<HttpResponseMessage> SalaryByEmployee()
        {
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                NameValueCollection formData = provider.FormData;
                string Auth = formData["Auth"].ToString();
                ObjExpnesesRequest.EmployeeId = Convert.ToInt64(formData["EmployeeId"]);

                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetSalaryByEmployee(Convert.ToInt64(ObjExpnesesRequest.EmployeeId));

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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/SalaryByEmployee : " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
                return Request.CreateResponse(ObjResponseModel);
            }

        }

        [HttpPost]
        [Route("api/PayRoll/LoanByEmployee")]
        public async Task<HttpResponseMessage> LoanByEmployee()
        {
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                NameValueCollection formData = provider.FormData;
                string Auth = formData["Auth"].ToString();
                ObjExpnesesRequest.EmployeeId = Convert.ToInt64(formData["EmployeeId"]);

                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetLoanByEmployee(Convert.ToInt64(ObjExpnesesRequest.EmployeeId));

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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/LoanByEmployee : " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
        
                return Request.CreateResponse(ObjResponseModel);
            }

        }

        [HttpPost]
        [Route("api/PayRoll/ExpensesByEmployee")]
        public async Task<HttpResponseMessage> ExpensesByEmployee()
        {
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                NameValueCollection formData = provider.FormData;
                string Auth = formData["Auth"].ToString();
                ObjExpnesesRequest.EmployeeId = Convert.ToInt64(formData["EmployeeId"]);
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLPayRoll.GetExpensesByEmployee(Convert.ToInt64(ObjExpnesesRequest.EmployeeId));

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
                string msg = ex.Message;
                ObjResponseModel.Status = false;
                ObjResponseModel.Message = "api/PayRoll/ExpensesByEmployee : " + ObjMessages.ExceptionError + " ErrorMsg : " + msg;
                return Request.CreateResponse(ObjResponseModel);


                
            }

        }


    }
}
