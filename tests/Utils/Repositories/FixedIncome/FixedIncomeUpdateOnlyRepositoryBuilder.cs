using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.FixedIncome;

public class FixedIncomeUpdateOnlyRepositoryBuilder
{
    private static FixedIncomeUpdateOnlyRepositoryBuilder _instance;
    private readonly Mock<IFixedIncomeUpdateOnlyRepository> _repository;

    private FixedIncomeUpdateOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IFixedIncomeUpdateOnlyRepository>();
    }

    public static FixedIncomeUpdateOnlyRepositoryBuilder Instance()
    {
        _instance = new FixedIncomeUpdateOnlyRepositoryBuilder();
        return _instance;
    }

    public IFixedIncomeUpdateOnlyRepository Build()
    {
        return _repository.Object;
    }
}