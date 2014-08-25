using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApiDemo.Tests
{
    [TestClass]
    public static class Global
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            
        }

        public static ClaimsPrincipal GetPrincipal()
        {
            var claims = new List<Claim>()
            {
                  new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "eric@codagami.com"),
                  new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Eric"),
                  new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Administrator"),
                  new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "eric@codagami.com"),
                  new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", "4E75353B-9188-44D8-9E80-03CFAD711FFA"),
            };

            var id = new ClaimsIdentity(claims, "FakeAuthType");
            var cp = new ClaimsPrincipal(id);

            return cp;
        }
    }
}
