using CCP;
using CrayonService.Queries.Validators;
using CrayonService.Repository.Repository;
using CrayonService.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Queries
{
    public class GetAllServicesForAccount
    {
        public class Query : IRequest<List<OrderedServiceModel>>
        {
            public Guid AccountId { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<OrderedServiceModel>>
        {
            private readonly ISubscriptionsRepository _subscriptionRepository;

            public QueryHandler(ISubscriptionsRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            public async Task<List<OrderedServiceModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listOfService = await _subscriptionRepository.GetAllSubscriptionsForAccount(request.AccountId);

                var allServicesForAccount = new List<OrderedServiceModel>();

                foreach(var service in listOfService)
                {
                    allServicesForAccount.Add(new OrderedServiceModel()
                    {
                        Quantity = service.Quantity,
                        ServiceName = service.ServiceName,
                        ServiceState = Enum.GetName(typeof(ServiceStatus), service.State),
                        ValidThrough = service.ValidThrough,
                        SubcsriptionId = service.ServiceSubscripitonId
                    });
                }

                return allServicesForAccount;

            }

        }
    }
}
