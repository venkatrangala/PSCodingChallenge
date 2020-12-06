using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Infrastructure;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        IRestCountriesService _restCountriesService;
        private readonly ILogger<PaymentsenseCodingChallengeController> _logger;
        public PaymentsenseCodingChallengeController(IRestCountriesService restCountriesService, ILogger<PaymentsenseCodingChallengeController> logger)
        {
            _restCountriesService = restCountriesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var countries = await _restCountriesService.GetCountries();
                return Ok(countries);
            }
            catch (RestCountriesException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    return BadRequest($" not found.");
                else
                    return StatusCode(500, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
