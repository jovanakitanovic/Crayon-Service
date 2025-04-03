using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Queries.Validators
{
    public class AllAccountsValidator: AbstractValidator<GetAllAccounts.Query>
    {
        public AllAccountsValidator() {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
        }
    }
}
