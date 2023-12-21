using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.VariableIncome.Create;

public class VariableIncomeValidator : AbstractValidator<VariableIncomeRequest>
{
    public VariableIncomeValidator()
    {
        RuleFor(x => x.Sender).NotEmpty().WithMessage(ErrorMessages.EMISSOR_NAO_PODE_SER_VAZIO);
        RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorMessages.NOME_NAO_PODE_SER_VAZIO);
        RuleFor(x => x.MinimumInvestment).GreaterThan(1.0).WithMessage(ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_0);
        RuleFor(x => x.InvestmentVariableType).IsInEnum().WithMessage(ErrorMessages.TIPO_INVESTIMENTO_VARIAVEL_INVALIDO);
        RuleFor(x => x.Dividends).GreaterThanOrEqualTo(0.2).WithMessage(ErrorMessages.DIVIDENDOS_DEVE_SER_MAIOR_OU_IGUAL_A_0);
        RuleFor(x => x.Sector).IsInEnum().WithMessage(ErrorMessages.SETOR_INVALIDO);
    }
}