using Application.Services.UserSigned;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Serilog;

namespace Application.UseCases.VariableIncome.Create;

public class CreateVariableIncomeUseCase : ICreateVariableIncomeUseCase
{
    private readonly IMapper _mapper;
    private readonly IVariableIncomeWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSigned _userSigned;

    public CreateVariableIncomeUseCase(IMapper mapper, IVariableIncomeWriteOnlyRepository writeOnlyRepository, IUserSigned userSigned, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _writeOnlyRepository = writeOnlyRepository;
        _userSigned = userSigned;
        _unitOfWork = unitOfWork;
    }

    public async Task<VariableIncomeResponse> Execute(VariableIncomeRequest request)
    {
        Validate(request);

        var userSigned = await _userSigned.GetUser();

        var investment = _mapper.Map<Domain.Entities.VariableIncome>(request);
        investment.UserId = userSigned.Id;

        await _writeOnlyRepository.Create(investment);

        await _unitOfWork.Commit();

        return _mapper.Map<VariableIncomeResponse>(investment);
    }

    private void Validate(VariableIncomeRequest request)
    {
        var validator = new VariableIncomeValidator();
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
    }
}