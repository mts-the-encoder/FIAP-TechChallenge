using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.Update;

public interface IUpdateVariableIncomeUseCase
{
    Task<VariableIncomeResponse> Execute(long id, VariableIncomeRequest request);
}