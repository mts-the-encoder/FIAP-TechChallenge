using Communication.Requests;
using Communication.Responses;

namespace Application.UseCases.FixedIncome.Update;

public interface IUpdateFixedIncomeUseCase
{
    Task<FixedIncomeResponse> Execute(long id, FixedIncomeRequest request);
}