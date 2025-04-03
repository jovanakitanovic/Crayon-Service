using CCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.CCPServices
{
    public class OrderServiceCCP : IOrderServiceCCP
    {
        public async Task<OrderedService> OrderServiceFromCCP(Guid serviceId, Guid accoutId)
        {
            throw new NotImplementedException();
        }
    }
}
