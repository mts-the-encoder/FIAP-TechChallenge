using Domain.Entities;

namespace Domain.Repositories.Investments;

public interface IFixedIncomeReadOnlyRepository
{
    Task<IList<FixedIncome>> GetAll(long id);
    Task<IList<FixedIncome>> GetAllCDB(long id);
    Task<IList<FixedIncome>> GetTesouroDireto(long id);
    Task<FixedIncome> GetById(long id);
}