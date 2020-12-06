using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Paymentsense.Coding.Challenge.Api.Infrastructure;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Tests.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class RestCountriesServiceTests
    {
        private static IMapper _mapper;
        public RestCountriesServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                var mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public async Task Returns_Expected_Values_From_the_Api()
        {

            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://restcountries.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);

            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.OkResponse);
            var sut = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);

            var result = await sut.GetCountries();

            Assert.IsType<List<Country>>(result);
        }

        [Fact]
        public async Task Returns_RestCountriesException_When_Called_With_Bad_url()
        {
            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://badurl.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);

            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.NotFoundResponse, HttpStatusCode.NotFound);
            var sut = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);

            var result = await Assert.ThrowsAsync<RestCountriesException>(() => sut.GetCountries());
            Assert.Equal(404, (int)result.StatusCode);
        }


        [Fact]
        public async Task Returns_OpenWeatherException_On_OpenWeatherInternalError()
        {
            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://badurl.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);
            
            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.InternalErrorResponse, HttpStatusCode.InternalServerError);
            var sut = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);

            var result = await Assert.ThrowsAsync<RestCountriesException>(() => sut.GetCountries());
            Assert.Equal(500, (int)result.StatusCode);
        }
    }
}
