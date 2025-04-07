using CCP;
using CrayonService.Repository.Repository;
using CrayonService.Shared.Models;
using CrayonService.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command
{
    public class CancelSubscription
    {
        public class Command : IRequest<OrderedServiceModel>
        {
            public Guid SubscriptionId { get; set; }
            public Guid AccountId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, OrderedServiceModel>
        {
            private readonly ISubscriptionsRepository _subscriptionsRepository;
            private readonly ICCPApi _ccpApi;
            public CommandHandler(ISubscriptionsRepository subscriptionsRepository, ICCPApi ccpApi)
            {
                _subscriptionsRepository = subscriptionsRepository;
                _ccpApi = ccpApi;
            }

            public async Task<OrderedServiceModel> Handle(Command request, CancellationToken cancellationToken)
            {

                var subscriptionStatus = await _subscriptionsRepository.VerifySubscriptionst(request.SubscriptionId, request.AccountId);

                if (!subscriptionStatus)
                    throw new CustomBadRequestException(Constants.DataInvalid);

                var subscriptionCanceled = await _ccpApi.CancelService(request.SubscriptionId);

                if (!subscriptionCanceled)
                    throw new CustomInternalServerError(Constants.CCPServiceUpdate);

                var updatedData = await _subscriptionsRepository.CancelSubscription(request.SubscriptionId);

                var orderedService = new OrderedServiceModel()
                {
                    ServiceName = updatedData.ServiceName,
                    ServiceState = Enum.GetName(typeof(ServiceStatus), updatedData.State),
                    ValidThrough = updatedData.ValidThrough,
                    Quantity = updatedData.Quantity,
                    SubcsriptionId = updatedData.ServiceSubscripitonId
                };

                return orderedService;

            }
        }
    }
}
