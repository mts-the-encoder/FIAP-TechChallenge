using Application.Services.UserSigned;
using AutoMapper;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Application.UseCases.FixedIncome.GetTesouroDireto;

public class GetTesouroDiretoUseCase : IGetTesouroDiretoUseCase
{
    private readonly IFixedIncomeReadOnlyRepository _repository;
    private readonly IUserSigned _userSigned;
    private readonly IMapper _mapper;
    public GetTesouroDiretoUseCase(IFixedIncomeReadOnlyRepository repository, IUserSigned userSigned, IMapper mapper)
    {
        _repository = repository;
        _userSigned = userSigned;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FixedIncomeResponse>> Execute()
    {
        var userSigned = await _userSigned.GetUser();

        var investments = await _repository.GetTesouroDireto(userSigned.Id);

        Validate(userSigned, investments);

        return _mapper.Map<IEnumerable<FixedIncomeResponse>>(investments);
    }

    private static void Validate(Domain.Entities.User user, IList<Domain.Entities.FixedIncome> investment)
    {
        if (investment.Any(x => x.UserId != user.Id) || investment is null)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.INVESTIMENTO_NAO_ENCONTRADO });
    }
}