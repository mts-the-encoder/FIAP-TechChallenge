using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IVariableIncomeWriteOnlyRepository
{
    Task Create(VariableIncome income);
}