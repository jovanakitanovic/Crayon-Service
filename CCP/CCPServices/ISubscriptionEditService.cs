using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.CCPServices
{
    public interface ISubscriptionEditService
    {
        public Task<bool> CancelSubscription(Guid serviceId);
        public Task<bool> ExtendsSubscription(Guid serviceId, DateTime validityDate);
        public Task<bool> QuantityUpdateOnSubscription(Guid serviceId, int quantity);

    }
}
