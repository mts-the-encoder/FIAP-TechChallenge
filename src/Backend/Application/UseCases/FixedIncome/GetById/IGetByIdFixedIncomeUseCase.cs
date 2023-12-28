using Communication.Responses;

namespace Application.UseCases.FixedIncome.GetById;

public interface IGetByIdFixedIncomeUseCase
{
    Task<FixedIncomeResponse> Execute(long id);
}