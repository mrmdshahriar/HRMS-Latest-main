using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TalenAPI.Models;
using TalenBAL;
using TalenBAL.BAL;
using TalenBAL.BALModel;
using TalenBAL.User;


using Newtonsoft.Json.Linq;



using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http.Cors;
using Safehaul.Models;
using Microsoft.AspNetCore.Http;
using HttpRequest = System.Web.HttpRequest;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TalenAPI.Controllers
{
    public class RecruitmenteController : ApiController
    {
        Models.ResponseModel ObjResponseModel = new Models.ResponseModel();
        Messages ObjMessages = new Messages();
        Configuration ObjConfiguration = new Configuration();
        BLRecruitment ObjBLRecruitment = new BLRecruitment();
        GeneralViewModel ObjGeneralView = new GeneralViewModel();
        BLEmployeeProfile ObjEmployee = new BLEmployeeProfile();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        [EnableCors("*", "*", "*")]
        [HttpPost]
        [Route("api/Recruitment/Jobs")]
        public HttpResponseMessage Jobs(string Auth)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLRecruitment.GetAllJobsMasterApproved();
                 


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


        [Route("api/Recruitment/JobsById")]
        public HttpResponseMessage JobsById(string Auth, long jobId)
        {
            try
            {
                if (Auth == ObjConfiguration.GetAuthCode())
                {
                    var record = ObjBLRecruitment.GetSelectedJobById(jobId);

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
        [Route("api/Recruitment/AppliedCandidate")]
        public async Task<HttpResponseMessage> AppliedCandidateAsync()
        {

            string guid = Guid.NewGuid().ToString();
            var DocFle = new List<string>();
            var FilePath = "";
            string filename = "";
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                NameValueCollection formData = provider.FormData;

                string Auth = formData["Auth"];
                string CandidateName = formData["CandidateName"];
                string FatherName = formData["FatherName"];
                string CNIC = formData["CNIC"];
                string ContactNumber = formData["ContactNumber"]; 
                string Email = formData["Email"];
                long JobId = Convert.ToInt64(formData["JobId"]);
                string AppliedFrom = formData["AppliedFrom"];
                DateTime AvailableDate = Convert.ToDateTime(formData["AvailableDate"]);
                string Photo = "";// formData["Photo"];
                string Attachment = "";//fi formData["Attachment"];

                var httpRequest = System.Web.HttpContext.Current.Request;
                var postedFile = httpRequest.Files;

                if (Auth == ObjConfiguration.GetAuthCode()) 
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        foreach (string file in httpRequest.Files)
                        {
                            var PostedFile = httpRequest.Files[file];
                            FilePath = Path.Combine(ObjConfiguration.docPhysicalPath() + guid + PostedFile.FileName);
                            //PostedFile.SaveAs(FilePath);
                            //DocFle.Add(FilePath);
                            //Attachment = guid + PostedFile.FileName;
                            string[] CloudPath = ObjConfiguration.CloudainryApiPath();

                            var myAccount = new Account { ApiKey = CloudPath[0], ApiSecret = CloudPath[1], Cloud = CloudPath[2] };
                            Cloudinary _cloudinary = new Cloudinary(myAccount);

                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(guid + PostedFile.FileName, PostedFile.InputStream),
                                Folder = "TalentFusion",
                                

                            };
                            var uploadResult = _cloudinary.Upload(uploadParams);
                            if(file == "Attachment")
                            {
                                Attachment = uploadResult.SecureUri.ToString();
                            }
                            if(file== "Photo")
                            {
                                Photo = uploadResult.SecureUri.ToString();
                            }
                            
                            

                            // var PostedFile = httpRequest.Files[file];
                            // FilePath = Path.Combine(ObjConfiguraion.docPhysicalPath() + guid + PostedFile.FileName);
                            // PostedFile.SaveAs(FilePath);
                            // DocFle.Add(FilePath);
                            //filename = guid + PostedFile.FileName;


                        }
                    }
                   

                    var  Jobreq = ObjBLRecruitment.AddAppliedCandidate(CandidateName, FatherName, CNIC, ContactNumber, Email, JobId, AppliedFrom, AvailableDate, Photo, Attachment);


                    ObjResponseModel.Status = true;
                    ObjResponseModel.Message = ObjMessages.SuccessMessage;
                    ObjResponseModel.Result = Jobreq;
                    return Request.CreateResponse(ObjResponseModel);



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

        [HttpGet]
        [Route("api/SafehaulApi/silentpost")]
        public async Task<HttpResponseMessage> silentpost()
        {
            NameValueCollection formData = await Request.Content.ReadAsFormDataAsync();


            string Name = formData["CandidateName"].ToString();
            ObjResponseModel.Status = true;
            ObjResponseModel.Message = ObjMessages.SuccessMessage;
            ObjResponseModel.Result = null;

            return Request.CreateResponse(HttpStatusCode.OK, ObjResponseModel);
        }
        }
}
