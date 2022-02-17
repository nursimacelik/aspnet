using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Final.Project.IntegrationTests
{
    // Microsoft article: Integration tests in ASP.NET Core
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
    public class BasicTests : IClassFixture<WebApplicationFactory<Final.Project.Web.Startup>>
    {
        private readonly WebApplicationFactory<Final.Project.Web.Startup> factory;

        public BasicTests(WebApplicationFactory<Final.Project.Web.Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("/Brand")]
        [InlineData("/Category")]
        [InlineData("/Color")]
        [InlineData("/IncomingOffer")]
        [InlineData("/Offer")]
        [InlineData("/Product")]
        [InlineData("/UsingStatus")]
        public async Task Get_EndpointsReturnUnauthorized(string url)
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
