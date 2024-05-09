using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL
{
    public class Configuration
    {
        public string GetAuthCode()
        {
            string AuthCode = "TF#HRM@2022";
            return AuthCode;
        }

        public string docPhysicalPath()
        {
            //string DocPath = "E:\\Upwork Task 2022\\Complete Project\\Project Repository\\HRMS\\TalenBAL\\Doc\\upload\\";
            string DocPath = "http://talenfusionapi.somee.com\\Files\\";// "http:\\talenfusionapi.somee.com\\Doc\\upload\\";
            return DocPath;
        }

        public string DocRelatePath()
        {            
            string DocRelate = "http://talenfusionapi.somee.com/Doc/upload/";
            return DocRelate;
        }

        public string[] CloudainryApiPath()
        {
            string[] str = new string[3];

            // string array elements
            str[0] = "716799484818185";
            str[1] = "1HyHBzW9zKIYnTrjHCvIuKYOlRk";
            str[2] = "hur";
           
            return str;
        }       
    }
}
