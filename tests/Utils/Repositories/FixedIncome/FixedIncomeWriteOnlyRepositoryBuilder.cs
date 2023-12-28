using Domain.Repositories.Investments;
using Moq;

namespace Utils.Repositories.FixedIncome;

public class FixedIncomeWriteOnlyRepositoryBuilder
{
    private static FixedIncomeWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<IFixedIncomeWriteOnlyRepository> _repository;

    private FixedIncomeWriteOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IFixedIncomeWriteOnlyRepository>();
    }

    public static FixedIncomeWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new FixedIncomeWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IFixedIncomeWriteOnlyRepository Build()
    {
        return _repository.Object;
    }
}