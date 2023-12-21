using Communication.Requests;
using Exceptions;
using FluentValidation;

namespace Application.UseCases.VariableIncome.FixedIncome.Create;

public class FixedIncomeValidator : AbstractValidator<FixedIncomeRequest>
{
    public FixedIncomeValidator()
    {
        RuleFor(x => x.Sender).NotEmpty().WithMessage(ErrorMessages.EMISSOR_NAO_PODE_SER_VAZIO);
        RuleFor(x => x.MinimumInvestment).GreaterThan(50.0).WithMessage(ErrorMessages.INVESTIMENTO_MINIMO_DEVE_SER_MAIOR_QUE_50);
        RuleFor(x => x.InvestmentFixedType).IsInEnum().WithMessage(ErrorMessages.TIPO_INVESTIMENTO_FIXO_INVALIDO);
        RuleFor(x => x.Profitability).NotEmpty().WithMessage(ErrorMessages.RENTABILIDADE_NAO_PODE_SER_VAZIA);
        RuleFor(x => x.MaturityDate).Must(BeValidDate).WithMessage(ErrorMessages.DATA_VENCIMENTO_INVALIDA);
        RuleFor(x => x.IR).NotEmpty().GreaterThanOrEqualTo(10).WithMessage(ErrorMessages.IR_NAO_PODE_SER_VAZIO_MAIOR_OU_IGUAL_A_10);
    }

    private static bool BeValidDate(DateTime date) => date > DateTime.Now;
}