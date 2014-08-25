using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;

namespace WebApiDemo.Tests.Api
{
    [TestClass]
    public abstract class ApiControllerTestBase
    {
        // Utility Properties to make the tests stupid easy
        public Uri BaseUri { get; set; }
        public HttpConfiguration HttpConfig { get; set; }
        public HttpClient Client { get; set; }
        public TestServer Server { get; set; }
        public ClaimsPrincipal User { get; set; }

        [TestInitialize]
        public virtual void Initialize()
        {
            HttpConfig = new HttpConfiguration();

            WebApiConfig.Register(HttpConfig);
            HttpConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var handler = new MakeLocalHttpHandler();
            User = Global.GetPrincipal();
            handler.User = User;
            HttpConfig.MessageHandlers.Add(handler);

            Server = TestServer.Create(app => app.UseWebApi(HttpConfig));

            Client = Server.HttpClient;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Client.Dispose();
            Server.Dispose();
            HttpConfig.Dispose();
        }
    }

    public class MakeLocalHttpHandler : DelegatingHandler
    {
        public IPrincipal User { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // make all of our requests appear "local"
            request.Properties["MS_IsLocal"] = new Lazy<bool>(() => true);

            Thread.CurrentPrincipal = User;
            request.GetRequestContext().Principal = User;

            return base.SendAsync(request, cancellationToken);
        }
    }
}
