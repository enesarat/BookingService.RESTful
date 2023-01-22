using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.Helper.Exceptions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is DataNotFoundException)
                statusCode = HttpStatusCode.NotFound;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;

            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                statusCode = (int)statusCode,
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
