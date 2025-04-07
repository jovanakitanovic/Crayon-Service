using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared.Models
{
    public class ErrorResponseObject
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ErrorReponseObjectDetailed : ErrorResponseObject
    {
        public List<string> DetailedMessage { get; set; }

    }
}
