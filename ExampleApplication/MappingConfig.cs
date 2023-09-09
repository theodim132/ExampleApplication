using AutoMapper;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Country, CountryDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto { Common = src.Name }))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => new List<string> { src.Capital }))
                    .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => src.Borders.Select(b => b.Name).ToList()));

                config.CreateMap<CountryDto, Country>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Common))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Capital.FirstOrDefault()))
                    .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => src.Borders.Select(b => new Border { Name = b }).ToList()));

                config.CreateMap<Border, BorderDto>();
                config.CreateMap<BorderDto, Border>();

                config.CreateMap<Name, NameDto>();
                config.CreateMap<NameDto, Name>();
            });


            return mappingConfig;
        }
    }

}
