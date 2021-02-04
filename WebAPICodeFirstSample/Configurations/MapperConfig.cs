using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPICodeFirstSample.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IConfiguration config)
        {            
            //CreateMap<tbl_MucLuongToiThieuVung, MucLuongToiThieuVungModel>().ReverseMap();  
        }
    }
    public class MapperConfig
    {
        internal static void Config(IServiceCollection services, IConfiguration config)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new MapperProfile(config));
                });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
