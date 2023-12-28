using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IVariableIncomeUpdateOnlyRepository
{
    Task<VariableIncome> GetInvestmentById(long id);
    void Update(VariableIncome investment);
}