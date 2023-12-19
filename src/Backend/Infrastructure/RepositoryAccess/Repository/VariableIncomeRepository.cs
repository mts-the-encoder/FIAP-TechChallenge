using Domain.Entities;
using Domain.Repositories.Investments;

namespace Infrastructure.RepositoryAccess.Repository;

public class VariableIncomeRepository : IVariableIncomeWriteOnlyRepository
{
    private readonly AppDbContext _context;
    public VariableIncomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(VariableIncome income)
    {
        await _context.VariableIncomes.AddAsync(income);
    }
}