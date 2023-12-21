using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.VariableIncome;

public class VariableIncomeWriteOnlyRepositoryBuilder
{
    private static VariableIncomeWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<IVariableIncomeWriteOnlyRepository> _repository;

    private VariableIncomeWriteOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IVariableIncomeWriteOnlyRepository>();
    }

    public static VariableIncomeWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new VariableIncomeWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IVariableIncomeWriteOnlyRepository Build()
    {
        return _repository.Object;
    }
}