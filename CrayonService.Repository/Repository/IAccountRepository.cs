using CrayonService.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        public Task<Accounts> GetAccountById(Guid accountId);

    }
}
