namespace Application.UseCases.FixedIncome.Delete;

public interface IDeleteFixedIncomeUseCase
{
    Task Execute(long id);
}