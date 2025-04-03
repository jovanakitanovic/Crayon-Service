using CrayonService.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crayon_Service.Helpers
{
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; set; }

        protected T Get<T>() where T : class
        {
            return HttpContext.RequestServices.GetService(typeof(T)) as T;
        }
        protected IMediator GetMediator()
        {
            return Get<IMediator>();
        }
        protected void _init()
        {
            Mediator = GetMediator();
        }

        protected async Task<IActionResult> HandleRequest<TResponse>(IRequest<TResponse> request,
                                                             CancellationToken cancellationToken)
        {
            try
            {
                _init();
                var result = await Mediator.Send(request, cancellationToken);

                if (Request.Method == HttpMethod.Post.Method)
                {
                    return Created(string.Empty, result);
                }
                else
                {
                    return Ok(result);
                }

            }
            catch(CustomBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(CustomInternalServerError ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
