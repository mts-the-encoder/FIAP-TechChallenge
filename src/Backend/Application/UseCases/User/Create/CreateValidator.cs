using Communication.Requests;
using Exceptions;
using FluentValidation;
using FluentValidation.Results;
using static System.Text.RegularExpressions.Regex;

namespace Application.UseCases.User.Create;

public class CreateValidator : AbstractValidator<UserRequest>
{
    public CreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.NOME_EM_BRANCO);
        RuleFor(x => x.Email).NotEmpty().WithMessage(ErrorMessages.EMAIL_EM_BRANCO);
        RuleFor(x => x.Phone).NotEmpty().WithMessage(ErrorMessages.TELEFONE_EM_BRANCO);
        RuleFor(x => x.CNPJ).NotEmpty().WithMessage(ErrorMessages.CNPJ_EM_BRANCO);
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(ErrorMessages.EMAIL_INVALIDO);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Phone), () =>
        {
             RuleFor(x => x.Phone).Custom((phone, context) =>
             {
                 const string pattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                 var isMatch = IsMatch(phone, pattern);

                 if (!isMatch) context.AddFailure(new ValidationFailure(nameof(phone), ErrorMessages.TELEFONE_INVALIDO));
             });
        });

        When(x => !string.IsNullOrWhiteSpace(x.CNPJ), () =>
        {
            RuleFor(x => x.CNPJ).Custom((cnpj, context) =>
            {
                const string cnpjPattern = @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$";
                var isMatch = IsMatch(cnpj, cnpjPattern);

                if (!isMatch)
                    context.AddFailure(new ValidationFailure(nameof(cnpj), ErrorMessages.CNPJ_INVALIDO));
            });
        });

    }
}