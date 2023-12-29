using Application.UseCases.FixedIncome.Create;
using Communication.Enum;
using Exceptions;
using FluentAssertions;
using Utils.Requests;
using Xunit;

namespace Validators.Tests.FixedIncome.Create;

public class CreateFixedIncomeValidatorTest
{
    [Fact]
    public void Validate_Success()
    {
        var validator = new FixedIncomeValidator();

        var request = FixedIncomeRequestBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Error_Blank_Sender()
    {
        var validator = new FixedIncomeValidator();

        var request = FixedIncomeRequestBuilder.Build();
        request.Sender = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.EMISSOR_NAO_PODE_SER_VAZIO);
    }

    [Fact]
    public void Validate_Error_Investment_Type()
    {
        var validator = new FixedIncomeValidator();

        var request = FixedIncomeRequestBuilder.Build();
        request.InvestmentFixedType = (InvestmentFixedType)1000;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.TIPO_INVESTIMENTO_FIXO_INVALIDO);
    }

    [Theory]
    [InlineData(10.0)]
    [InlineData(15.00)]
    [InlineData(20.00)]
    [InlineData(25.00)]
    [InlineData(30.00)]
    [InlineData(35.00)]
    [InlineData(40.00)]
    [InlineData(45.00)]
    public void Validate_Error_Minimum_Investment(double investment)
    {
        var validator = new FixedIncomeValidator();

        var request = FixedIncomeRequestBuilder.Build();
        request.MinimumInvestment = investment;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_50);
    }
}