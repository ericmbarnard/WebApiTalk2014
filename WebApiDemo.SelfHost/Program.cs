using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApiDemo.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // sub class of HttpConfiguration!
            var config = new HttpSelfHostConfiguration("http://localhost:32567"); // random port

            WebApiDemo.WebApiConfig.Register(config);   

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
