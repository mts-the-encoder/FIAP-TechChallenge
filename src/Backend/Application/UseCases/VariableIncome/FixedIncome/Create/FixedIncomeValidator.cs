using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.VariableIncome.FixedIncome.Create;

public class FixedIncomeValidator : AbstractValidator<FixedIncomeRequest>
{
    public FixedIncomeValidator()
    {
        RuleFor(x => x.Sender).NotEmpty().WithMessage("");
        RuleFor(x => x.MinimumInvestment).GreaterThan(50.0).WithMessage("");
        RuleFor(x => x.InvestmentFixedType).IsInEnum().WithMessage("");
        RuleFor(x => x.Profitability).NotEmpty().WithMessage("");
        RuleFor(x => x.MaturityDate).Must(BeValidDate).WithMessage("");
        RuleFor(x => x.IR).NotEmpty().GreaterThanOrEqualTo(10).WithMessage("");
    }

    private static bool BeValidDate(DateTime date) => date > DateTime.Now;
}