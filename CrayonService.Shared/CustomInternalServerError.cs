using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared
{
    public class CustomInternalServerError : Exception
    {
        public CustomInternalServerError(string errorMessages, Exception internalException = null) :base(errorMessages, internalException)
        {
        }
    }
}
