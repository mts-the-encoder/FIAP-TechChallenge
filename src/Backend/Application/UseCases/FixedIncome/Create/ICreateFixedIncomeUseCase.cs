using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.FixedIncome.Create;

public interface ICreateFixedIncomeUseCase
{
    Task<FixedIncomeResponse> Execute(FixedIncomeRequest request);
}