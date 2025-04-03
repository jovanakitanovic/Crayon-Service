using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command.Validators
{
    public class OrderServiceValidator : AbstractValidator<OrderService.Command>
    {

        public OrderServiceValidator()
        {
            RuleFor(x => x.ServiceId).NotEmpty().NotNull();
            RuleFor(x => x.AccountId).NotEmpty().NotNull();
        }

    }
}
