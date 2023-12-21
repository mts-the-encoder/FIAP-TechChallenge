using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.VariableIncome.Create;

public interface ICreateVariableIncomeUseCase
{
    Task<VariableIncomeResponse> Execute(VariableIncomeRequest request);
}