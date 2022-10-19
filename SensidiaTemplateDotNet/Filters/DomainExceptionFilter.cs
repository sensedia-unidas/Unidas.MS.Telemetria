using SensidiaTemplateDotNet.Domain;
using SensidiaTemplateDotNet.Infrastructure;
using SensidiaTemplateDotNet.Application;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace SensidiaTemplateDotNet.Filters
{

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is DomainException)
            {
                //string json = JsonConvert.SerializeObject(domainException.Message);
                //context.Result = new BadRequestObjectResult(json);
                code = HttpStatusCode.BadRequest;
            }
            else if (exception is ServiceException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (exception is InfrastructureException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (exception is Exception)
            {
                code = HttpStatusCode.NotFound;
            }
            // else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (exception is MyException)             code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (code == HttpStatusCode.InternalServerError)
                logger.LogError(exception.Message, exception);
            else if(code == HttpStatusCode.BadRequest)
                logger.LogInformation(exception.Message, exception);
            else
                logger.LogWarning(exception.Message, exception);

            return context.Response.WriteAsync(result);
        }
    }

   
    //public sealed class DomainExceptionFilter : IExceptionFilter
    //{
    //    public void OnException(ExceptionContext context)
    //    {
    //        DomainException? domainException = context.Exception as DomainException;
    //        if (domainException != null)
    //        {
    //            string json = JsonConvert.SerializeObject(domainException.Message);

    //            context.Result = new BadRequestObjectResult(json);
    //            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //        }

    //        ServiceException? applicationException = context.Exception as ServiceException;
    //        if (applicationException != null)
    //        {
    //            string json = JsonConvert.SerializeObject(applicationException.Message);

    //            context.Result = new BadRequestObjectResult(json);
    //            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //        }

    //        InfrastructureException? infrastructureException = context.Exception as InfrastructureException;
    //        if (infrastructureException != null)
    //        {
    //            string json = JsonConvert.SerializeObject(infrastructureException.Message);

    //            context.Result = new BadRequestObjectResult(json);
    //            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //        }
    //    }
    //}
}
