using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace CrayonService.Shared
{
    public class CustomBadRequestException  : Exception
    {
        public List<string> errorMessage { get; set; }


        public override string StackTrace
        {
            get { return string.Empty; }
        }

        public CustomBadRequestException(string errorMessage) : base (errorMessage.ToString())
        {
        }

        public static async Task CreateCustomException(bool hasErrored, List<string> errorMessages)
        {

            var  errorMessage = new StringBuilder();

            foreach(var message in errorMessages)
            {
                errorMessage.Append(message+"\n");
            }

            throw new CustomBadRequestException(errorMessage.ToString());
        }

    }
}
