using CrayonService.Shared;
using CrayonService.Shared.Models;
using MySqlX.XDevAPI.Common;

namespace Crayon_Service.Helpers
{
    public class GlobalExceptionHandling
    {
        public readonly RequestDelegate _delegateCall;
        //logging can be added

        public GlobalExceptionHandling(RequestDelegate delegateCall)
        {
            _delegateCall = delegateCall;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _delegateCall(context);
            }
            catch(CustomInternalServerError ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new ErrorResponseObject()
                {
                    Message = String.Format(Constants.StatusCode500Error, ex.Message),
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch(CustomBadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = new ErrorReponseObjectDetailed()
                {
                    Message = Constants.StatueCode400Error,
                    StatusCode = StatusCodes.Status400BadRequest,
                    DetailedMessage = ex.Message.Split("\n").Where(x => !x.Equals(String.Empty)).ToList()
                };
                await context.Response.WriteAsJsonAsync(response);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new ErrorResponseObject()
                {
                    Message = String.Format(Constants.StatusCode500Error, ex.Message),
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
