#nullable enable
using Domain.Entities;
using Domain.Repositories.Investments;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryAccess.Repository;

public class VariableIncomeRepository : IVariableIncomeWriteOnlyRepository, IVariableIncomeReadOnlyRepository, IVariableIncomeUpdateOnlyRepository
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

    public async Task Delete(long id)
    {
        var investment = await _context.VariableIncomes.FirstOrDefaultAsync(x => x.Id == id);
        _context.VariableIncomes.Remove(investment);
    }

    public async Task<IList<VariableIncome>> GetAllFromUser(long id)
    {
        return await _context.VariableIncomes.Where(x => x.UserId == id).AsNoTracking().ToListAsync();
    }

    public async Task<VariableIncome> GetById(long id)
    {
        return await _context.VariableIncomes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<VariableIncome> GetInvestmentById(long id)
    {
        return await _context.VariableIncomes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(VariableIncome investment)
    {
        _context.VariableIncomes.Update(investment);
    }
}