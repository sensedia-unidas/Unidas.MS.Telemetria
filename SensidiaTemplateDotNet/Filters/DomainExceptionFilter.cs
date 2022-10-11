using SensidiaTemplateDotNet.Domain;
using SensidiaTemplateDotNet.Infrastructure;
using SensidiaTemplateDotNet.Application;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SensidiaTemplateDotNet.Filters
{
    public sealed class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            DomainException? domainException = context.Exception as DomainException;
            if (domainException != null)
            {
                string json = JsonConvert.SerializeObject(domainException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            ServiceException? applicationException = context.Exception as ServiceException;
            if (applicationException != null)
            {
                string json = JsonConvert.SerializeObject(applicationException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            InfrastructureException? infrastructureException = context.Exception as InfrastructureException;
            if (infrastructureException != null)
            {
                string json = JsonConvert.SerializeObject(infrastructureException.Message);

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
