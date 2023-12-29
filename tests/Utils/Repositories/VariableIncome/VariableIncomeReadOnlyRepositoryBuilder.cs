using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.VariableIncome;

public class VariableIncomeReadOnlyRepositoryBuilder
{
    private static VariableIncomeReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IVariableIncomeReadOnlyRepository> _repository;

    private VariableIncomeReadOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IVariableIncomeReadOnlyRepository>();
    }

    public static VariableIncomeReadOnlyRepositoryBuilder Instance()
    {
        _instance = new VariableIncomeReadOnlyRepositoryBuilder();
        return _instance;
    }

    public IVariableIncomeReadOnlyRepository Build()
    {
        return _repository.Object;
    }

    public VariableIncomeReadOnlyRepositoryBuilder GetById(Domain.Entities.VariableIncome investment)
    {
        _repository.Setup(r => r.GetById(investment.Id)).ReturnsAsync(investment);

        return this;
    }
}