using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL
{
    public class Messages
    {
        public string SignInSuccess = string.Format("Sign In Successfully");
        public string IncorrectIdPaswd = string.Format("Incorrect LoginId or Password");
        public string Uploaded = string.Format("Image Uploaded Successfully.");
        public string NotUploaded = string.Format("Image Uploaded UnSuccessfully.");
        public string RequiredImage = string.Format("Please Upload Image.");
        public string AddSuccess = string.Format("Record Added Successfully");
        public string UpdateSuccess = string.Format("Record Update Successfully");
        public string NotAdd = string.Format("Record Added Fail");
        public string NotUpdated = string.Format("Record Update Fail");
        public string NotAvailable = string.Format("Record is Not Available");
        public string NotValidUser = string.Format("Not valid User");
        public string DriverNotAvailable = string.Format("data is Not Available");
        public string SuccessMessage = string.Format("Success");
        public string FailMessage = string.Format("Fail");
        public string ExceptionError = string.Format("Exception Error");
        public string EmailAlreadyExist = string.Format("Email Address Is Already Exist ");
        public string AuthCodeError = string.Format("Auth code is not valid ");
        public string Alreadyexist = string.Format("Request is already exist ");
        public string Something = string.Format("Some thing went wrong ");
    }
}
