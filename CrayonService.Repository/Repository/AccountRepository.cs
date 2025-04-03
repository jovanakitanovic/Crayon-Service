using CrayonService.Repository.Models;
using CrayonService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private CrayonDBContext _dataContext;

        public AccountRepository(CrayonDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Accounts> GetAccountById(Guid accountId)
        {

            try
            {
                var result = _dataContext.Account.Where(x => x.AccountId == accountId).FirstOrDefault();

                return result;

            }
            catch (Exception ex)
            {
                throw new CustomInternalServerError(ex.Message, ex);
            }

        }
    }
}
