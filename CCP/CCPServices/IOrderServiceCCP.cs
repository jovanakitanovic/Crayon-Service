using CCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.CCPServices
{
    public interface IOrderServiceCCP
    {
        public Task<OrderedService> OrderServiceFromCCP(Guid serviceId, Guid accoutId);

    }
}
