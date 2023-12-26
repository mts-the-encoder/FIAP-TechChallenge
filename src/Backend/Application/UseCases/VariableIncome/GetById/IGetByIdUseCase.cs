using Communication.Responses;

namespace Application.UseCases.VariableIncome.GetById;

public interface IGetByIdUseCase
{
    Task<VariableIncomeResponse> Execute(long id);
}

