using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared
{
    public static class Constants
    {
        public static readonly string BadRequestMessage = "Invalid input";
        public static readonly string AccountIdInvalid = "Account id is not valid";
        public static readonly string ServiceInvalid = "Required service is not found";
        public static readonly string AlreadyAddedService = "Requested service is already tied to an account";
        public static readonly string DataInvalid = "Data in request is not valid";
    }
}
