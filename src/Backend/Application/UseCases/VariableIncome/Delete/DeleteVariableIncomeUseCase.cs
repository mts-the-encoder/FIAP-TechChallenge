using Application.Services.UserSigned;
using Domain.Repositories.Investments;
using Exceptions;
using Exceptions.ExceptionBase;
using Infrastructure.RepositoryAccess.UnitOfWork;

namespace Application.UseCases.VariableIncome.Delete;

public class DeleteVariableIncomeUseCase : IDeleteVariableIncomeUseCase
{
    private readonly IVariableIncomeWriteOnlyRepository _writeOnlyRepository;
    private readonly IVariableIncomeReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSigned _userSigned;

    public DeleteVariableIncomeUseCase(IUnitOfWork unitOfWork, IUserSigned userSigned, IVariableIncomeWriteOnlyRepository writeOnlyRepository, IVariableIncomeReadOnlyRepository readOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _userSigned = userSigned;
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Execute(long id)
    {
        var userSigned = await _userSigned.GetUser();

        var investment = await _readOnlyRepository.GetById(id);

        Validate(userSigned, investment);

        await _writeOnlyRepository.Delete(id);

        await _unitOfWork.Commit();
    }

    private static void Validate(Domain.Entities.User user,Domain.Entities.VariableIncome investment)
    {
        if (investment.UserId != user.Id || investment is null)
            throw new ValidationErrorsException(new List<string> { ErrorMessages.INVESTIMENTO_NAO_ENCONTRADO });
    }
}