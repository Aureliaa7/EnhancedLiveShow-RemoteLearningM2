using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace LiveShow.Api.Filters
{
    public class ModelValidationFilter : System.Web.Http.Filters.ActionFilterAttribute, IFilterMetadata
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
