using Communication.Responses;

namespace Application.UseCases.FixedIncome.GetTesouroDireto;

public interface IGetTesouroDiretoUseCase
{
    Task<IEnumerable<FixedIncomeResponse>> Execute();
}