using Communication.Response;
using Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException)
                HandleProjectExcpetion(context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectExcpetion(ExceptionContext context)
        {
            if (context.Exception is ProductNotFoundException productEx)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(productEx.Message));
            }
            else if (context.Exception is ErrorOnValidationException exception)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
            }
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("Erro desconhecido"));
        }
    }
}
