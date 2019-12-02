
using AutoMapper;
using PersonDirectory.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Configuration
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfig;

        private static void Initialize()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Gender, PersonDirectory.Service.Models.Gender>().ReverseMap();
                cfg.CreateMap<City, PersonDirectory.Service.Models.City>().ReverseMap();
                cfg.CreateMap<PhoneNumber, PersonDirectory.Service.Models.PhoneNumber>().ReverseMap();
                cfg.CreateMap<RelatedPerson, PersonDirectory.Service.Models.RelatedPerson>().ReverseMap();
              
                cfg.CreateMap<PersonDirectory.Service.Models.Person, Person>().ForMember(dest => dest.GenderId, map => map.MapFrom((s, d) => s.Gender))
                        .ForMember(dest => dest.Gender, map => map.Ignore()).ForMember(dest => dest.CityId, map => map.MapFrom((s, d) => s.City?.Id ?? null));
               
                cfg.CreateMap<Person, PersonDirectory.Service.Models.Person>().ForMember(dest => dest.Gender, map => map.MapFrom((s, d) => s.GenderId))
                        .ForMember(dest => dest.City, map => map.MapFrom((s, d) => s.CityId != null ? new PersonDirectory.Service.Models.City { Id = s.CityId.Value } : null))
                        .ForSourceMember(s => s.Gender, d => d.DoNotValidate());
            });
        }

        private static Mapper _mapper;
        public static Mapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    if (_mapperConfig == null)
                        Initialize();

                    _mapper = new Mapper(_mapperConfig);
                }
                return _mapper;
            }
        }
    }
}
