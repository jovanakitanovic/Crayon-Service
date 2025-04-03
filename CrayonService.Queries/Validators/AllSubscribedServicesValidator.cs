using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Queries.Validators
{
    public class AllSubscribedServicesValidator : AbstractValidator<GetAllServicesForAccount.Query>
    {
        public AllSubscribedServicesValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty().NotNull();
        }
    }
}
