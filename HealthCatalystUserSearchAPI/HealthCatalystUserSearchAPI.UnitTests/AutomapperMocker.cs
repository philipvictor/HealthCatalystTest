using AutoMapper;
using HealthCatalystUserSearchAPI.AutoMapper;

namespace HealthCatalystUserSearchAPI.UnitTests
{
    public static class AutomapperMocker
    {

        public static IMapper GetTestingAutoMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
