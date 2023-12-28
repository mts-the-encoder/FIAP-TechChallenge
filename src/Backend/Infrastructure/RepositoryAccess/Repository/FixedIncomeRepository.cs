using Domain.Entities;
using Domain.Enums;
using Domain.Repositories.Investments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryAccess.Repository;

public class FixedIncomeRepository : IFixedIncomeWriteOnlyRepository, IFixedIncomeUpdateOnlyRepository, IFixedIncomeReadOnlyRepository
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

    public async Task Delete(long id)
    {
        var investment = await _context.FixedIncomes.FirstOrDefaultAsync(x => x.Id == id);
        _context.FixedIncomes.Remove(investment);
    }

    public async Task<FixedIncome> GetInvestmentById(long id)
    {
        return await _context.FixedIncomes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(FixedIncome investment)
    {
        _context.FixedIncomes.Update(investment);
    }

    public async Task<IList<FixedIncome>> GetAll(long id)
    {
        return await _context.FixedIncomes.Where(x => x.UserId == id).AsNoTracking().ToListAsync();
    }

    public async Task<IList<FixedIncome>> GetAllCDB(long id)
    {
        return await _context.FixedIncomes.AsNoTracking().Where(x => x.InvestmentFixedType == InvestmentFixedType.CDB && x.UserId == id).ToListAsync();
    }

    public async Task<IList<FixedIncome>> GetTesouroDireto(long id)
    {
        return await _context.FixedIncomes.AsNoTracking().Where(x => x.InvestmentFixedType == InvestmentFixedType.TesouroDireto && x.UserId == id).ToListAsync();
    }

    public async Task<FixedIncome> GetById(long id)
    {
        return await _context.FixedIncomes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }
}