using CCP.Models;
using CrayonService.Repository.Models;
using CrayonService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.Repository
{
    public interface ISubscriptionsRepository
    {
        public Task<Guid> InsertSubscription(Guid accountId, Guid serviceId, OrderedService service);
        public Task<List<ServiceOrder>> GetAllSubscriptionsForAccount(Guid accountId);
        public Task<bool> VerifySubscriptionst(Guid subscriptionId, Guid accountId);
        public Task<ServiceOrder> UpdateSubscriptionQuantity(Guid subscriptionId, int quantity);
        public Task<ServiceOrder> CancelSubscription(Guid subscriptionId);
        public Task<ServiceOrder> ExtendSubscription(Guid subscriptionId, DateTime valitityDate);


    }
}
