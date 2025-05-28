using Moq;
using Moq.Protected;
using PipeLine.HttpService.Dtos;
using PipeLine.HttpService.Services;
using PipeLine.Shared.Dtos.UserDtos;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PipeLine.AdminDesktop.Tests
{
    public class LoginHttpServiceTests
    {
        private HttpClient CreateHttpClient(HttpResponseMessage responseMessage)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            return new HttpClient(handlerMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenLoginSuccessful()
        {
            var expectedToken = "fake-jwt-token";
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedToken)
            };
            var httpClient = CreateHttpClient(response);
            var loginService = new LoginHttpService(httpClient);

            var loginRequest = new LoginRegisterRequest
            {
                Email = "testuser@example.com",
                Password = "password123"
            };

            var result = await loginService.LoginAsync(loginRequest);

            Assert.Equal(expectedToken, result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenLoginFails()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized
            };
            var httpClient = CreateHttpClient(response);
            var loginService = new LoginHttpService(httpClient);

            var loginRequest = new LoginRegisterRequest
            {
                Email = "testuser@example.com",
                Password = "wrongpassword"
            };

            var result = await loginService.LoginAsync(loginRequest);

            Assert.Null(result);
        }
    }
}
