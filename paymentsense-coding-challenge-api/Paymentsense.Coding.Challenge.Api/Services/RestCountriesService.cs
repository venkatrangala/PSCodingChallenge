using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Infrastructure;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly IConfiguration _configuration;
        private readonly string _restCountriesBaseUrl;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IMapper _mapper;


        public RestCountriesService(IConfiguration configuration, IHttpClientFactory httpFactory, IMapper mapper)
        {
            _configuration = configuration;
            _httpFactory = httpFactory;
            _mapper = mapper;
            _restCountriesBaseUrl = _configuration.GetSection("RestCountriesBaseUrl").Value;

        }

        public async Task<List<Country>> GetCountries()
        {
            var url = BuildRestCountriesUrl();
            var client = _httpFactory.CreateClient("RestCountriesClient");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var restCountriesResponse = JsonConvert.DeserializeObject<List<RestCountryResponse>>(jsonString);
                var countries = _mapper.Map<List<Country>>(restCountriesResponse);

                return countries;
            }

            throw new RestCountriesException(response.StatusCode, "Error response from RestCountriesApi: " + response.ReasonPhrase);
        }

        private string BuildRestCountriesUrl()
        {
            return _restCountriesBaseUrl + "rest/v2/all";
        }
        
    }
}
