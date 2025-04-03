using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared.Models
{
    public class Services
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
