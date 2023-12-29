using Application.UseCases.VariableIncome.Delete;
using FluentAssertions;
using Utils.Entities;
using Utils.Repositories;
using Utils.Repositories.VariableIncome;
using Utils.UserSigned;
using Xunit;

namespace UseCases.Tests.FixedIncome.Delete;

public class DeleteUseCaseTest
{
    [Fact]
    public async Task Validate_Success()
    {
        var (user, _) = UserBuilder.Build();

        var investment = VariableIncomeBuilder.Build();

        var useCase = CreateUseCase(user, investment);

        var action = async () => { await useCase.Execute(investment.Id); };

        await action.Should().NotThrowAsync();
    }

    private static DeleteVariableIncomeUseCase CreateUseCase(Domain.Entities.User user, Domain.Entities.VariableIncome investment)
    {
        var userSigned = UserSignedBuilder.Instance().GetUser(user).Build();
        var writeOnlyRepository = VariableIncomeWriteOnlyRepositoryBuilder.Instance().Build();
        var readOnlyRepository = VariableIncomeReadOnlyRepositoryBuilder.Instance().GetById(investment).Build();
        var unitOfWork = UnitOfWorkBuilder.Instance().Build();

        return new DeleteVariableIncomeUseCase(unitOfWork, userSigned, writeOnlyRepository, readOnlyRepository);
    }
}