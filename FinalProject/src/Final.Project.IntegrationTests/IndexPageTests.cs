using Final.Project.Core.ProductServices;
using Final.Project.Core.Shared;
using Final.Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;

namespace Final.Project.IntegrationTests
{
    // Microsoft article: Integration tests in ASP.NET Core
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
    public class IndexPageTests :
    IClassFixture<CustomWebApplicationFactory<Final.Project.Web.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Final.Project.Web.Startup>
            _factory;
        // Please provide a valid jwt token before testing
        private readonly string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjIwIiwiZW1haWwiOiJubm4zNTc1QGdtYWlsLmNvbSIsImV4cCI6MTY0NTA4ODQ3NSwiaXNzIjoiUGF0aWthIn0.sN-Cc8KGLPz_2CK39VODFUrTV6HwYOJUwAistaC7z64";

        public IndexPageTests(
            CustomWebApplicationFactory<Final.Project.Web.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        [Fact]
        public async Task Get_Color_ReturnsOneItemList()
        {
            // Arrange
            //Act
            HttpResponseMessage response;
            using (var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, "https://localhost:44322/Color"))
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwtToken);

                response = await _client.SendAsync(requestMessage);
            }

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<Color>>(responseBody);

            // Assert
            Assert.Single(list);
            
        }

    }

}
