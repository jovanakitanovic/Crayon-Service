using CCP;
using CrayonService.Repository.AccountRepository;
using CrayonService.Repository.Repository;
using CrayonService.Shared;
using CrayonService.Shared.Models;
using MediatR;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command
{
    public class OrderService
    {
        public class Command : IRequest<OrderedServiceModel>
        {
            public Guid AccountId {  get; set; }
            public Guid ServiceId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, OrderedServiceModel>
        {
            private readonly ICCPApi _ccpApi;
            private readonly IAccountRepository _accountRepository;
            private readonly ISubscriptionsRepository _subscriptionsRepository;
            public CommandHandler(ICCPApi ccpApi, IAccountRepository accountRepository, ISubscriptionsRepository subscriptionsRepository)
            {
                _ccpApi = ccpApi;
                _accountRepository = accountRepository;
                _subscriptionsRepository = subscriptionsRepository;
            }

            public async Task<OrderedServiceModel> Handle(Command request, CancellationToken cancellationToken)
            {

                var account =await _accountRepository.GetAccountById(request.AccountId);

                if (account == null)
                    throw new CustomBadRequestException(Constants.AccountIdInvalid);

                var orderingStatus = await _ccpApi.OrderService(request.ServiceId, request.AccountId);

                if(orderingStatus == null)
                    throw new CustomBadRequestException(Constants.ServiceInvalid);

                var savingOrder =await _subscriptionsRepository.InsertSubscription(accountId: request.AccountId, serviceId: request.ServiceId, orderingStatus);


                var orderedService = new OrderedServiceModel()
                {

                    ServiceName = orderingStatus.ServiceName,
                    ServiceState = Enum.GetName(typeof(ServiceStatus),orderingStatus.ServiceState),
                    ValidThrough = orderingStatus.ValidThrough,
                    Quantity = orderingStatus.Quantity,
                    SubcsriptionId = savingOrder
                };

                return orderedService;
            }

        }
    }
}
