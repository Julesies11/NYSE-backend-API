using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace NYSE.API.Test
{
        public class TestProvider : IDisposable
        {
            private TestServer server;
            public HttpClient Client { get; private set; }
            public TestProvider()
            {

                //var server = new TestServer(_host);
                var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

                Client = server.CreateClient();

            }

            public void Dispose()
            {
                server?.Dispose();
                Client?.Dispose();
            }
        }

}
