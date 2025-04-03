using CCP;
using CrayonService.Queries.Validators;
using CrayonService.Repository.AccountRepository;
using CrayonService.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Queries
{
    public class GetAllServices
    {

        public class Query : IRequest<List<Services>>
        {
        }

        public class QueryHandler : IRequestHandler<Query,List<Services>>
        {
            private readonly ICCPApi _ccpApi;

            public QueryHandler(ICCPApi ccpApi)
            {
                _ccpApi = ccpApi;
            }

            public async Task<List<Services>> Handle(Query request, CancellationToken cancellationToken)
            {

                var listOfServices = await _ccpApi.GetListOfServices();

                var returnListOfServices = new List<Services>();

                foreach (var service in listOfServices)
                {
                    returnListOfServices.Add(new Services() { Name = service.Name, Quantity = service.Quantity, ServiceId = service.ServiceId});
                }

                return returnListOfServices;
            }

        }
    }
}
