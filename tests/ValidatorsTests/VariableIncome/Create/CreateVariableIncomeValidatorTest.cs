using Application.UseCases.VariableIncome.Create;
using Communication.Enum;
using Exceptions;
using FluentAssertions;
using Utils.Requests;
using Xunit;

namespace Validators.Tests.VariableIncome.Create;

public class CreateVariableIncomeValidatorTest
{
    [Fact]
    public void Validate_Success()
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Error_Blank_Name()
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.NOME_NAO_PODE_SER_VAZIO);
    }

    [Theory]
    [InlineData(0.2)]
    [InlineData(0.5)]
    public void Validate_Error_Minimum_Investment(double value)
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();
        request.MinimumInvestment = value;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_0);
    }

    [Fact]
    public void Validate_Error_Investment_Type()
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();
        request.InvestmentVariableType = (InvestmentVariableType)1000;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.TIPO_INVESTIMENTO_VARIAVEL_INVALIDO);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.10)]
    [InlineData(0.15)]
    public void Validate_Error_Minimum_Dividends(double dividends)
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();
        request.Dividends = dividends;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.DIVIDENDOS_DEVE_SER_MAIOR_OU_IGUAL_A_0);
    }

    [Fact]
    public void Validate_Error_Sector()
    {
        var validator = new VariableIncomeValidator();

        var request = VariableIncomeRequestBuilder.Build();
        request.Sector = (Sector)1000;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.SETOR_INVALIDO);
    }
}