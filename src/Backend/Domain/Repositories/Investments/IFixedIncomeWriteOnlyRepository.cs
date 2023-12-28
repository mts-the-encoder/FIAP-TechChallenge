using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IFixedIncomeWriteOnlyRepository
{
    Task Create(FixedIncome fixedIncome);
    Task Delete(long id);
}