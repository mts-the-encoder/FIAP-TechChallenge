using Application.Services.Mapper;
using AutoMapper;

namespace Utils.Mapper;

public class MapperBuilder
{
    public static IMapper Instance()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguration>();
        });

        return config.CreateMapper();
    }
}