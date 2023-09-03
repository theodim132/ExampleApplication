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
                config.CreateMap<Country, CountryDto>();
                config.CreateMap<CountryDto, Country>();

                config.CreateMap<Border, BorderDto>();
                config.CreateMap<BorderDto, Border>();

                config.CreateMap<Name, NameDto>();
                config.CreateMap<NameDto, Name>();
            });

            return mappingConfig;
        }
    }

}
