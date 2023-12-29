using Application.UseCases.FixedIncome.Create;
using Communication.Enum;
using Exceptions;
using Exceptions.ExceptionBase;
using FluentAssertions;
using Utils.Entities;
using Utils.Mapper;
using Utils.Repositories;
using Utils.Repositories.FixedIncome;
using Utils.Requests;
using Utils.UserSigned;
using Xunit;

namespace UseCases.Tests.FixedIncome.Create;

public class CreateFixedIncomeUseCaseTest
{

    [Fact]
    public async Task Validate_Success()
    {
        var (user, _) = UserBuilder.Build();

        var request = FixedIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();
        response.Id.Should().NotBeNullOrWhiteSpace();
        response.Sender.Should().Be(request.Sender);
        response.InvestmentFixedType.Should().Be(request.InvestmentFixedType);
    }

    [Fact]
    public async Task Validate_Error_Sender_Empty()
    {
        var (user, _) = UserBuilder.Build();

        var request = FixedIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.Sender = string.Empty;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.EMISSOR_NAO_PODE_SER_VAZIO));
    }

    [Fact]
    public async Task Validate_Error_Type_Null()
    {
        var (user, _) = UserBuilder.Build();

        var request = FixedIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.InvestmentFixedType = (InvestmentFixedType)10;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.TIPO_INVESTIMENTO_FIXO_INVALIDO));
    }

    [Fact]
    public async Task Validate_Error_Investment_Value()
    {
        var (user, _) = UserBuilder.Build();

        var request = FixedIncomeRequestBuilder.Build();

        var useCase = CreateUseCase(user);

        request.MinimumInvestment = 20.0;

        var action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_50));
    }

    private static CreateFixedIncomeUseCase CreateUseCase(Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Instance();
        var unitOfWork = UnitOfWorkBuilder.Instance().Build();
        var repositoryWriteOnly = FixedIncomeWriteOnlyRepositoryBuilder.Instance().Build();
        var userSigned = UserSignedBuilder.Instance().GetUser(user).Build();

        return new CreateFixedIncomeUseCase(mapper, repositoryWriteOnly, unitOfWork, userSigned);
    }
}