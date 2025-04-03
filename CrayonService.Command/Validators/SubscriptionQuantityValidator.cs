using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command.Validators
{
    public class SubscriptionQuantityValidator : AbstractValidator<UpdateSubscriptionQuantity.Command>
    {

        public SubscriptionQuantityValidator()
        {
            RuleFor(x => x.SubscriptionId).NotEmpty().NotNull();
            RuleFor(x=>x.Details).NotNull();
            RuleFor(x=>x.Details.AccountId).NotEmpty().NotNull();
            RuleFor(x=>x.Details.Quantity).NotEqual(0).GreaterThan(0);
        }
    
    }
}
