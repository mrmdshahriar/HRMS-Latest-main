using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalenBAL.ViewModel
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Result { get; set; }
    }
}
