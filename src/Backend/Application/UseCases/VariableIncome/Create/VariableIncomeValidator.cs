using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.VariableIncome.Create;

public class VariableIncomeValidator : AbstractValidator<VariableIncomeRequest>
{
    public VariableIncomeValidator()
    {
        RuleFor(x => x.Sender).NotEmpty().WithMessage("");
        RuleFor(x => x.Name).NotEmpty().WithMessage("");
        RuleFor(x => x.MinimumInvestment).GreaterThan(0).WithMessage("");
        RuleFor(x => x.InvestmentVariableType).IsInEnum().WithMessage("");
        RuleFor(x => x.Dividends).GreaterThanOrEqualTo(0).WithMessage("");
        RuleFor(x => x.Sector).IsInEnum().WithMessage("");
    }
}

