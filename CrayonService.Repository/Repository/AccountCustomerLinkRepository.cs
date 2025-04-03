using CrayonService.Repository.Models;
using CrayonService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.AccountRepository
{
    public class AccountCustomerLinkRepository : IAccountCustomerLinkRepository
    {
        private CrayonDBContext _dataContext;

        public AccountCustomerLinkRepository(CrayonDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<AccountCustomerLink>> GetAllAccountsByCustomerId(Guid customerId)
        {
            try
            {
                var result = _dataContext.AccountCustomerLink.Where(x => x.CustomerId == customerId).ToList();

                return result;

            } catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }
        }
    }
}
