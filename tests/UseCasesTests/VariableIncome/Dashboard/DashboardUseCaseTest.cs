using Application.Services.UserSigned;
using Application.UseCases.DashboardVariable;
using Communication.Requests;
using FluentAssertions;
using Utils.Entities;
using Utils.Mapper;
using Utils.Repositories.VariableIncome;
using Utils.UserSigned;
using Xunit;

namespace UseCases.Tests.VariableIncome.Dashboard;

public class DashboardUseCaseTest
{
    [Fact]
    public async Task Validate_Success_No_Investments()
    {
        var (user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute(new());

        response.Investments.Should().HaveCount(0);
    }

    private static DashboardVariableUseCase CreateUseCase(
        Domain.Entities.User user,
        Domain.Entities.VariableIncome investment = null)
    {
        var userSigned = UserSignedBuilder.Instance().GetUser(user).Build();
        var mapper = MapperBuilder.Instance();
        var readOnlyRepository = VariableIncomeReadOnlyRepositoryBuilder.Instance().Build();

        return new DashboardVariableUseCase(readOnlyRepository, mapper, userSigned);
    }
}