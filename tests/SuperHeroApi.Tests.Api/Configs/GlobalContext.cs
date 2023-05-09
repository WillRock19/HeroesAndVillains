using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    public class GlobalContext : IDisposable
    {
        public HttpClient Client { get; set; }
        private TestServer _server;

        public GlobalContext()
        {
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");
                });

            _server = factory.Server;
            Client = factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
