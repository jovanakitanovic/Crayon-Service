using Crayon_Service.Helpers;
using CrayonService.Queries;
using CrayonService.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crayon_Service.Controllers
{
    [ApiController]
    [Route("[controller]/Accounts")]
    public class AccountController : BaseController
    {
        [HttpGet("{customerId:Guid}/ListAccounts")]
        [ProducesResponseType(200, Type = typeof(List<CustomerAccounts>))]

        public async Task<IActionResult> GetAccountsForUser([Required] Guid customerId, CancellationToken cancellationToken)
        {
            var query = new GetAllAccounts.Query
            {
                CustomerId = customerId
            };

            return await HandleRequest(query, cancellationToken);
        }
    }
}
