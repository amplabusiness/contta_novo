using AutoMapper;
using Corporate.Contta.Schedule.Api.Mapper;

namespace Corporate.Contta.Schedule.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig;
        }
       
    }
}
