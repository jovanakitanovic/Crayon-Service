using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command.Validators
{
    public class CancelSubscriptionValidator : AbstractValidator<CancelSubscription.Command>
    {

        public CancelSubscriptionValidator()
        {
            RuleFor(x => x.SubscriptionId).NotEmpty().NotNull();
            RuleFor(x => x.AccountId).NotEmpty().NotNull();
        }
    }
}
