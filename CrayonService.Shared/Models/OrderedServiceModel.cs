using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared.Models
{
    public enum ServiceStatus
    {
        Ordered, 
        Active, 
        Inactive,
        Canceled
    }
    public class OrderedServiceModel
    {
        public string ServiceName { get; set; }
        public string ServiceState { get; set; }
        public DateTime ValidThrough { get; set; }
        public int Quantity { get; set; }

        public Guid SubcsriptionId { get; set; }
    }
}
