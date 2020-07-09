using LiveShow.Services.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace LiveShow.Api.Filters
{
    public class LiveShowExceptionFilter : System.Web.Http.Filters.ExceptionFilterAttribute, IFilterMetadata
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if(context.Exception is ItemNotFoundException)
            {
                var message = context.Exception.Message;
                Trace.WriteLine(message);
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
