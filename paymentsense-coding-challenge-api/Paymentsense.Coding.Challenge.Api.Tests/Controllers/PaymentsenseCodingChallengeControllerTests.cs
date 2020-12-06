using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Tests.Infrastructure;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private static IMapper _mapper;

        public PaymentsenseCodingChallengeControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }


        [Fact]
        public void Returns_200_Result_On_Valid_Invocation()
        {
            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://restcountries.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);

            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.OkResponse);
            var service = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);
            var sut = new PaymentsenseCodingChallengeController(service,new NullLogger<PaymentsenseCodingChallengeController>());

            var result = sut.Get().Result as OkObjectResult;
            
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task Returns_400_Result_With_Invalid_url()
        {
            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://wrongUrl.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);

            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.NotFoundResponse, HttpStatusCode.NotFound);
            var service = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);

            var sut = new PaymentsenseCodingChallengeController(service, new NullLogger<PaymentsenseCodingChallengeController>());

            var result = await sut.Get() as ObjectResult;

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Returns_500_When_Api_Returns_Error()
        {
            var mockConfiguration = new Mock<IConfigurationSection>();
            mockConfiguration.SetupGet(m => m.Value).Returns("https://restcountries.eu/");
            mockConfiguration.Setup(a => a.GetSection("RestCountriesBaseUrl")).Returns(mockConfiguration.Object);

            var clientFactory = TestClientBuilder.RestCountriesClientFactory(RestCountriesResponses.InternalErrorResponse, HttpStatusCode.InternalServerError);
            var service = new RestCountriesService(mockConfiguration.Object, clientFactory, _mapper);
            var sut = new PaymentsenseCodingChallengeController(service, new NullLogger<PaymentsenseCodingChallengeController>());

            var result = await sut.Get() as ObjectResult;

            Assert.Equal(500, result.StatusCode);
        }
    }
}
