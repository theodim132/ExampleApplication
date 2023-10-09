using AutoMapper;
using MyApp.Domain.MyDomain;
using MyApp.Domain.MyDomain.Dto;
using MyApp.Domain.MyDomain.Model;

namespace MyApp.Domain.MyDomain.Mappers
{
    public class CountryMapperProfile : Profile
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

                config.CreateMap<MyApp.DataAccess.Abstractions.MyDomain.Entities.Country, Model.Country>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Capital))
                    .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => src.Borders.FirstOrDefault().Name));

                config.CreateMap<MyApp.Domain.MyDomain.Model.Name, Model.Name>()
                    .ForMember(dest => dest.common, opt => opt.MapFrom(src => src.common));


                config.CreateMap<Model.Country, MyApp.DataAccess.Abstractions.MyDomain.Entities.Country>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Capital))
                    .ForMember(dest => dest.Borders, opt => opt.MapFrom(src => src.Borders.FirstOrDefault().Name));

                config.CreateMap<Model.Name, MyApp.DataAccess.Abstractions.MyDomain.Entities.Name>()
                    .ForMember(dest => dest.common, opt => opt.MapFrom(src => src.common));

                //config.CreateMap<Country, MyApp.DataAccess.Abstractions.MyDomain.Entities.Country>()
                //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name { common = src.Name }))
                //   .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => new List<string> { src.Capital }
                //  ));

                //config.CreateMap<MyApp.DataAccess.Abstractions.MyDomain.Entities.Country, Country>()
                //  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new Name { common = src.Name }))
                //  .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => new List<string> { src.Capital }
                // ));


                config.CreateMap<Border, MyApp.DataAccess.Abstractions.MyDomain.Entities.Border>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                config.CreateMap<MyApp.DataAccess.Abstractions.MyDomain.Entities.Border, Border>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            });

            return mappingConfig;
        }
    }

}
