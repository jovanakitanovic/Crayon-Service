using CrayonService.Queries.Validators;
using CrayonService.Repository.AccountRepository;
using CrayonService.Shared;
using CrayonService.Shared.Models;
using MediatR;


namespace CrayonService.Queries
{
    public class GetAllAccounts
    {

        public class Query : IRequest<CustomerAccounts>
        {
            public Guid CustomerId { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, CustomerAccounts>
        {
            private readonly IAccountCustomerLinkRepository _accountRepository;

            public QueryHandler(IAccountCustomerLinkRepository accountsRepository)
            {
                _accountRepository = accountsRepository;
            }

            public async Task<CustomerAccounts> Handle(Query request, CancellationToken cancellationToken)
            {

                var listOfAccounts = await _accountRepository.GetAllAccountsByCustomerId(request.CustomerId);

                var returnListOfAccounts = new CustomerAccounts();

                foreach (var account in listOfAccounts)
                {
                    returnListOfAccounts.AccountIds.Add(account.AccountId);
                }

                return returnListOfAccounts;
            }

        }
    }
}
