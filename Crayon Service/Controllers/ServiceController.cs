using CCP.CCPServices;
using Crayon_Service.Helpers;
using CrayonService.Command;
using CrayonService.Queries;
using CrayonService.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Crayon_Service.Controllers
{
    /// <summary>
    /// alal
    /// </summary>
    [ApiController]
    [Route("[controller]/Services")]
    public class ServiceController : BaseController
    {
        [HttpGet("ListOfServices")]
        [ProducesResponseType(200, Type = typeof(List<Services>))]
        public async Task<IActionResult> GetAllAvailableServices(CancellationToken cancellationToken)
        {
            return await HandleRequest(new GetAllServices.Query(), cancellationToken);
        }

        [HttpPost("ListOfServices")]
        [ProducesResponseType(201, Type = typeof(OrderedServiceModel))]
        public async Task<IActionResult> OrderServiceFromCCP([Required] Guid serviceId, [Required] Guid accountId, CancellationToken cancellationToken)
        {
            var command = new OrderService.Command()
            {
                AccountId = accountId,
                ServiceId = serviceId
            };

            return await HandleRequest(command, cancellationToken);
        }

        [HttpGet("{accountId:Guid}/ListOfServices")]
        [ProducesResponseType(200, Type = typeof(List<OrderedServiceModel>))]

        public async Task<IActionResult> GetServicesForAccount([Required] Guid accountId, CancellationToken cancellationToken)
        {
            var query = new GetAllServicesForAccount.Query()
            {
                AccountId = accountId
            };

            return await HandleRequest(query, cancellationToken);
        }

        [HttpPut("{subscriptionId:Guid}/QuantityUpdate")]
        [ProducesResponseType(200, Type = typeof(OrderedServiceModel))]
        public async Task<IActionResult> UpdateQuantityForAubscription([Required] Guid subscriptionId,[Required] SubscriptionQuantityUpdate details, CancellationToken cancellationToken)
        {
            var query = new UpdateSubscriptionQuantity.Command()
            {
                SubscriptionId = subscriptionId,
                Details = details                
            };

            return await HandleRequest(query, cancellationToken);
        }

        [HttpPut("Cancel")]
        [ProducesResponseType(200, Type = typeof(OrderedServiceModel))]
        public async Task<IActionResult> UpdateQuantityForAubscription([Required] Guid subscriptionId,[Required] Guid accountId, CancellationToken cancellationToken)
        {
            var query = new CancelSubscription.Command()
            {
                SubscriptionId = subscriptionId,
                AccountId = accountId
            };

            return await HandleRequest(query, cancellationToken);
        }

        [HttpPut("{subscriptionId:Guid}/ExtendValidity")]
        [ProducesResponseType(200, Type = typeof(OrderedServiceModel))]
        public async Task<IActionResult> ExctendServiceValidity([Required] Guid subscriptionId, [Required] SubscrtiprionExtendValidity details, CancellationToken cancellationToken)
        {
            var query = new ExtendServiceValidity.Command()
            {
                SubscriptionId = subscriptionId,
                Details = details
            };

            return await HandleRequest(query, cancellationToken);
        }
    }

}
