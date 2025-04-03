﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrayonService.Command.Validators
{
    public class ExtendValidityValidator : AbstractValidator<ExtendServiceValidity.Command>
    {

        public ExtendValidityValidator()
        {
            RuleFor(x => x.SubscriptionId).NotEmpty().NotNull();
            RuleFor(x => x.Details).NotEmpty().NotNull();
            RuleFor(x => x.Details.AccountId).NotEmpty().NotNull();
            RuleFor(x => x.Details.ValiditiyDate).NotEmpty().NotNull().GreaterThan(DateTime.Now);
        }
    }
}
