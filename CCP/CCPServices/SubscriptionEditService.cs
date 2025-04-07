using CCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.CCPServices
{
    public class SubscriptionEditService : ISubscriptionEditService
    {
        public async Task<bool> CancelSubscription(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExtendsSubscription(Guid serviceId, DateTime validityDate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> QuantityUpdateOnSubscription(Guid serviceId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
