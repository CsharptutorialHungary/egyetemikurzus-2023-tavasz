using Moq;
using NUnit.Framework;
using System.Net;
using System.Reflection;
using Moq.Protected;
using T4XJYT_LGI301.Core.API;

namespace T4XJYT_LGI301.Tests
{
    [TestFixture]
    public class ApiDataProviderTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private ApiDataProvider _apiDataProvider;
        
        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _apiDataProvider = new ApiDataProvider(_httpClient);
        }
        
        [TearDown]
        public void Teardown()
        {
            _httpMessageHandlerMock = null;
            _httpClient = null;
            _apiDataProvider = null;
        }

        [Test]
        public void ApiDataProvider_Constructor_CreatesHttpClientWhenParameterIsNull()
        {
            // Arrange
            HttpClient? httpClient = null;

            // Act
            var apiDataProvider = new ApiDataProvider(httpClient);

            // Assert
            var httpClientField = typeof(ApiDataProvider).GetField("_httpClient", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(httpClientField);
            var actualHttpClient = httpClientField.GetValue(apiDataProvider);
            Assert.IsNotNull(actualHttpClient);
            Assert.IsInstanceOf<HttpClient>(actualHttpClient);
        }
        
        [Test]
        public void ApiDataProvider_Constructor_UsesProvidedHttpClientWhenParameterIsNotNull()
        {
            // Arrange
            var expectedHttpClient = new HttpClient();

            // Act
            var apiDataProvider = new ApiDataProvider(expectedHttpClient);

            // Assert
            var httpClientField = typeof(ApiDataProvider).GetField("_httpClient", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(httpClientField);
            var actualHttpClient = httpClientField.GetValue(apiDataProvider);
            Assert.AreEqual(expectedHttpClient, actualHttpClient);
        }
        
        [Test]
        public async Task ApiDataProvider_GetTextFromAPI_ReturnsNonEmptyString()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("This is a test.")
                });

            // Act
            string text = await _apiDataProvider.GetTextFromAPI();

            // Assert
            Assert.IsNotEmpty(text);
            Assert.IsNotNull(text);
        }

        [Test]
        public async Task ApiDataProvider_GetTextFromAPI_ReturnsTextFromAPI()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("bacon ipsum, t-bone pork belly.")
                });

            // Act
            string text = await _apiDataProvider.GetTextFromAPI();

            // Assert
            Assert.IsNotEmpty(text);
            Assert.IsNotNull(text);
            StringAssert.Contains("bacon", text);
            StringAssert.Contains("ipsum", text);
        }

        [Test]
        public async Task ApiDataProvider_GetTextFromAPI_ReturnsEmptyStringOnUnsuccessfulStatusCode()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Something went wrong"));


            // Act
            string text = await _apiDataProvider.GetTextFromAPI();

            // Assert
            Assert.AreEqual(string.Empty, text);
        }
        
        [Test]
        public async Task ApiDataProvider_GetTextFromAPI_ReturnsEmptyStringOnCancellation()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException("Something went wrong"));


            // Act
            string text = await _apiDataProvider.GetTextFromAPI();

            // Assert
            Assert.AreEqual(string.Empty, text);
        }
        
        [Test]
        public async Task ApiDataProvider_GetTextFromAPI_ReturnsEmptyStringOnException()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            string text = await _apiDataProvider.GetTextFromAPI();

            // Assert
            Assert.AreEqual(string.Empty, text);
        }
    }
}
