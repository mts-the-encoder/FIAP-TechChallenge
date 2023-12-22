using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IVariableIncomeReadOnlyRepository
{
    Task<IList<VariableIncome>> GetAllFromUser(long id);
    Task<VariableIncome> GetById(long id);
}