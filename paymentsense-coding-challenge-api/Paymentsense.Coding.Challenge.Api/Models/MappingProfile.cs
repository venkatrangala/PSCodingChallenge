using AutoMapper;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RestCountryResponse, Country>();
        }

    }
}
