using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Tests.Infrastructure
{
    public static class TestClientBuilder
    {
        public static IHttpClientFactory RestCountriesClientFactory(StringContent content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content
                });
            var client = new HttpClient(handler.Object);
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);
            return clientFactory.Object;
        }
    }
}
