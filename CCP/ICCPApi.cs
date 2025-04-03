using CCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP
{
    public interface ICCPApi
    {
        public Task<List<Service>> GetListOfServices();

        public Task<OrderedService> OrderService(Guid serviceId, Guid accoutnId);
    }
}
