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

    public IVariableIncomeUpdateOnlyRepository Build()
    {
        return _repository.Object;
    }
}