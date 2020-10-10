using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace FormHandler.Tests
{
    public class FormHandlerShould : IDisposable
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public FormHandlerShould()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnNotFoundIfFormDoesNotExist()
        {
            var response = await SendRequestAsync("/form/nonExistingForm/submit");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task RedirectToFormTarget()
        {
            var response = await SendRequestAsync("/form/06332e35-9e64-4bbc-82b6-2a7f94be6b06/submit");

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            Assert.Equal("http://localhost:1313/gracias", response.Headers.Location.ToString());
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string requestUri)
        {
            return await client.PostAsync(requestUri,
                new FormUrlEncodedContent(new KeyValuePair<string, string>[] { }));
        }

        public void Dispose()
        {
            server.Dispose();
        }
    }
}