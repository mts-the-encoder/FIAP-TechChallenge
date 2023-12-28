using Application.Services.UserSigned;
using Application.UseCases.VariableIncome.Create;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Serilog;

namespace Application.UseCases.Update;

public class UpdateVariableIncomeUseCase : IUpdateVariableIncomeUseCase
{
    private readonly IVariableIncomeUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserSigned _userSigned;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVariableIncomeUseCase(IVariableIncomeUpdateOnlyRepository repository, IMapper mapper, IUserSigned userSigned, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _userSigned = userSigned;
        _unitOfWork = unitOfWork;
    }

    public async Task<VariableIncomeResponse> Execute(long id, VariableIncomeRequest request)
    {
        var userSigned = await _userSigned.GetUser();

        var investment = await _repository.GetInvestmentById(id);

        Validate(userSigned, investment);

        _repository.Update(investment);

        await _unitOfWork.Commit();

        return _mapper.Map<VariableIncomeResponse>(investment);
    }

    private static void Validate(Domain.Entities.User user, Domain.Entities.VariableIncome investment)
    {
        var request = _mapper.Map<VariableIncomeRequest>(investment);
        var validator = new VariableIncomeValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage).ToList();

            var concatenatedErrors = string.Join("\n",errorMessages);

            Log.ForContext("UserName", _userSigned.GetUser().Result.Email)
                .Error($"{concatenatedErrors}");

            throw new ValidationErrorsException(errorMessages);
        }

        if (investment.UserId != user.Id || investment is null)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.INVESTIMENTO_NAO_ENCONTRADO });
    }
}