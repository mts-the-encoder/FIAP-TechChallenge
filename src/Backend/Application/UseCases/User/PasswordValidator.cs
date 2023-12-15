using Exceptions;
using FluentValidation;

namespace Application.UseCases.User;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ErrorMessages.SENHA_EM_BRANCO);

        When(password => !string.IsNullOrWhiteSpace(password),() =>
        {
            RuleFor(password => password).MinimumLength(6).WithMessage(ErrorMessages.SENHA_CURTA);
        });
    }
}