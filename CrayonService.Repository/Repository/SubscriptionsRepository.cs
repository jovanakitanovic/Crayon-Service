using CCP.Models;
using CrayonService.Repository.Models;
using CrayonService.Shared;
using CrayonService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.Repository
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private CrayonDBContext _dataContext;

        public SubscriptionsRepository(CrayonDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<ServiceOrder>> GetAllSubscriptionsForAccount(Guid accountId)
        {
            try
            {
                var services = _dataContext.ServiceOrder.Where(x=>x.AccountId == accountId).ToList();

                return services;
            }

            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }

        public async Task<Guid> InsertSubscription(Guid accountId, Guid serviceId, OrderedService service)
        {
            try
            {
                var subscriptionId = Guid.NewGuid();

                var subscription = new ServiceOrder()
                {
                    AccountId = accountId,
                    Quantity = service.Quantity,
                    ServiceId = serviceId,
                    ServiceName = service.ServiceName,
                    State = service.ServiceState,
                    ValidThrough = service.ValidThrough,
                    ServiceSubscripitonId = subscriptionId
                };


                _dataContext.ServiceOrder.Add(subscription);
                _dataContext.SaveChanges();

                return subscriptionId;

            }

            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }

        public async Task<bool> VerifySubscriptionst(Guid subscriptionId, Guid accountID)
        {
            try
            {
                var services = _dataContext.ServiceOrder.Any(x => x.ServiceSubscripitonId == subscriptionId && x.AccountId== accountID);

                return services;
            }

            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }

        public async Task<ServiceOrder> UpdateSubscriptionQuantity(Guid subscriptionId, int quantity)
        {
            try
            {
                var data = _dataContext.ServiceOrder.First(x => x.ServiceSubscripitonId == subscriptionId);

                data.Quantity = quantity;
                _dataContext.SaveChanges();

                return data;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }

        public async Task<ServiceOrder> CancelSubscription(Guid subscriptionId)
        {
            try
            {
                var data = _dataContext.ServiceOrder.First(x => x.ServiceSubscripitonId == subscriptionId);

                data.State = (int)ServiceStatus.Canceled;
                _dataContext.SaveChanges();

                return data;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }

        public async Task<ServiceOrder> ExtendSubscription(Guid subscriptionId, DateTime valitityDate)
        {
            try
            {
                var data = _dataContext.ServiceOrder.First(x => x.ServiceSubscripitonId == subscriptionId);

                data.ValidThrough  = valitityDate;
                _dataContext.SaveChanges();

                return data;
            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }
    }
}
