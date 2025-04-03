using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Shared.Models
{
    public class SubscriptionQuantityUpdate
    {
        public Guid AccountId { get; set; }
        public int Quantity { get; set; }
    }
}
