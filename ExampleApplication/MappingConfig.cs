using AutoMapper;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace Microservices.Services.HotelRoomAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Country, CountryDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto { Common = src.Name }))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => new List<string> { src.Capital }
                   ));

                config.CreateMap<CountryDto, Country>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Common))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Capital.FirstOrDefault()));
               
            });

            return mappingConfig;
        }
    }

}
