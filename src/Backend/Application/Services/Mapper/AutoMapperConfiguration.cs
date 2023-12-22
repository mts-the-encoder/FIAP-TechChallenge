using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Entities;
using HashidsNet;

namespace Application.Services.Mapper;

public class AutoMapperConfiguration : Profile
{
    private readonly IHashids _hashIds;

    public AutoMapperConfiguration(IHashids hashIds)
    {
        _hashIds = hashIds;
        EntityToResponse();
        RequestToEntity();
    }

    private void EntityToResponse()
    {
        CreateMap<VariableIncome, VariableIncomeResponse>()
            .ForMember(dest => dest.Id,config => config
                .MapFrom(x => _hashIds.EncodeLong(x.Id))).ReverseMap();

        CreateMap<FixedIncome, FixedIncomeResponse>()
            .ForMember(dest => dest.Id,config => config
                .MapFrom(x => _hashIds.EncodeLong(x.Id))).ReverseMap();

        CreateMap<User, UserResponse>().ReverseMap()
            .ForMember(dest => dest.Password, cfg => cfg.Ignore());

        CreateMap<VariableIncome, VariableIncomeDashboardResponse>().ForMember(dest => dest.Id,config => config
            .MapFrom(x => _hashIds.EncodeLong(x.Id)))
            .ForMember(dest => dest.Price, cfg => cfg
                .MapFrom(x => x.MinimumInvestment)).ReverseMap();
    }

    private void RequestToEntity()
    {
        CreateMap<UserRequest, User>().ReverseMap();
        CreateMap<VariableIncomeRequest, VariableIncome>().ReverseMap();
        CreateMap<FixedIncomeRequest, FixedIncome>().ReverseMap();
        CreateMap<VariableDashboardRequest, VariableIncome>().ReverseMap();
    }
}