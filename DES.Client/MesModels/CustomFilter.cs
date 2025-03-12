 
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
 
using System.Threading.Tasks;
 
namespace MesModels;
public class CustomFilter : IAsyncActionFilter
{
     
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsExcludedAction(context))
        {
            var httpContext = context.HttpContext;

            var userdata = httpContext.Request.Cookies["userdata"];
            var userpages = httpContext.Request.Cookies["userpages"];
            var rolepages = httpContext.Request.Cookies["rolepages"];
            if (!string.IsNullOrEmpty(userpages))
            {
                
                   

                    if (context.Controller is Controller controller)
                    {

                    controller.ViewData["userpages"] = userpages;
                    controller.ViewData["userdata"] = userdata;
                    controller.ViewData["rolepages"] = rolepages;

                }


                 
            }
        }
        await next();
    }

    private bool IsExcludedAction(ActionExecutingContext context)
    {
        var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
        var excludeFromUserDataFilterAttribute = actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
            .Any(a => a.GetType() == typeof(ExcludeAction));

        return excludeFromUserDataFilterAttribute;
    }
}
