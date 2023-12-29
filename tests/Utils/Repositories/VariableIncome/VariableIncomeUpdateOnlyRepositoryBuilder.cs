using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.VariableIncome;

public class VariableIncomeUpdateOnlyRepositoryBuilder
{
    private static VariableIncomeUpdateOnlyRepositoryBuilder _instance;
    private readonly Mock<IVariableIncomeUpdateOnlyRepository> _repository;

    private VariableIncomeUpdateOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IVariableIncomeUpdateOnlyRepository>();
    }

    public static VariableIncomeUpdateOnlyRepositoryBuilder Instance()
    {
        _instance = new VariableIncomeUpdateOnlyRepositoryBuilder();
        return _instance;
    }

    public VariableIncomeUpdateOnlyRepositoryBuilder GetById(Domain.Entities.VariableIncome investment)
    {
        _repository.Setup(r => r.GetInvestmentById(investment.Id)).ReturnsAsync(investment);

        return this;
    }

    public IVariableIncomeUpdateOnlyRepository Build()
    {
        return _repository.Object;
    }
}