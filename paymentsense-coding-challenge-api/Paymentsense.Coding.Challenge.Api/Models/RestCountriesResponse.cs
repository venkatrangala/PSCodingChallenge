using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class Language
    {
        [JsonProperty("iso639_1")]
        public string Iso6391 { get; set; }
        [JsonProperty("iso639_2")]
        public string Iso6392 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }

    public class Translations
    {
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }
        public string it { get; set; }
        public string br { get; set; }
        public string pt { get; set; }
        public string nl { get; set; }
        public string hr { get; set; }
        public string fa { get; set; }
    }

    public class RegionalBloc
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public List<string> OtherAcronyms { get; set; }
        public List<string> OtherNames { get; set; }
    }

    public class RestCountryResponse
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Borders { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public string Flag { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public List<string> CallingCodes { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public int Population { get; set; }
        public List<double> Latlng { get; set; }
        public string Demonym { get; set; }
        public double? Area { get; set; }
        public double? Gini { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public Translations Translations { get; set; }
        public List<RegionalBloc> RegionalBlocs { get; set; }
        public string Cioc { get; set; }
    }
}
