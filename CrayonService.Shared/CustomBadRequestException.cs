using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared
{
    public class CustomBadRequestExceptionObject :Exception
    {
        private List<string> errorMessages { get; set; }
        private Exception internalException { get; set; }

        public CustomBadRequestExceptionObject(List<string> errorMessages, Exception internalException = null)
        {
            this.errorMessages = errorMessages;
            this.internalException = internalException;
        }

    }
}
