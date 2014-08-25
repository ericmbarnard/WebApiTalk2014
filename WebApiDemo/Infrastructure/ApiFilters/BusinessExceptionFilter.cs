using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiDemo
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}

namespace WebApiDemo.Infrastructure.ApiFilters
{
    public class BusinessExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            var req = actionExecutedContext.Request;
            var resp = actionExecutedContext.Response;
            var exception = actionExecutedContext.Exception;

            if (exception is BusinessException)
            {
                resp = HandleBusinessException(actionExecutedContext, req, resp, exception);
            }
        }

        private HttpResponseMessage HandleBusinessException(HttpActionExecutedContext actionExecutedContext, HttpRequestMessage req, HttpResponseMessage resp, Exception exception)
        {
            // change it from a 500 to a 400
            resp = req.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);

            // overwrite the response
            actionExecutedContext.Response = resp;

            // boom, we're done
            return resp;
        }
    }
}