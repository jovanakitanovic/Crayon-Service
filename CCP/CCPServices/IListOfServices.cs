using CCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.CCPServices
{
    public interface IListOfServices
    {
        public Task<List<Service>> GetListOfServices();

    }
}
