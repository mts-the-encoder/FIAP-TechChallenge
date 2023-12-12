using Application.UseCases.User.Create;
using Exceptions;
using FluentAssertions;
using Utils.Requests;
using Xunit;

namespace Validators.Tests.Users.Create;

public class CreateUserValidatorTest
{
    [Fact]
    public void Validate_Success()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Error_Blank_Name()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.NOME_EM_BRANCO);
    }

    [Fact]
    public void Validate_Error_Blank_Email()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.EMAIL_EM_BRANCO);
    }

    [Fact]
    public void Validate_Error_Invalid_Email()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.Email = "email@";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.EMAIL_INVALIDO);
    }

    [Fact]
    public void Validate_Error_Short_Password()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.SENHA_EM_BRANCO);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validate_Error_Blank_Password(int passwordLength)
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.SENHA_CURTA);
    }

    [Fact]
    public void Validate_Error_Invalid_Phone()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.Phone = "01 0 0101-0101";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.TELEFONE_INVALIDO);
    }

    [Fact]
    public void Validate_Error_Invalid_Cnpj()
    {
        var validator = new CreateValidator();

        var request = UserRequestBuilder.Build();
        request.CNPJ = "123456780001-90";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ErrorMessages.CNPJ_INVALIDO);
    }
}