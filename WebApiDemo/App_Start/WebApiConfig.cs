using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApiDemo.Infrastructure.ApiFilters;
using WebApiDemo.Infrastructure.MessageHandlers;

namespace WebApiDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Replace the default JsonFormatter with our custom one
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            #region Special Sauce

            config.Filters.Add(new LoggingFilter());
            config.Filters.Add(new BusinessExceptionFilter());

            // Yay, no more XML!
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.MessageHandlers.Add(new CustomMessageHandler());

            #endregion
        }
    }
}
