using Paymentsense.Coding.Challenge.Api.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace Paymentsense.Coding.Challenge.Api.Tests.Infrastructure
{
    public static class RestCountriesResponses
    {
        public static StringContent OkResponse => BuildOkResponse();
        public static StringContent NotFoundResponse => BuildNotFoundResponse();
        public static StringContent InternalErrorResponse => BuildInternalErrorResponse();

        private static StringContent BuildOkResponse()
        {
            var response = new List<RestCountryResponse>
                {
                    new RestCountryResponse
                    {
                        Name = "Afghanistan",
                        Languages = new List<Language>(){new Language(){ Name = "Pashto", Iso6391 = "ps", Iso6392 = "pus", NativeName = "پښتو"}},
                        Flag = "https://restcountries.eu/data/afg.svg",
                        Borders = new List<string>{"IRN", "PAK", "TKM", "UZB", "TJK", "CHN"},
                        Currencies = new List<Currency>(){ new Currency{ Code=  "AFN", Name ="Afghan afghani", Symbol = "؋"}},
                        Capital = "Kabul",
                        Timezones = new List<string>(){"UTC+04:30"},
                        Population = 27657145
                    }
                };
            var json = JsonSerializer.Serialize(response);
            return new StringContent(json);
        }

        private static StringContent BuildNotFoundResponse()
        {
            var json = JsonSerializer.Serialize(new { Cod = 404, Message = "city not found" });
            return new StringContent(json);
        }

        private static StringContent BuildInternalErrorResponse()
        {
            var json = JsonSerializer.Serialize(new { Cod = 500, Message = "Internal Error." });
            return new StringContent(json);
        }
    }
}
