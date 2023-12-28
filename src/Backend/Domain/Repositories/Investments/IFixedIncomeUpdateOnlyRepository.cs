using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IFixedIncomeUpdateOnlyRepository
{
    Task<FixedIncome> GetInvestmentById(long id);
    void Update(FixedIncome investment);
}