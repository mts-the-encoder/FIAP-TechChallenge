using Application.UseCases.Update;
using Domain.Enums;
using Utils.Mapper;
using Utils.Repositories.VariableIncome;
using Utils.Repositories;
using Utils.UserSigned;
using FluentAssertions;
using Utils.Entities;
using Utils.Requests;
using Xunit;

namespace UseCases.Tests.VariableIncome.Update;

public class UpdateVariableIncomeUseCaseTest
{
    [Fact]
    public async Task Validate_Success()
    {
        var (user, _) = UserBuilder.Build();

        var investment = VariableIncomeBuilder.Build();

        var useCase = CreateUseCase(user, investment);

        var request = VariableIncomeRequestBuilder.Build();

        await useCase.Execute(investment.Id, request);

        var action = async () => { await useCase.Execute(user.Id, request); };

        await action.Should().NotThrowAsync();

        investment.Should().NotBeNull();
    }

    private UpdateVariableIncomeUseCase CreateUseCase(Domain.Entities.User user, Domain.Entities.VariableIncome investment)
    {
        var mapper = MapperBuilder.Instance();
        var unitOfWork = UnitOfWorkBuilder.Instance().Build();
        var repository = VariableIncomeUpdateOnlyRepositoryBuilder.Instance().GetById(investment).Build();
        var userSigned = UserSignedBuilder.Instance().GetUser(user).Build();

        return new UpdateVariableIncomeUseCase(repository, mapper, userSigned, unitOfWork);
    }
}