using Domain.Entities;
using Domain.Repositories.Investments;

namespace Infrastructure.RepositoryAccess.Repository;

public class FixedIncomeRepository : IFixedIncomeWriteOnlyRepository
{
    private readonly AppDbContext _context;
    public FixedIncomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(FixedIncome fixedIncome)
    {
        await _context.FixedIncomes.AddAsync(fixedIncome);
    }
}