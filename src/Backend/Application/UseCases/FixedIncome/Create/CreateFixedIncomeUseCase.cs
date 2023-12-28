using Application.Services.UserSigned;
using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories.Investments;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Serilog;

namespace Application.UseCases.FixedIncome.Create;

public class CreateFixedIncomeUseCase : ICreateFixedIncomeUseCase
{
    private readonly IFixedIncomeWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSigned _userSigned;

    public CreateFixedIncomeUseCase(IMapper mapper, IFixedIncomeWriteOnlyRepository writeOnlyRepository, IUnitOfWork unitOfWork, IUserSigned userSigned)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _userSigned = userSigned;
        _mapper = mapper;
    }

    public async Task<FixedIncomeResponse> Execute(FixedIncomeRequest request)
    {
        Validate(request);

        var userSigned = await _userSigned.GetUser();

        var investment = _mapper.Map<Domain.Entities.FixedIncome>(request);
        investment.UserId = userSigned.Id;

        await _writeOnlyRepository.Create(investment);

        await _unitOfWork.Commit();

        return _mapper.Map<FixedIncomeResponse>(investment);
    }

    private void Validate(FixedIncomeRequest request)
    {
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
    }
}