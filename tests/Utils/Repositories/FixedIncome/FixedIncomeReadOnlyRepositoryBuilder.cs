using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.FixedIncome;

public class FixedIncomeReadOnlyRepositoryBuilder
{
    private static FixedIncomeReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IFixedIncomeReadOnlyRepository> _repository;

    private FixedIncomeReadOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IFixedIncomeReadOnlyRepository>();
    }

    public static FixedIncomeReadOnlyRepositoryBuilder Instance()
    {
        _instance = new FixedIncomeReadOnlyRepositoryBuilder();
        return _instance;
    }

    public IFixedIncomeReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}