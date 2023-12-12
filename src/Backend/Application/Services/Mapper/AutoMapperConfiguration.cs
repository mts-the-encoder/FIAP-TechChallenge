using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Entities;

namespace Application.Services.Mapper;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<User, UserRequest>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<UserResponse, UserRequest>().ReverseMap();
    }
}