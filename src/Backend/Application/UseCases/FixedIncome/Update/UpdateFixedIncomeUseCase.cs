using Application.Services.UserSigned;
using Application.UseCases.FixedIncome.Create;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Serilog;

namespace Application.UseCases.FixedIncome.Update;

public class UpdateFixedIncomeUseCase : IUpdateFixedIncomeUseCase
{
    private readonly IFixedIncomeUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserSigned _userSigned;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFixedIncomeUseCase(IFixedIncomeUpdateOnlyRepository repository, IMapper mapper, IUserSigned userSigned, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _userSigned = userSigned;
        _unitOfWork = unitOfWork;
    }

    public async Task<FixedIncomeResponse> Execute(long id, FixedIncomeRequest request)
    {
        var userSigned = await _userSigned.GetUser();

        var investment = await _repository.GetInvestmentById(id);

        Validate(userSigned, investment);

        _repository.Update(investment);

        await _unitOfWork.Commit();

        return _mapper.Map<FixedIncomeResponse>(investment);
    }

    private void Validate(Domain.Entities.User user, Domain.Entities.FixedIncome investment)
    {
        var request = _mapper.Map<FixedIncomeRequest>(investment);
        var validator = new FixedIncomeValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();

            var concatenatedErrors = string.Join("\n", errorMessages);

            Log.ForContext("UserName", _userSigned.GetUser().Result.Email)
                .Error($"{concatenatedErrors}");

            throw new ValidationErrorsException(errorMessages);
        }

        if (investment.UserId != user.Id || investment is null)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.INVESTIMENTO_NAO_ENCONTRADO });
    }
}