

using Microsoft.AspNetCore.Mvc.Filters;

namespace SensidiaTemplateDotNet.Filters
{
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);


            }
        }
    }
}
