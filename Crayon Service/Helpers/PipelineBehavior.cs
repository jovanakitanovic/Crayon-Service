using CrayonService.Shared;
using FluentValidation;
using MediatR;

namespace Crayon_Service.Helpers
{
    public class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public PipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = _validators.Select(v => v.ValidateAsync(context, cancellationToken));
                var failures = validationResults.SelectMany(r => r.Result.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    await CustomBadRequestException.CreateCustomException(true, failures.Select(x => x.ErrorMessage).ToList());
            }


            var response = await next();

            return response;


        }

    }
}
