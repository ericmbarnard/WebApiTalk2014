using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Logging;

namespace WebApiDemo.Infrastructure.ApiFilters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        const string EXECUTING_MESSAGE = @"Executing Api Action: {0} - Url: {1}";
        const string PARAMS_MESSAGE = @"Api Action Parameters: {0}";
        const string EXECUTED_MESSAGE = @"Executed Api Action: {0} - Url: {1}";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var log = GetLogger(filterContext.ControllerContext);

            log.DebugFormat(EXECUTING_MESSAGE,
                    filterContext.ActionDescriptor.ActionName,
                    filterContext.ControllerContext.Request.RequestUri.ToString());

            if (filterContext.ActionArguments.Count > 0)
            {
                log.DebugFormat(PARAMS_MESSAGE, GetParamsMessage(filterContext));
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            GetLogger(filterContext.ActionContext.ControllerContext)
                .DebugFormat(EXECUTED_MESSAGE,
                    filterContext.ActionContext.ActionDescriptor.ActionName,
                    filterContext.Request.RequestUri.ToString());

            base.OnActionExecuted(filterContext);
        }

        private static ILog GetLogger(HttpControllerContext context)
        {
            return LogManager.GetLogger(context.Controller.GetType());
        }

        private static string GetParamsMessage(HttpActionContext filterContext)
        {
            return String.Join(Environment.NewLine, filterContext.ActionArguments);
        }
    }
}