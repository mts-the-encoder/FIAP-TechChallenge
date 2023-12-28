using Communication.Responses;

namespace Application.UseCases.FixedIncome.GetCDB;

public interface IGetCDBUseCase
{
    Task<IEnumerable<FixedIncomeResponse>> Execute();
}