using Communication.Requests;
using FluentValidation;

namespace Application.UseCases.VariableIncome.Create;

public class VariableIncomeValidator : AbstractValidator<VariableIncomeRequest>
{
    public VariableIncomeValidator()
    {
        RuleFor(x => x.Sender).NotEmpty().WithMessage("Sender cannot be empty");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(x => x.MinimumInvestment).GreaterThan(0).WithMessage("Minimum investment must be greater than 0");
        RuleFor(x => x.InvestmentVariableType).IsInEnum().WithMessage("Invalid investment variable type");
        RuleFor(x => x.Dividends).GreaterThanOrEqualTo(0).WithMessage("Dividends must be greater than or equal to 0");
        RuleFor(x => x.Sector).IsInEnum().WithMessage("Invalid sector");
    }
}