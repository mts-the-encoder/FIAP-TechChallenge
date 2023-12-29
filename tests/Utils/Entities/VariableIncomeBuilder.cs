using Bogus;
using Domain.Entities;
using Domain.Enums;

namespace Utils.Entities;

public class VariableIncomeBuilder
{
    public static VariableIncome Build()
    {
        var vIncomeCreated = new Faker<VariableIncome>()
            .RuleFor(x => x.Id,_ => 1)
            .RuleFor(x => x.UserId,_ => 1)
            .RuleFor(x => x.Sender, f => f.Company.CompanyName())
            .RuleFor(x => x.Name, f => f.Company.CompanySuffix())
            .RuleFor(x => x.Dividends, _ => 0.50)
            .RuleFor(x => x.MinimumInvestment,_ => 10.0)
            .RuleFor(x => x.InvestmentVariableType, f => f.PickRandom<InvestmentVariableType>())
            .RuleFor(x => x.Sector, f => f.PickRandom<Sector>());

        return vIncomeCreated;
    }
}

