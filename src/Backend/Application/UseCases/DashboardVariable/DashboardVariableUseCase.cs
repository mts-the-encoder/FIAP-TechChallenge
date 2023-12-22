using Application.Services.UserSigned;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Enums;
using Domain.Extension;
using Domain.Repositories.Investments;

namespace Application.UseCases.DashboardVariable;

public class DashboardVariableUseCase : IDashboardVariableUseCase
{
    private readonly IVariableIncomeReadOnlyRepository _repository;
    private readonly IUserSigned _userSigned;
    private readonly IMapper _mapper;

    public DashboardVariableUseCase(IVariableIncomeReadOnlyRepository repository, IMapper mapper,
        IUserSigned userSigned)
    {
        _repository = repository;
        _mapper = mapper;
        _userSigned = userSigned;
    }

    public async Task<DashboardVariableResponse> Execute(VariableDashboardRequest request)
    {
        var userSigned = await _userSigned.GetUser();

        var investments = await _repository.GetAllFromUser(userSigned.Id);

        investments = Filter(request, investments);

        return new DashboardVariableResponse
        {
            Investments = _mapper.Map<List<VariableIncomeDashboardResponse>>(investments)
        };
    }

    private static IList<Domain.Entities.VariableIncome> Filter(VariableDashboardRequest request,
        IList<Domain.Entities.VariableIncome> investments)
    {
        if (investments is null) return new List<Domain.Entities.VariableIncome>();

        var investmentsF = investments.ToList();

        if (request.Type.HasValue)
        {
            investmentsF = investmentsF
                .Where(x => x.InvestmentVariableType == (InvestmentVariableType)request.Type.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(request.NameOrSender))
        {
            investmentsF = investmentsF.Where(r => r.Name.CompareUpperCase(request.NameOrSender) || r.Sender.CompareUpperCase(request.NameOrSender)).ToList();
        }

        return investmentsF.OrderBy(c => c.Name).ToList();
    }

}