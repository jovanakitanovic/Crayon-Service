using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Models
{
    public class OrderedService
    {
        public string ServiceName { get; set; } 
        public int LicenceNumber { get; set; } 
        public int  ServiceState { get; set; } 
        public DateTime ValidThrough { get; set; } 
        public int Quantity { get;set; }
    }
}
