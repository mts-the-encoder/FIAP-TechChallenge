using Application.UseCases.VariableIncome.Create;
using Communication.Enum;
using Exceptions;
using Exceptions.ExceptionBase;
using FluentAssertions;
using Utils.Entities;
using Utils.Mapper;
using Utils.Repositories;
using Utils.Repositories.User;
using Utils.Repositories.VariableIncome;
using Utils.Requests;
using Utils.UserSigned;
using Xunit;

namespace UseCases.Tests.VariableIncome.Create;

public class CreateVariableIncomeUseCaseTest
{
    [Fact]
    public async Task Validate_Success()
    {
        var (user, _) = UserBuilder.Build();

        var request = VariableIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();
        response.Id.Should().NotBeNullOrWhiteSpace();
        response.Name.Should().Be(request.Name);
        response.InvestmentVariableType.Should().Be(request.InvestmentVariableType);
    }

    [Fact]
    public async Task Validate_Error_Name_Empty()
    {
        var (user, _) = UserBuilder.Build();

        var request = VariableIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.Name = string.Empty;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.NOME_NAO_PODE_SER_VAZIO));
    }

    [Fact]
    public async Task Validate_Error_Sector_Null()
    {
        var (user, _) = UserBuilder.Build();

        var request = VariableIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.Sector = (Sector)10;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.SETOR_INVALIDO));
    }

    [Fact]
    public async Task Validate_Error_Investment_Value()
    {
        var (user, _) = UserBuilder.Build();

        var request = VariableIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.MinimumInvestment = 0.0;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_0));
    }

    private static CreateVariableIncomeUseCase CreateUseCase(Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Instance();
        var unitOfWork = UnitOfWorkBuilder.Instance().Build();
        var repositoryWriteOnly = VariableIncomeWriteOnlyRepositoryBuilder.Instance().Build();
        var userSigned = UserSignedBuilder.Instance().GetUser(user).Build();

        return new CreateVariableIncomeUseCase(mapper, repositoryWriteOnly, userSigned, unitOfWork);
    }
}