using Bogus;
using Domain.Entities;
using Domain.Enums;

namespace Utils.Entities;

public class VariableIncomeBuilder
{
    public static VariableIncome Build()
    {
        var vIncomeCreated = new Faker<VariableIncome>()
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.UserId, f => f.Random.Int())
            .RuleFor(x => x.Sender, f => f.Company.CompanyName())
            .RuleFor(x => x.Name, f => f.Company.CompanySuffix())
            .RuleFor(x => x.Dividends, f => f.Random.Double())
            .RuleFor(x => x.MinimumInvestment, f => f.Random.Double())
            .RuleFor(x => x.InvestmentVariableType, f => f.PickRandom<InvestmentVariableType>())
            .RuleFor(x => x.Sector, f => f.PickRandom<Sector>());

        return vIncomeCreated;
    }
}

