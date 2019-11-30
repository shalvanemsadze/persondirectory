using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Shared.Helper_Types
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfig;

        private static void Initialize()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<JobInfo, Job>();
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
