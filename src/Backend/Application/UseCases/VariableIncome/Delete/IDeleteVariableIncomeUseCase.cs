namespace Application.UseCases.VariableIncome.Delete;

public interface IDeleteVariableIncomeUseCase
{
    Task Execute(long id);
}