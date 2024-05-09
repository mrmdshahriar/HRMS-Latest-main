using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalenAPI.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}