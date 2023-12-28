using Communication.Responses;

namespace Application.UseCases.FixedIncome.GetAll;

public interface IGetAllFixedIncomeUseCase
{
    Task<IEnumerable<FixedIncomeResponse>> Execute();
}