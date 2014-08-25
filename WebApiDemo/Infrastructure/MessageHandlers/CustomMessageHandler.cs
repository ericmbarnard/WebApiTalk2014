using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiDemo.Infrastructure.MessageHandlers
{
    public class CustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var resp = await base.SendAsync(request, cancellationToken);

            sw.Stop();
            resp.Headers.Add("X-Response-Timing", sw.ElapsedTicks.ToString());
            
            return resp;
        }
    }
}