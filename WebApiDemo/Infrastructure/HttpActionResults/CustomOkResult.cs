using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;

namespace WebApiDemo.Infrastructure.HttpActionResults
{
    public class CustomOkResult<T> : OkNegotiatedContentResult<T>
    {
        public CustomOkResult(T content, ApiController controller)
            : base(content, controller)
        {
        }

        public CustomOkResult(T content, IContentNegotiator negotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            : base(content, negotiator, request, formatters)
        {
        }

        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var resp = await base.ExecuteAsync(cancellationToken);
            
            resp.Headers.Add("X-CustomResult", DateTime.UtcNow.Ticks.ToString());

            return resp;
        }
    }
}