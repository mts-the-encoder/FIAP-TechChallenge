using Application.Services.UserSigned;
using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Application.UseCases.FixedIncome.GetById;

public class GetByIdFixedIncomeUseCase : IGetByIdFixedIncomeUseCase
{
    private readonly IFixedIncomeReadOnlyRepository _repository;
    private readonly IUserSigned _userSigned;
    private readonly IMapper _mapper;
    public GetByIdFixedIncomeUseCase(IMapper mapper, IUserSigned userSigned, IFixedIncomeReadOnlyRepository repository)
    {
        _mapper = mapper;
        _userSigned = userSigned;
        _repository = repository;
    }

    public async Task<FixedIncomeResponse> Execute(long id)
    {
        var userSigned = await _userSigned.GetUser();

        var investment = await _repository.GetById(id);

        Validate(userSigned, investment);

        return _mapper.Map<FixedIncomeResponse>(investment);
    }

    private static void Validate(Domain.Entities.User user, Domain.Entities.FixedIncome investment)
    {
        if (investment.UserId != user.Id || investment is null)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.INVESTIMENTO_NAO_ENCONTRADO });
    }
}