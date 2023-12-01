using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.User.Create;

public class CreateValidator : AbstractValidator<UserRequest>
{
    public CreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.NOME_EM_BRANCO);
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
    }
}